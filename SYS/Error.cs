using System;
using System.Data;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using Npgsql;

namespace kPrasat.SYS
{
    [Alias("SysErrorLog")]
    class ErrorLog
    {
        [AutoIncrement]
        public long Id { get; set; }
        //[References(typeof(Session))]
        public long SessionId { get; set; }
        [Default(typeof(DateTime), "now()")]
        public DateTime? At { get; set; }        
        public string Message { get; set; }
        public string Trace { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }
    }

    class ErrorLogFacade
    {
        public static DataTable GetDataTable(string where, string filter = "")
        {
            var sql = "select l.id, username, login_at, logout_at, version, machine_name, machine_user_name, log_at, priority, module, type, message\n" +
                "from sm_session s\nleft join sm_session_log l on s.id = l.session_id where 1 = 1";
            //if (status.Length > 0)
            //    sql += " and status = '" + status + "'";
            if (filter.Length > 0)
                sql += " and (message ~* :filter or type ~* :filter or module ~* :filter)";
            sql += "\norder by login_at desc, log_at desc";
            var cmd = new NpgsqlCommand(sql, new NpgsqlConnection(Database.ConnectionString));
            if (filter.Length > 0)
                cmd.Parameters.AddWithValue(":filter", filter);
            var da = new NpgsqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private static void Save(ErrorLog m)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            if (m.Id == 0)
            {
                m.SessionId = App.session.Id;
                m.At = ts;
                //todo: if failed > log error > log to file
                Database.Connection.Insert(m);
            }
            else
            {
                //Database.Connection.UpdateOnly(m, p => new { p.Username }, p => p.Id == m.Id);
            }
        }

        public static ErrorLog Select(long Id)
        {
            return Database.Connection.SingleById<ErrorLog>(Id);
        }

        public static void SetStatus(long Id, string s)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new ErrorLog { Status = s }, p => p.Id == Id);
        }

        public static void Log(string message, string trace, string info = "")
        {
            var log = new ErrorLog()
            {                
                Message = message,
                Trace = trace,
                Info = info
            };
            Log(log);
        }

        public static void Log(ErrorLog log)
        {
            Save(log);
        }
    }
}
