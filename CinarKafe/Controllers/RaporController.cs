using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinarKafe.Models;
using Dapper;
using CinarKafe.Db;
using System.Data;
using System.Data.SqlClient;
using CinarKafe.Models.ViewModels;
using System.Security.Authentication;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace CinarKafe.Controllers
{
    [Authorize(Roles = RoleNames.CanManageApplication)]
    public class RaporController : Controller
    {
        // GET: Rapor
        public ActionResult Index()
        {
            string connString = Tools.GetConnectionString();
            IEnumerable<RaporViewModel> raporlistesi;

            using(IDbConnection conn = new SqlConnection(connString))
            {
                try
                {
                //    raporlistesi = conn.Query<RaporViewModel, Siperis, Urun, UrunKategorisi, RaporViewModel>("db_a7dfd0_cinardb.sp_raporlistele", 
                //    (rapor, siperis, urun, urunKategorisi) => 
                //    { rapor.Siperis = siperis; rapor.Urun = urun; rapor.UrunKategorisi = urunKategorisi; return rapor; },
                //    splitOn: "Id, Id, Id, Id", commandType: CommandType.StoredProcedure);
                
                    raporlistesi = conn.Query<RaporViewModel, Siperis, Urun, UrunKategorisi, RaporViewModel>("dbo.sp_raporlistele",
                        (rapor, siperis, urun, urunKategorisi) =>
                        { rapor.Siperis = siperis; siperis.Urun = urun; urun.UrunKategorisi = urunKategorisi; return rapor; },
                        splitOn: "Id, Id, Id, Id", commandType: CommandType.StoredProcedure);
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
            }

            return View(raporlistesi);
        }
    }
}