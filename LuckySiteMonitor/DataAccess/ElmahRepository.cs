using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web.Configuration;
using Dapper;
using LuckySiteMonitor.Entities;

namespace LuckySiteMonitor.DataAccess {
    public class ElmahRepository {
        private readonly ElmahConfig _config;

        public ElmahRepository(ElmahConfig config) {
            _config = config;
        }

        private IDbConnection CreateConnection() {
            var conn = new SqlCeConnection(_config.ConnectionString);
            conn.Open();
            return conn;
        }

        private class Sql {
            public const string Test = "select top (1) ErrorId from ELMAH_Error where Application = @application or @application is null";
            public const string GetAll = "select * from Sites left outer join Elmah on Sites.Id = Elmah.SiteId";
            public const string GetById = GetAll + " where Sites.Id = @id";
            public const string Insert = "insert into Sites (Name, Description, CreatedOn, ModifiedOn) values (@Name, @Description, @CreatedOn, null)";
            public const string GetLast = "select * from Sites where Id = @@Identity";
            public const string Update = "update Sites set Name = @Name, Description = @Description, ModifiedOn = @ModifiedOn, IsActive = @IsActive where Id = @Id";
            public const string Delete = "delete from Sites where Id = @Id";
        }

        public enum TestResults {
            Success,
            Error,
            NoResults
        }

        public TestResults Test() {
            try {
                using (var conn = CreateConnection()) {
                    var result = conn.Query<ElmahLog>(Sql.Test, new { Application = string.IsNullOrWhiteSpace(_config.ApplicationFilter) ? null : _config.ApplicationFilter });
                    conn.Close();
                    return result.Any() ? TestResults.Success : TestResults.NoResults;
                }
            } catch {
                return TestResults.Error;
            }
        }

        public IEnumerable<Site> Get() {
            using (var conn = CreateConnection()) {
                //var result = conn.Query<Site, IEnumerable<Elmah>, Site>(Sql.GetAll, (site, elmah) => { site.Elmah = elmah; return site; });
                conn.Close();
                return null;
            }
        }

        public Site Get(int id) {
            using (var conn = CreateConnection()) {
                //var result = conn.Query<Site, IEnumerable<Elmah>, Site>(Sql.GetById, (site, elmah) => { site.Elmah = elmah; return site; }, new { id });
                conn.Close();
                return null;
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
