using System;
using System.Data;
using System.IO;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using Npgsql;
using System.Windows.Forms;

namespace kBit.ERP.SYS
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
        private static string LogFileName = Path.Combine(Application.StartupPath, "Error.log");

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
            try
            {
                if (m.Id == 0)
                {
                    m.SessionId = App.session.Id;
                    m.At = ts;
                    Database.Connection.Insert(m);
                }
                else
                {
                    //Database.Connection.UpdateOnly(m, p => new { p.Username }, p => p.Id == m.Id);
                }
            }
            catch (Exception ex)
            {
                LogToFile(ex);
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

        /// <summary>
        /// Log error to database
        /// </summary>
        /// <param name="ex">Exception variable</param>
        /// <param name="info">Extra information</param>
        public static void Log(Exception ex, string info = "")
        {
            var log = new ErrorLog()
            {
                Message = ex.ToString(),
                Trace = ex.StackTrace,
                Info = info
            };
            Log(log);
        }

        public static void Log(ErrorLog log)
        {
            Save(log);
        }

        public static void LogToFile(Exception ex, string info = "")
        {
            string log = DateTime.Now.ToString("yyy-MM-dd ddd hh:mm:ss tt");
            log += "\n" + ex.ToString() + "\n" + ex.StackTrace;
            if (info.Length > 0) log += "\n" + info;
            try
            {
                using (StreamWriter sr = new StreamWriter(LogFileName, true))
                {
                    sr.WriteLine(log);
                }
            }
            catch { }
        }

        /// <summary>
        /// Maintain log file. If file size > 3M then rename it. 
        /// </summary>
        public static void MaintainLogFile()
        {
            double fSize = (new FileInfo(LogFileName).Length / 1024f) / 1024f;
            if (fSize > 3)
            {
                string sFile = Path.Combine(Application.StartupPath, "Error" + DateTime.Today.ToString("_yyMMdd") + ".log");
                File.Move(LogFileName, sFile);
            }
        }
    }
}