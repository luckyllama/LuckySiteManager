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

        private class Sql {
            public const string GetAll = "select * from Sites left outer join Elmah on Sites.Id = Elmah.SiteId";
            public const string GetById = GetAll + " where Sites.Id = @id";
            public const string Insert = "insert into Sites (Name, Description, CreatedOn, ModifiedOn) values (@Name, @Description, @CreatedOn, null)";
            public const string GetLast = "select * from Sites where Id = @@Identity";
            public const string Update = "update Sites set Name = @Name, Description = @Description, ModifiedOn = @ModifiedOn, IsActive = @IsActive where Id = @Id";
            public const string Delete = "delete from Sites where Id = @Id";
        }

        public IEnumerable<Site> Get() {
            using (var conn = CreateConnection()) {
                var sites = conn.Query<Site>(Sql.GetAll);
                conn.Close();
                return sites;
            }
        }

        public Site Get(int id) {
            using (var conn = CreateConnection()) {
                var site = conn.Query<Site>(Sql.GetById, new { id });
                conn.Close();
                return site.FirstOrDefault();
            }
        }

        public int Insert(Site site) {
            using (var conn = CreateConnection()) {
                site.CreatedOn = DateTime.Now;
                conn.Execute(Sql.Insert, site);
                var result = conn.Query<Site>(Sql.GetLast);
                conn.Close();
                return result.First().Id;
            }
        }

        public void Update(Site site) {
            using (var conn = CreateConnection()) {
                site.ModifiedOn = DateTime.Now;
                conn.Execute(Sql.Update, site);
                conn.Close();
            }
        }

        public void Delete(int id) {
            using (var conn = CreateConnection()) {
                conn.Execute(Sql.Delete, new { id });
                conn.Close();
            }
        }
    }
}
