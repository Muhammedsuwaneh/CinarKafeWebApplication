using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CinarKafe.Models;
using CinarKafe.Dtos;
using CinarKafe.Db;
using System.Data.Entity;
using System.Data.SqlClient;
using Dapper;
using AutoMapper;
using System.Data;
using System.Net.Sockets;
using System.IO;
using System.Security.Authentication;

namespace CinarKafe.Controllers.Api
{
    public class MasalarController : ApiController
    {
        /// <summary>
        /// Returns an IEnumerable for masas from the database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = RoleNames.CanManageApplication)]
        [Route("api/Masas")]
        public IHttpActionResult GetMasas(string query = null)
        {
            string connString = Tools.GetConnectionString();

            IEnumerable<Masa> data;

            using (IDbConnection conn = new SqlConnection(connString))
            {
                data = conn.Query<Masa, MasaDurumu, Masa>("dbo.sp_masalistele", 
                    (masa, durum) => { masa.MasaDurumu = durum; return masa; }, 
                    splitOn: "MasaDurumuId", commandType: CommandType.StoredProcedure);
            }

            return Ok(data);
        }

        /// <summary>
        /// Deletes a masa from the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = RoleNames.CanManageApplication)]
        [Route("api/DeleteMasa/{id}")]
        public IHttpActionResult DeleteMasa(int id)
        {
            string connString = Tools.GetConnectionString();

            var param1 = new DynamicParameters();

            var param2 = new DynamicParameters();

            param1.Add("p_Id", id);

            param2.Add("p_Id", id);
            param2.Add("@ireturnvalue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (IDbConnection conn = new SqlConnection(connString))
            {
                // check if masa is busy before deleting
                conn.Execute("dbo.fn_masastate", param2, commandType: CommandType.StoredProcedure);

                var masaState = param2.Get<int>("@ireturnvalue");

                // if masa is busy it can't be deleted
                if (masaState == 2) return BadRequest("Hata oluştu ! Masa mesguldur ve silinmedi");

                var rowsAffected = conn.Execute("dbo.sp_masasil", param1, commandType: CommandType.StoredProcedure);

                if(rowsAffected == 0) return BadRequest("Hata oluştu !. Masa silinemedi. Tekrar deneyin");
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = RoleNames.CanManageApplication)]
        [Route("api/MakeSiperisPayment/{id}")]
        public IHttpActionResult MakeSiperisPayment(int id)
        {
            string connString = Tools.GetConnectionString();

            try
            {
                using (IDbConnection conn = new SqlConnection(connString))
                {
                    conn.Execute($"UPDATE dbo.Siperislers AS s1 SET s1.odenmis = 1 WHERE s1.Id = {id}");
                }
            }
            catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }
            catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }
            catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.RequestTimeout)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }
            catch (SocketException)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }
            catch (SqlException)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }
            catch (IOException)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }
            catch (AuthenticationException)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }
            catch(TimeoutException)
            {
                return BadRequest("Serve'da bir hata oluştu");
            }

            return Ok();
        }
    }
}