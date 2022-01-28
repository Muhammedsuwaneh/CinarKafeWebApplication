using CinarKafe.Db;
using CinarKafe.Models;
using CinarKafe.Models.ViewModels;
using Dapper;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Web.Http;

namespace CinarKafe.Controllers.Api
{
    public class HomeController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("api/InsertOrders")]
        public IHttpActionResult InsertOrders([FromBody]JObject SiperisListesi)
        {
            // get siperis data 
            var urunlerListesi = SiperisListesi["UrunListesi"].ToList();
            var masaId = SiperisListesi["MasaId"];
            int converted_masaId = Convert.ToInt32(masaId);

            string connString = Tools.GetConnectionString();
            var param1 = new DynamicParameters();
            var param2 = new DynamicParameters();

            using (IDbConnection conn = new SqlConnection(connString))
            {
                try
                {
                    foreach (var orderItem in urunlerListesi)
                    {
                        int UrunId = Convert.ToInt32(orderItem["UrunId"]);
                        int Adet = Convert.ToInt32(orderItem["Adet"]);
                        double ToplamFiyat = Convert.ToDouble(orderItem["ToplamFiyat"]);
                        double StokMiktari = Convert.ToInt32(orderItem["StokMiktari"]);
                        string[] tempTarihi = Convert.ToString(orderItem["Tarihi"]).Split('-');

                        DateTime Tarihi = new DateTime(Convert.ToInt32(tempTarihi[0]), Convert.ToInt32(tempTarihi[1]), Convert.ToInt32(tempTarihi[2]),
                            Convert.ToInt32(tempTarihi[3]), Convert.ToInt32(tempTarihi[4]), Convert.ToInt32(tempTarihi[5]));

                        param1.Add("@p_Id", DbType.Int32, direction: ParameterDirection.Output);
                        param1.Add("p_UrunId", UrunId);
                        param1.Add("p_Adet", Adet);
                        param1.Add("p_ToplamFiyat", ToplamFiyat);
                        param1.Add("p_Tarihi", Tarihi);

                        // insert siperis data
                        conn.Execute("dbo.sp_siperisekle", param1, commandType: CommandType.StoredProcedure);

                        var userId = User.Identity.GetUserId();

                        var newSiperisId = param1.Get<int>("@p_Id");

                        param2.Add("p_ApplicationUserId", userId);
                        param2.Add("p_SiperisId", newSiperisId);
                        param2.Add("p_MasaId", converted_masaId);
                        param2.Add("p_odenmis", false);

                        // insert siperisler data  
                        conn.Execute("dbo.sp_siperislerekle", param2, commandType: CommandType.StoredProcedure);

                        StokMiktari = StokMiktari - Adet;

                        // update product stok amount 
                        conn.Execute($"UPDATE dbo.Uruns AS u SET u.StokMiktari = {StokMiktari} WHERE u.Id = {UrunId}");

                    }

                    // update table 
                    conn.Execute($"UPDATE dbo.Masas AS m SET m.KacKisiOturuyor = m.KacKisiOturuyor + 1 WHERE m.Id = {converted_masaId}");

                    return Ok();
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }
                catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }
                catch (SocketException)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }
                catch (SqlException)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }
                catch (IOException)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }
                catch (AuthenticationException)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }
                catch (TimeoutException)
                {
                    return BadRequest("Server'da Bir hata oluştu :(");
                }

            }
        }
    }
}