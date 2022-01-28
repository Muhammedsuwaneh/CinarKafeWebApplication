using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CinarKafe.Dtos;
using System.ComponentModel.DataAnnotations;
using CinarKafe.Db;
using CinarKafe.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace CinarKafe.Controllers.Api
{
    public class UrunlerController : ApiController
    {
        [HttpGet]
        [Route("api/Uruns")]
        public IHttpActionResult GetUruns(string query = null)
        {
            string connString = Tools.GetConnectionString();

            IEnumerable<Urun> urunler;

            using (IDbConnection conn = new SqlConnection(connString))
            {
                urunler = conn.Query<Urun, UrunKategorisi, Urun>("dbo.sp_urunlistele",
                    (urun, kategori) => { urun.UrunKategorisi = kategori; return urun; },
                    splitOn: "UrunKategorisiId", commandType: CommandType.StoredProcedure);

                if (urunler.Count() == 0) return BadRequest();
            }

            return Ok(urunler);
        }


        [HttpDelete]
        [Route("api/DeleteUrun/{id}")]
        [Authorize(Roles = RoleNames.CanManageApplication)]
        public IHttpActionResult DeleteUrun(int id)
        {
            string connString = Tools.GetConnectionString();

            var param = new DynamicParameters();

            param.Add("p_Id", id);

            using (IDbConnection conn = new SqlConnection(connString))
            {
                // check if ürün is on order list and not paid for 

                // delete ürün
                var rowsAffected = conn.Execute("dbo.sp_urunsil", param, commandType: CommandType.StoredProcedure);

                if (rowsAffected == 0) return BadRequest("Ürün silinemedi. Tekrar deneyin ya da sayfayı yenileyin");
            }


            return Ok();
        }

    }
}
