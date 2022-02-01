using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinarKafe.Models;
using CinarKafe.App_Start;
using CinarKafe.Models.ViewModels;
using CinarKafe.Dtos;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using CinarKafe.Db;

namespace CinarKafe.Controllers
{

    [Authorize(Roles = RoleNames.CanManageApplication)]
    public class GarsonlarController : Controller
    {

        //private readonly MySqlConnection _connection;

        // GET: Garsonlar
        /// <summary>
        /// Garson bilgisini listeleme
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string connString = Tools.GetConnectionString();

            IEnumerable<Garson> data;

            using (IDbConnection conn = new SqlConnection(connString))
            {
                data = conn.Query<Garson>("dbo.sp_garsonlistele", commandType: CommandType.StoredProcedure);
            }

            return View(data);
        }

        /// <summary>
        /// Yeni garson ekleme sayfasi
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var garsonViewModel = new GarsonFormViewModel();

            return View("New", garsonViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Garson garson)
        {

            if (!ModelState.IsValid)
            {
                var model = new GarsonFormViewModel();

                return View("New", model);
            }

            string connString = Tools.GetConnectionString();

            // add a new garson
            if(garson.Id == 0)
            {
                var viewModel = new GarsonFormViewModel();

                using (IDbConnection cnn = new SqlConnection(connString))
                {
                    var parameters1 = new DynamicParameters();
                    var parameters2 = new DynamicParameters();


                    parameters1.Add("@p_Id", DbType.Int32, direction: ParameterDirection.Output);
                    parameters1.Add("p_Ad", garson.Ad);
                    parameters1.Add("p_Soyad", garson.Soyad);
                    parameters1.Add("p_SCN", garson.SCN);
                    parameters1.Add("p_Eposta", garson.Eposta);
                    parameters1.Add("p_Adres", garson.Adres);
                    parameters1.Add("p_TelefonNumara", garson.TelefonNumara);
                    parameters1.Add("p_Maas", garson.Maas);

                    parameters2.Add("@ireturnvalue", DbType.Int32, direction: ParameterDirection.ReturnValue);
                    parameters2.Add("@SCN", garson.SCN);
                    parameters2.Add("@Eposta", garson.Eposta);

                    // check if garson exist
                    cnn.Execute("dbo.fn_doesgarsonexist", parameters2, commandType: CommandType.StoredProcedure);

                    int DoesGarsonExist = parameters2.Get<int>("@ireturnvalue");

                    if (DoesGarsonExist != -1)
                    {
                        ModelState.AddModelError("", "Hata ! Eklemek istediğiniz garson zaten mevcut");

                        return View("New", viewModel);
                    }

                    // save new garson 
                    var rowsAffected = cnn.Execute("dbo.sp_garsonekle", parameters1, commandType: CommandType.StoredProcedure);

                    if (rowsAffected == 0)
                    {
                        ModelState.AddModelError("", "Hata oluştu ! Garson eklenmedi");

                        return View("New", viewModel);
                    }
                }
            }

            else
            {
                using(IDbConnection conn = new SqlConnection(connString))
                {

                    // update garson 
                    conn.Execute("dbo.sp_updategarson", garson, commandType: CommandType.StoredProcedure);
                }
            }

            return RedirectToAction("Index", "Garsonlar");
        }

        public ActionResult Edit(int id)
        {
            Garson garson;

            GarsonFormViewModel viewModel;

            var parameters = new DynamicParameters();

            parameters.Add("Id", id);

            string connString = Tools.GetConnectionString();

            using(IDbConnection conn = new SqlConnection(connString))
            {
                // get garson by id 
                var data = conn.Query<GarsonDto>("dbo.sp_getgarsonbyId", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                // create new garson object 
                garson = new Garson
                {
                   Id = id,
                   Ad = data.Ad,
                   Soyad = data.Soyad,
                   SCN = data.SCN,
                   Eposta = data.Eposta,
                   TelefonNumara = data.TelefonNumara,
                   Adres = data.Adres,
                   Maas = data.Maas,

                };

                viewModel = new GarsonFormViewModel(garson);

                return View("New", viewModel);
            }
        }
    }
}