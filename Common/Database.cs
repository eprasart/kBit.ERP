using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Windows.Forms;

namespace kPrasat
{
    class Database
    {
        public static string ConnectionString = "";
        public static OrmLiteConnectionFactory Factory = null;
        public static IDbConnection Connection = null;

        public static void PrepareDatabase()
        {
            //Connection.DropTables(typeof(SM.SessionLog), typeof(SM.Session));
            //Connection.DropTables(typeof(IC.Location));
            Connection.CreateTableIfNotExists(typeof(IC.Location), typeof(SM.User), typeof(SM.Session), typeof(SM.SessionLog), typeof(SYS.ErrorLog));
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

    class SQL
    {
        public static string BuildWhere(params string[] colNames)
        {
            string sWhere = "";
            //foreach (var col in colNames)
            //{
            //    sWhere+=col + " ~* :filter or "
            //}

            return sWhere;
        }
    }
}
