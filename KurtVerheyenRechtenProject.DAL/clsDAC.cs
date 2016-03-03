using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KurtVerheyenRechtenProject.DAL
{
    public class clsDAC
    {
        private static bool _OKNOK;
        public static bool OK_NOK
        {
            get { return _OKNOK; }
            set { _OKNOK = value; }
        }
        private static string _MyConnectionString = Properties.Settings.Default.CN.ToString();
        public static string MyConnectionString
        {
            get { return _MyConnectionString; }
        }

        public static DataTable ExecuteDataTable(string storedProcedureName)
        {
            DataTable dt = new DataTable();
            string ControlValue = string.Empty;
            string MyConnection = string.Empty;
            using (SqlConnection cnn = new SqlConnection(_MyConnectionString))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 0;
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                try
                {
                    dt = new DataTable();
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return dt;
        }
        public static DataTable ExecuteDataTable(string storedProcedureName, ref int nr, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();
            string ControlValue = string.Empty;
            string MyConnection = string.Empty;
            using (SqlConnection cnn = new SqlConnection(_MyConnectionString))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 0;
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                if (arrParam != null)
                {
                    foreach (SqlParameter param in arrParam)
                    {
                        cmd.Parameters.Add(param);
                        if (param.ParameterName == "@ReturnValue")
                        {
                            ControlValue = "ok";
                            cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                        }
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                int ReturnValue = 0;
                try
                {
                    dt = new DataTable();
                    da.Fill(dt);
                    if (ControlValue == "ok")
                    {
                        ReturnValue = (int)cmd.Parameters["@ReturnValue"].Value;
                        nr = ReturnValue;
                    }
                }
                catch (SqlException ex)
                {
                    ReturnValue = 996;
                    nr = ReturnValue;
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    ReturnValue = 9999;
                    nr = ReturnValue;
                    throw new Exception(ex.Message);
                }

            }
            return dt;
        }
        public static SqlParameter Parameter(string parameterName, object parameterValue)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.Value = parameterValue;
            param.IsNullable = true;
            return param;
        }
    }
}
