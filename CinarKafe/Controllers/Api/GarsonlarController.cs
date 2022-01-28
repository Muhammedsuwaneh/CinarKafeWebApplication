using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Data.SqlClient;
using AutoMapper;
using CinarKafe.Db;
using CinarKafe.Dtos;
using CinarKafe.Models;
using Dapper;

namespace CinarKafe.Controllers.Api
{
    public class GarsonlarController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = RoleNames.CanManageApplication)]
        [Route("api/Garsons")]
        // GET api/garsons
        public IHttpActionResult GetGarsons(string query = null)
        {
            string connString = Tools.GetConnectionString();

            IEnumerable<Garson> data;

            Garson garson = new Garson();

            using (IDbConnection conn = new SqlConnection(connString))
            {
                data = conn.Query<Garson>("dbo.sp_garsonlistele", garson, commandType: CommandType.StoredProcedure);
            }

            return Ok(data);
        }

        // DELETE api/DeleteGarson/id
        [HttpDelete]
        [Authorize(Roles = RoleNames.CanManageApplication)]
        [Route("api/DeleteGarson/{id}")]
        public IHttpActionResult DeleteGarson(int id)
        {
            string connString = Tools.GetConnectionString();

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (IDbConnection conn = new SqlConnection(connString))
            {
                var rowsAffected = conn.Execute("dbo.sp_garsonsil", parameters, commandType: CommandType.StoredProcedure);

                if (rowsAffected == 0) return NotFound();
            }

            return Ok();
        }
    }
}
