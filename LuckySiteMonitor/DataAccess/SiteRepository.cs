using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using Dapper;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.DataAccess {
    public class SiteRepository {
        
        private static IDbConnection CreateConnection() {
            var conn = new SqlCeConnection(WebConfigurationManager.ConnectionStrings["SiteMonitor"].ConnectionString);
            conn.Open();
            return conn;
        }

        public IEnumerable<Site> Get() {
            using (var conn = CreateConnection()) {
                var sites = conn.Query<Site>("select * from Sites");
                conn.Close();
                return sites;
            }
        }
    }
}
