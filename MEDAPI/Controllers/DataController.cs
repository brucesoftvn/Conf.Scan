using CMS_Core.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
namespace MEDAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class DataController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mavach"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Data/API_THUOC")]
        public Models.Thuoc API_THUOC(Int64 mavach)
        {

            SQLServerConnection<Models.Thuoc> sQLServer1 = new SQLServerConnection<Models.Thuoc>();
            List<Models.Thuoc> lstDM = sQLServer1.SelectQueryCommand2(Common.GetConnPOS(), "API_THUOC", mavach);
            if (lstDM.Count > 0)
                return lstDM[0];
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Data/API_INSERT_KK")]
        public string API_INSERT_KK([FromBody] JObject jsonData)
        {

            string SP = "API_INSERT_KK";
            Models.KiemKe obj = jsonData.ToObject<Models.KiemKe>();
            //Models.CLS_ADichVu obj = new Models.CLS_ADichVu();
            SQLServerConnection<Models.KiemKe> sQLServer1 = new SQLServerConnection<Models.KiemKe>();
            string result = sQLServer1.ExecuteInsert(Common.GetConnPOS(), SP
            , obj.Mavach
            , obj.Mavt
            , obj.Soluong
            , obj.Thang
            , obj.Nam
            , obj.Ghichu
            , obj.Khole
            , obj.Ngaytao
            , obj.Nguoitao
            );
            string str = "{'result': '" + result + "'}";
            return JsonConvert.SerializeObject(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="thang"></param>
        /// <param name="nam"></param>
        /// <param name="makho"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Data/API_GETKIEMKE")]
        public List<Models.KiemKe> API_GETKIEMKE(string thang, string nam, string makho)
        {
            SQLServerConnection<Models.KiemKe> sQLServer1 = new SQLServerConnection<Models.KiemKe>();
            List<Models.KiemKe> lstDM = sQLServer1.SelectQueryCommand2(Common.GetConnPOS(), "API_GETKIEMKE", thang, nam, makho);
            if (lstDM.Count > 0)
                return lstDM;
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="makho"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Data/API_Login")]
        public List<Models.User> API_Login(string username, string password)
        {
            SQLServerConnection<Models.User> sQLServer1 = new SQLServerConnection<Models.User>();
            List<Models.User> lstDM = sQLServer1.SelectQueryCommand2(Common.GetConnPOS(), "SP_LOGIN_NEW_2", username, password);
            if (lstDM.Count > 0)
                return lstDM;
            else
                return null;
        }
    }
}
