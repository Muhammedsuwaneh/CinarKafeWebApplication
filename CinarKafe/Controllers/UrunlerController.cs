using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinarKafe.Models;
using CinarKafe.Models.ViewModels;
using CinarKafe.Db;
using Dapper;
using System.Data.Entity;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CinarKafe.Controllers
{

    [Authorize(Roles = RoleNames.CanManageApplication)]
    public class UrunlerController : Controller
    {
        private ApplicationDbContext _context;

        // GET: Urunler
        public ActionResult Index()
        {
            string connString = Tools.GetConnectionString();

            IEnumerable<Urun> urunler;

            using(IDbConnection conn = new SqlConnection(connString))
            {
                urunler = conn.Query<Urun, UrunKategorisi, Urun>("dbo.sp_urunlistele", 
                    (urun, kategori) => { urun.UrunKategorisi = kategori; return urun; }, 
                    splitOn: "UrunKategorisiId", commandType: CommandType.StoredProcedure);
            }

            return View(urunler);
        }

        /// <summary>
        /// displays the new urun form
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            _context = new ApplicationDbContext();

            // get all urun categories
            var urunKategorisi = _context.UrunKategorisis.ToList();

            var viewModel = new UrunFormViewModel
            {
                UrunKategorisi = urunKategorisi
            };

            return View(viewModel);
        }

        /// <summary>
        /// Adds a new urun to the db
        /// </summary>
        /// <param name="urun"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Urun urun)
        {

            if(!ModelState.IsValid)
            {
                var viewModel = new UrunFormViewModel
                {
                    UrunKategorisi = _context.UrunKategorisis.ToList()
                };

                return View("New", viewModel);
            }

            // add a new urun to db 
             if(urun.Id == 0)
            {
                var viewModel = new UrunFormViewModel();

                string connString = Tools.GetConnectionString();

                var parameters1 = new DynamicParameters();

                var parameters2 = new DynamicParameters();

                parameters2.Add("p_UrunAdi", urun.UrunAdi);

                parameters1.Add("@p_Id", DbType.Int32, direction: ParameterDirection.Output);
                parameters1.Add("p_UrunAdi", urun.UrunAdi);
                parameters1.Add("p_Fiyat", urun.Fiyat);
                parameters1.Add("p_StokMiktari", urun.StokMiktari);
                parameters1.Add("p_UrunKategorisiId", urun.UrunKategorisiId);
                parameters1.Add("p_Resim", urun.Resim);

                using (IDbConnection conn = new SqlConnection(connString))
                {
                    // check if urun already exist 
                    var doesUrunExist = conn.Execute($"SELECT U.Id FROM dbo.Uruns AS U WHERE U.UrunAdi = '{urun.UrunAdi}';");

                    if(doesUrunExist != -1)
                    {
                        ModelState.AddModelError("", "Hata oluştu !. Eklemek istediğin ürünü mevcut");

                        return View("New", viewModel);
                    }

                    // add new urun 
                    var rowsAffected = conn.Execute("dbo.sp_urunekle", parameters1, commandType: CommandType.StoredProcedure);

                    if (rowsAffected == 0)
                    {
                        ModelState.AddModelError("", "Hata oluştu !. ürün eklenmedi. Tekrar deneyin");

                        return View("New", viewModel);
                    }

                    else
                    {
                        int newId = parameters1.Get<int>("@p_Id");

                        // add urun to stok 
                        conn.Execute($"INSERT INTO dbo.Stoks (UrunId) VALUES({newId});");

                        string filename = "";

                        string path = Server.MapPath("~/Uploads/");

                        if (Request.Files.Count > 0)
                        {
                            HttpPostedFileBase postedFile = Request.Files["ImageData"];

                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            filename = String.Format($"{newId}-{Path.GetFileName(postedFile.FileName)}");

                            string fullPath = path + filename;

                            postedFile.SaveAs(fullPath);
                        }

                        else
                        {
                             filename = path + "default.png";
                        }

                        // update image resim path identity 
                        conn.Execute($"UPDATE dbo.Uruns AS U SET U.Resim = '{filename}' WHERE U.Id = {newId};");
                    }
                }
            }

            else
            {
                string filename = "";

                string path = Server.MapPath("~/Uploads/");

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase postedFile = Request.Files["ImageData"];

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filename = String.Format($"{urun.Id}-{Path.GetFileName(postedFile.FileName)}");

                    string fullPath = path + filename;

                    postedFile.SaveAs(fullPath);
                }

                else
                {
                    filename = urun.Resim;
                }

                var parameters = new DynamicParameters();

                string connString = Tools.GetConnectionString();

                parameters.Add("p_Id", urun.Id);
                parameters.Add("p_UrunAdi", urun.UrunAdi);
                parameters.Add("p_Fiyat", urun.Fiyat);
                parameters.Add("p_StokMiktari", urun.StokMiktari);
                parameters.Add("p_UrunKategorisiId", urun.UrunKategorisiId);
                parameters.Add("p_Resim", filename);

                using (IDbConnection conn = new SqlConnection(connString))
                {
                    var rowsAffected = conn.Execute("dbo.sp_updateurun", parameters, commandType: CommandType.StoredProcedure);

                    if(rowsAffected == 0)
                    {
                        var viewModel = new UrunFormViewModel(urun);

                        ModelState.AddModelError("", "Hata oluştu !. ürün güncellenmedi. Tekrar deneyin");

                        return View("New", viewModel);
                    }
                }
            }

            return RedirectToAction("Index", "Urunler");
        }

        /// <summary>
        /// Displays the update urun form with some populated data 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Urun urunData;

            UrunFormViewModel viewModel;

            var param = new DynamicParameters();

            string connString = Tools.GetConnectionString();

            using (IDbConnection conn = new SqlConnection(connString))
            {
                param.Add("p_Id", id);

                var data = conn.Query<Urun, UrunKategorisi, Urun>("dbo.sp_geturunbyId",
                    (urun, kategori) => { urun.UrunKategorisi = kategori; return urun; }, param,
                    splitOn: "UrunKategorisiId", commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (data == null)
                {
                    var newViewModel = new UrunFormViewModel();

                    ModelState.AddModelError("", "Ne yazık kı !! Bir hata oluştu ve ürün bulunamadı.");

                    return RedirectToAction("New", newViewModel);
                }

                urunData = new Urun
                {
                    Id = id,
                    UrunAdi = data.UrunAdi,
                    Fiyat = data.Fiyat,
                    StokMiktari = data.StokMiktari,
                    UrunKategorisi = data.UrunKategorisi,
                    UrunKategorisiId = data.UrunKategorisi.Id,
                    Resim = data.Resim
                };
            }

            _context = new ApplicationDbContext();

            viewModel = new UrunFormViewModel(urunData)
            {
                UrunKategorisi = _context.UrunKategorisis.ToList()
            };

            return View("New", viewModel); 
        }

        #region Helpers 

        #endregion
    }
}