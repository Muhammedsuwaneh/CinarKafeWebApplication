using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using CinarKafe.Models;
using CinarKafe.Db;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Authentication;

namespace CinarKafe.Controllers
{
    [Authorize]
    public class SiperislerController : Controller
    {
        // GET: Siperisler
        public ActionResult Index()
        {
            if (User.IsInRole(RoleNames.CanManageApplication))
                return RedirectToAction("Index", "Home");

            else
            {
                // fetch all orders of current user
                string connString = Tools.GetConnectionString();

                var param = new DynamicParameters();

                IEnumerable<Siperisler> data;
                var userId = User.Identity.GetUserId().ToString();

                param.Add("p_ApplicationUserId", userId);

                using (IDbConnection conn = new SqlConnection(connString))
                {

                    try
                    {
                        data = conn.Query<Siperisler, ApplicationUser, Siperis, Urun, UrunKategorisi, Siperisler>("dbo.sp_siperislerim",
                            (siperisler, user, siperis, urun, kategori) =>
                            { siperisler.ApplicationUser = user; siperisler.Siperis = siperis; siperis.Urun = urun; urun.UrunKategorisi = kategori; return siperisler; },
                            param, splitOn: "Id, Id, Id, Id, Id", commandType: CommandType.StoredProcedure);
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

                    return View(data);

                }
            }
        }
    }
}