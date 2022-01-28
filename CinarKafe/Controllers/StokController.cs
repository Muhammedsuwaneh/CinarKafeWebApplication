using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using CinarKafe.Models;
using CinarKafe.Db;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace CinarKafe.Controllers
{
    [Authorize(Roles = RoleNames.CanManageApplication)]
    public class StokController : Controller
    {
        // GET: Stok
        public ActionResult Index()
        {
            string connString = Tools.GetConnectionString();

            IEnumerable<Stok> data;

            using (IDbConnection conn = new SqlConnection(connString))
            {
                data = conn.Query<Stok, Urun, UrunKategorisi, Stok>("dbo.sp_stoklistele", 
                    (stok, urun, kategori) => { stok.Urun = urun; urun.UrunKategorisi = kategori; return stok; }, 
                    commandType: CommandType.StoredProcedure);
            }

            return View(data);
        }
    }
}