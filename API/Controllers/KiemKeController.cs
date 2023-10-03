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

namespace API.Controllers
{
    public class KiemKeController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mavach"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("api/[controller]/API_THUOC")]
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
        /// <param name="mavach"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Mvc.Route("api/[controller]/API_THUOC2")]
        public Models.Thuoc API_THUOC2(Int64 mavach)
        {

            SQLServerConnection<Models.Thuoc> sQLServer1 = new SQLServerConnection<Models.Thuoc>();
            List<Models.Thuoc> lstDM = sQLServer1.SelectQueryCommand2(Common.GetConnPOS(), "API_THUOC", mavach);
            if (lstDM.Count > 0)
                return lstDM[0];
            else
                return null;
        }
    }
}
