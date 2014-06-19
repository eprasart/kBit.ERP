using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kPrasat
{
    class Database
    {
        public static string ConnectionString = ""; //Data Source=localhost:1521/xe;User ID=erp;Password=erp;Unicode=True";
        public static OrmLiteConnectionFactory Factory = null;
        public static IDbConnection Connection = null;

        public static void PrepareDatabase()
        {            
            Connection.CreateTableIfNotExists(typeof(IC.Location), typeof(SM.User), typeof(SM.Session), typeof(SM.SessionLog));
            //db.CreateTableIfNotExists<Vendor>();
            //db.CreateTableIfNotExists<Location>();

        }

        public static DateTime GetCurrentTimeStamp()
        {
            return Connection.Scalar<DateTime>("select now()");
        }

        public static string ExcuteString(string sql)
        {
            return Connection.Scalar<string>(sql);
        }
    }
}
