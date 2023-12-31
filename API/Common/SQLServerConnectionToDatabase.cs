﻿using CMS_Core.Entity;
using CMS_Core.Implementtion;
using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CMS_Core.Common
{
    public class SQLServerConnectionToDatabase
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="ConnectionSQL"></param>
       /// <param name="spName"></param>
       /// <param name="values"></param>
       /// <returns></returns>
        public DataTable ExecuteToDataTable1(string ConnectionSQL, string spName, params object[] values)
        {
            object value = DBNull.Value;

            ///Thực hiện store, values là các biến đưa vào stored theo thứ tự           
            if (string.IsNullOrEmpty(spName))
                throw new ArgumentException("Không đưa tên stored");

            List<INFORMATION_SCHEMA_PARAMETERS> dataPARAMETERS = null;
            //Lấy thông tin trường bảng trong mencache
           
            if (dataPARAMETERS == null)
            {
                if (HttpContext.Current.Session["INFORMATION_SCHEMA_PARAMETERS" + spName] != null)
                {
                    dataPARAMETERS = (List<INFORMATION_SCHEMA_PARAMETERS>)HttpContext.Current.Session["INFORMATION_SCHEMA_PARAMETERS" + spName];
                }
                else
                {
                    ImpINFORMATION_SCHEMA_PARAMETERS impINFORMATION_SCHEMA_PARAMETERS = new ImpINFORMATION_SCHEMA_PARAMETERS();
                    dataPARAMETERS = impINFORMATION_SCHEMA_PARAMETERS.GetINFORMATION_SCHEMA_PARAMETERSByProcedure(spName, ConnectionSQL);
                    HttpContext.Current.Session["INFORMATION_SCHEMA_PARAMETERS" + spName] = dataPARAMETERS;
                   
                }
            }

            string DATA_TYPE = string.Empty;
            string PARAMETER_NAME = string.Empty;

            try
            {
                var connection = new SqlConnection(ConnectionSQL);
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    if (values != null && values.Length > 0)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            parm.Add(dataPARAMETERS[i].PARAMETER_NAME, values[i]);
                        }
                    }
                    DataTable dt = new DataTable();
                    dt.Load(con.ExecuteReader(spName, parm, commandType: CommandType.StoredProcedure));

                    return dt;
                }
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="connectionSQL"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public DataTable ExecuteToDataTable2(string spName, string connectionSQL, params object[] values)
        {
            object value = DBNull.Value;

            ///Thực hiện store, values là các biến đưa vào stored theo thứ tự           
            if (string.IsNullOrEmpty(spName))
                throw new ArgumentException("Không đưa tên stored");

            List<INFORMATION_SCHEMA_PARAMETERS> dataPARAMETERS = null;
            //Lấy thông tin trường bảng trong mencache

            if (dataPARAMETERS == null)
            {
                if (HttpContext.Current.Session["INFORMATION_SCHEMA_PARAMETERS" + spName] != null)
                {
                    dataPARAMETERS = (List<INFORMATION_SCHEMA_PARAMETERS>)HttpContext.Current.Session["INFORMATION_SCHEMA_PARAMETERS" + spName];
                }
                else
                {
                    ImpINFORMATION_SCHEMA_PARAMETERS impINFORMATION_SCHEMA_PARAMETERS = new ImpINFORMATION_SCHEMA_PARAMETERS();
                    dataPARAMETERS = impINFORMATION_SCHEMA_PARAMETERS.GetINFORMATION_SCHEMA_PARAMETERSByProcedure(spName, connectionSQL);
                    HttpContext.Current.Session["INFORMATION_SCHEMA_PARAMETERS" + spName] = dataPARAMETERS;

                }
            }

            string DATA_TYPE = string.Empty;
            string PARAMETER_NAME = string.Empty;

            try
            {
                var connection = new SqlConnection(connectionSQL);
                using (var con = connection)
                {
                    var parm = new DynamicParameters();
                    if (values != null && values.Length > 0)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            parm.Add(dataPARAMETERS[i].PARAMETER_NAME, values[i]);
                        }
                    }
                    DataTable dt = new DataTable();
                    dt.Load(con.ExecuteReader(spName, parm, commandType: CommandType.StoredProcedure));

                    return dt;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public DataTable ExecuteToDataTable3(string ConnectionSQL, string SQL)
        {
            object value = DBNull.Value;

            ///Thực hiện store, values là các biến đưa vào stored theo thứ tự           
            if (string.IsNullOrEmpty(SQL))
                throw new ArgumentException("Không đưa tên có câu lệnh SQL");
            try
            {
                var connection = new SqlConnection(ConnectionSQL);
                using (var con = connection)
                {

                    DataTable dt = new DataTable();
                    dt.Load(con.ExecuteReader(SQL));

                    return dt;
                }
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionSQL"></param>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public DataTable ExecuteToDataTable4(string connectionSQL, string SQL)
        {
            object value = DBNull.Value;

            ///Thực hiện store, values là các biến đưa vào stored theo thứ tự           
            if (string.IsNullOrEmpty(SQL))
                throw new ArgumentException("Không đưa tên có câu lệnh SQL");
            try
            {
                var connection = new SqlConnection(connectionSQL);
                using (var con = connection)
                {

                    DataTable dt = new DataTable();
                    dt.Load(con.ExecuteReader(SQL));

                    return dt;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
