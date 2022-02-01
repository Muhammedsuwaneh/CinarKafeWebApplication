using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using CinarKafe.Models;
using CinarKafe.Db;
using System.Data;
using System.Data.SqlClient;
using CinarKafe.Models.ViewModels;
using Dapper;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Authentication;

namespace CinarKafe.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole(RoleNames.CanManageApplication) && Request.IsAuthenticated)
            {
                // init homeVM
                var homeViewModel = new HomeViewModel();

                // fill pie chart model
                var pieChartData = new PieChartViewModel();

                try
                {
                    pieChartData = GetPieChartData();

                    // fill bar chart model
                    var barChartData = GetBarChartData();

                    // grid view data 
                    var gridViewData = GetGridViewData();

                    homeViewModel.PieChartData = pieChartData;

                    homeViewModel.BarChartData = barChartData;

                    homeViewModel.GridViewData = gridViewData;

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

                return View("Dashboard", homeViewModel);
            }

            else
            {

                string connString = Tools.GetConnectionString();

                IEnumerable<Urun> urunler;

                using (IDbConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        urunler = conn.Query<Urun, UrunKategorisi, Urun>("dbo.sp_urunlistele",
                        (urun, kategori) => { urun.UrunKategorisi = kategori; return urun; },
                        splitOn: "UrunKategorisiId", commandType: CommandType.StoredProcedure);
                    }
                    catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        return RedirectToAction("ServerError", "Home");
                    }
                    catch(WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToAction("PageNotFoundError", "Home");
                    }
                    catch(WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.RequestTimeout)
                    {
                        return RedirectToAction("ServerError", "Home");
                    }
                    catch(SocketException)
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
                    catch(AuthenticationException)
                    {
                        return RedirectToAction("ServerError", "Home");
                    }
                    catch(TimeoutException)
                    {
                        return RedirectToAction("ServerError", "Home");
                    }
                }

                return View(urunler);
            }
        }

        /// <summary>
        /// Fetch and populate pie chart data
        /// </summary>
        /// <returns></returns>
        public PieChartViewModel GetPieChartData()
        {
            var model = new PieChartViewModel();

            var labels = new List<string>();

            labels.Add("Ürünler");
            labels.Add("Masalar");
            labels.Add("Garsonlar");

            model.labels = labels;

            var childModel = new PieChartChildViewModel();

            var datasets = new List<PieChartChildViewModel>();

            var backgroundColorList = new List<string>();

            var dataList = new List<int>();

            int urunCount = GetUrunCount();

            int masaCount = GetMasaCount();

            int garsonCount = GetGarsonCount();
          

            foreach (var label in labels)
            {
                if(label == "Ürünler")
                {
                    backgroundColorList.Add("#ff6384");

                    dataList.Add(urunCount);
                }

                if(label == "Masalar")
                {

                    backgroundColorList.Add("#36a2eb");

                    dataList.Add(masaCount);

                }

                if(label == "Garsonlar")
                {
                    backgroundColorList.Add("#ffce56");

                    dataList.Add(garsonCount);
                }
            }

            childModel.backgroundColor = backgroundColorList;
            childModel.data = dataList;

            datasets.Add(childModel);
            model.datasets = datasets;

            return model;
        }

        /// <summary>
        /// Fetch and populate bar chart data 
        /// </summary>
        /// <returns></returns>
        public BarChartViewModel GetBarChartData()
        {
            var model = new BarChartViewModel();

            var labels = new List<string>();

            labels.Add("Ürünler");
            labels.Add("Masalar");
            labels.Add("Garsonlar");

            model.labels = labels;

            var childModel = new BarChartChildViewModel();

            var datasets = new List<BarChartChildViewModel>();

            var backgroundColorList = new List<string>();

            var borderColorList = new List<string>();

            var dataList = new List<int>();

            int urunCount = GetUrunCount();

            int masaCount = GetMasaCount();

            int garsonCount = GetGarsonCount();


            foreach (var label in labels)
            {
                if (label == "Ürünler")
                {
                    backgroundColorList.Add("#ff6384");

                    borderColorList.Add("#ff6384");

                    dataList.Add(urunCount);
                }

                if (label == "Masalar")
                {

                    backgroundColorList.Add("#36a2eb");

                    borderColorList.Add("#36a2eb");

                    dataList.Add(masaCount);
                }

                if (label == "Garsonlar")
                {
                    backgroundColorList.Add("#ffce56");

                    borderColorList.Add("#ffce56");

                    dataList.Add(garsonCount);
                }
            }

            childModel.borderWidth = 2;

            childModel.hoverBorderColor = "#ff6384";

            childModel.backgroundColor = backgroundColorList;
            childModel.borderColor = borderColorList;
            childModel.data = dataList;

            datasets.Add(childModel);
            model.datasets = datasets;

            return model;
        }

        public Dictionary<Tuple<string, string>, int> GetGridViewData()
        {
            int urunCount = GetUrunCount();
            int masaCount = GetMasaCount();
            int garsonCount = GetGarsonCount();

            Dictionary<Tuple<string, string>, int> data;

            data = new Dictionary<Tuple<string, string>, int>();

            data.Add(new Tuple<string, string>("Ürünler", "fa fa-utensils"), urunCount);
            data.Add(new Tuple<string, string>("Masalar", "fa fa-users"), masaCount);
            data.Add(new Tuple<string, string>("Garsonlar", "fa fa-table"), garsonCount);

            return data;
        }

        /// <summary>
        /// Gets the total number of uruns from Db
        /// </summary>
        /// <returns></returns>
        public int GetUrunCount()
        { 
            string connString = Tools.GetConnectionString();

            int urunCount;

            var param = new DynamicParameters();

            param.Add("@ireturnvalue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            // get urun count
            using (IDbConnection conn = new SqlConnection(connString))
            {

               conn.Execute("dbo.fn_geturuncount", param, commandType: CommandType.StoredProcedure);
            }

            urunCount = param.Get<int>("@ireturnvalue");

            return urunCount;
        }


        /// <summary>
        /// Gets the total number of masas from Db
        /// </summary>
        /// <returns></returns>
        public int GetMasaCount()
        {
            string connString = Tools.GetConnectionString();

            int masaCount;

            var param = new DynamicParameters();

            param.Add("@ireturnvalue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            // get urun count
            using (IDbConnection conn = new SqlConnection(connString))
            {
                conn.Execute("dbo.fn_getmasacount", param, commandType: CommandType.StoredProcedure);
            }

            masaCount = param.Get<int>("@ireturnvalue");

            return masaCount;
        }


        /// <summary>
        /// Gets the total number of garsons from Db
        /// </summary>
        /// <returns></returns>
        public int GetGarsonCount()
        {
            string connString = Tools.GetConnectionString();

            int garsonCount;

            var param = new DynamicParameters();

            param.Add("@ireturnvalue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            // get urun count
            using (IDbConnection conn = new SqlConnection(connString))
            {
                conn.Execute("dbo.fn_getgarsoncount", param, commandType: CommandType.StoredProcedure);
            }

            garsonCount = param.Get<int>("@ireturnvalue");

            return garsonCount;
        }

        /// <summary>
        /// Redirects to the sepetim page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Sepetim()
        {
            if (User.IsInRole(RoleNames.CanManageApplication))
                return RedirectToAction("Index", "Home");

            List<SiperisViewModel> viewModel;

            IEnumerable<Masa> masalar;

            string connString = Tools.GetConnectionString();

            using(IDbConnection conn = new SqlConnection(connString))
            {
                try {
                     masalar = conn.Query<Masa, MasaDurumu, Masa>("dbo.sp_mevcutmasalarlistele",
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
                catch(TimeoutException)
                {
                    return RedirectToAction("Server", "Home");
                }

                viewModel = new List<SiperisViewModel>();
                viewModel.Add(new SiperisViewModel { Masalar = masalar });

                //viewModel.RemoveAt(0);
            }

            return View(viewModel);
        }

        public ActionResult ServerError()
        {
            return View();
        }

        public ActionResult PageNotFoundError()
        {
            return View();
        }
    }
}