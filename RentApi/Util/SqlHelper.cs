using CPTech.CustomORM.Mapping;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CPTech.CustomORM.Dal
{
    /// <summary>
    /// 数据库访问帮助类
    /// </summary>
    public class SqlHelper
    {
        private readonly IConfiguration configuration;

        public SqlHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DataTable ExcuteSql(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }

            return dt;
        }

        public ICollection<T> ExcuteSql<T>(string sql) where T : new()
        {
            Type type = typeof(T);
            ICollection<T> dt = new List<T>();

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    T t = new T();
                    foreach (var prop in type.GetProperties())
                    {
                        string propName = prop.GetMappingName();
                        prop.SetValue(t, dr[propName] is DBNull ? null : dr[propName]);
                    }
                    dt.Add(t);
                }
                dr.Close();
            }

            return dt;
        }

        public int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
