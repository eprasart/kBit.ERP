using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Windows.Forms;
using kBit.ERP;

namespace kBit.ERP
{
    class Database
    {
        public static string ConnectionString = "";
        public static OrmLiteConnectionFactory Factory = null;
        public static IDbConnection Connection = null;

        public static void PrepareDatabase()
        {
            //Connection.DropTables(typeof(SM.SessionLog), typeof(SM.Session));
            Connection.CreateTableIfNotExists(typeof(IC.Location), typeof(SM.Session), typeof(SM.SessionLog), typeof(SYS.ErrorLog));
            Connection.CreateTableIfNotExists(typeof(SM.User), typeof(SM.Role), typeof(SM.UserRole), typeof(SM.Function), typeof(SM.UserFunction),
                typeof(SM.RoleFunction));

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
