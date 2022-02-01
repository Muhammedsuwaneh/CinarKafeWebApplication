using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinarKafe.Models;
using CinarKafe.Models.ViewModels;
using CinarKafe.Db;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Authentication;

namespace CinarKafe.Controllers
{
    [Authorize(Roles = RoleNames.CanManageApplication)]
    public class MasalarController : Controller
    {
        private ApplicationDbContext _context;

        public MasalarController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Masalar
        public ActionResult Index()
        {
            string connString = Tools.GetConnectionString();

            IEnumerable<Masa> data;

            using (IDbConnection conn = new SqlConnection(connString))
            {
                try
                {
                    data = conn.Query<Masa, MasaDurumu, Masa>("dbo.sp_masalistele",
                    (masa, durum) => { masa.MasaDurumu = durum; return masa; },
                    splitOn: "MasaDurumuId", commandType: CommandType.StoredProcedure);

                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    return RedirectToAction("PageNotFoundError", "Home");
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (SocketException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (SqlException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (IOException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (AuthenticationException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (TimeoutException)
                {
                    return RedirectToAction("Server", "Home");
                }
            }

            return View(data);
        }

        /// <summary>
        /// Displays the new masa for page
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var viewModel = new MasaFormViewModel();

            return View(viewModel);
        }

        /// <summary>
        /// Saves a new masa
        /// </summary>
        /// <param name="masa"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Masa masa)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MasaFormViewModel(masa)
                {
                    MasaDurumu = _context.MasaDurumlar.ToList()
                };

                return View("New", viewModel);
            }

            string connString = Tools.GetConnectionString();

            if(masa.Id == 0)
            {
                var viewModel = new MasaFormViewModel();

                using(IDbConnection conn = new SqlConnection(connString))
                {
                    var param1 = new DynamicParameters();

                    param1.Add("MasaNumarasi", masa.MasaNumarasi);
                    param1.Add("@ireturnvalue", DbType.Int32, direction: ParameterDirection.ReturnValue);

                    // check if masa exists
                    conn.Execute("dbo.fn_doesmasaexist", param1, commandType: CommandType.StoredProcedure);

                    var DoesMasaExist = param1.Get<int>("@ireturnvalue");

                    // enters if masa exist i.e if the value returned is not an Id 
                    if(DoesMasaExist != -1)
                    {
                        // throw an error and redirect to form 
                        ModelState.AddModelError("", "Hata ! Eklemek istediğiniz masa zaten mevcut");

                        return View("New", viewModel);
                    }

                    var param2 = new DynamicParameters(); 

                    param2.Add("p_MasaNumarasi", masa.MasaNumarasi);
                    param2.Add("p_MasaKapasitesi", masa.MasaKapasitesi);
                    param2.Add("p_MasaDurumuId", masa.MasaDurumuId);
                    param2.Add("p_KacKisiOturuyor", 0);

                    // save new masa
                    var rowsAffected = conn.Execute("dbo.sp_masaekle", param2, commandType: CommandType.StoredProcedure);

                    if(rowsAffected == 0)
                    {
                        ModelState.AddModelError("", "Hata oluştu ! Masa eklenmedi");

                        return View("New", viewModel);
                    }
                }
            }

            return RedirectToAction("Index", "Masalar");
        }

        public ActionResult Detail(int id)
        {
            string connString = Tools.GetConnectionString();

            var param = new DynamicParameters();

            param.Add("p_MasaNumarasi", id);

            IEnumerable<Siperisler> detail;

            using (IDbConnection conn = new SqlConnection(connString))
            {
                try
                {
                    detail = conn.Query<Siperisler, ApplicationUser, Siperis, Masa, Urun, UrunKategorisi, Siperisler>("dbo.sp_masabilgisilistele",
                    (siperisler, user, siperis, masa, urun, kategori) => 
                    { siperisler.ApplicationUser = user; siperisler.Siperis = siperis; siperisler.Masa = masa; siperis.Urun = urun; urun.UrunKategorisi = kategori; return siperisler; },
                    param, splitOn: "Id, Id, Id, Id, Id, Id", commandType: CommandType.StoredProcedure);

                   // test
                   // detail = conn.Query<Siperisler, Siperis, Urun, UrunKategorisi, Siperisler>("db_a7dfd0_cinardb.sp_masabilgisilistele",
                   //(siperisler, siperis, urun, kategori) => { siperisler.Siperis = siperis; siperis.Urun = urun; urun.UrunKategorisi = kategori; return siperisler; },
                   //param, commandType: CommandType.StoredProcedure);
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    return RedirectToAction("PageNotFoundError", "Home");
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (SocketException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (SqlException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (IOException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch (AuthenticationException)
                {
                    return RedirectToAction("ServerError", "Home");
                }
                catch(TimeoutException)
                {
                    return RedirectToAction("ServerError", "Home");
                }


                return View(detail);

            }
        }
    }
}