using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CourierAppAPI.Context
{
    class EOneContext : DbContext
    {
        public EOneContext()
            :base("eone")
        {
            Database.SetInitializer<EOneContext>(null);
        }

        public IEnumerable<T> ExecuteQuery<T>(string query, object [] param)
        {
            var dta = this.Database.SqlQuery<T>(query, param);
            return dta;
        }
    }
}