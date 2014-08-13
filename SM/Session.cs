using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using Dapper;

namespace kBit.ERP.SM
{
    public class Session
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public DateTime? LoginAt { get; set; }
        public DateTime? LogoutAt { get; set; }
        public string Version { get; set; }
        public string MachineName { get; set; }
        public string MachineUserName { get; set; }
        public String Status { get; set; }
    }

    class SessionLog
    {
        public long Id { get; set; }
        public DateTime? LogAt { get; set; }
        //[References(typeof(Session))]
        public long SessionId { get; set; }
        public string Priority { get; set; }
        public string Module { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }

    static class SessionFacade
    {
        public static DataTable GetDataTable(string filter = "", string status = "")
        {
            var sql = "select id, username, login_at, logout_at, version, machine_name, machine_user_name from sm_session where 1 = 1";
            if (status.Length > 0)
                sql += " and status = '" + status + "'";
            //if (filter.Length > 0)
            //    sql += " and (username ~* :filter or full_name ~* :filter or phone ~* :filter or email ~* :filter or note ~* :filter)";
            sql += "\norder by username";
            var cmd = new NpgsqlCommand(sql, SqlFacade.Connection);
            if (filter.Length > 0)
                cmd.Parameters.AddWithValue(":filter", filter);
            var da = new NpgsqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static long Save(Session m)
        {
            long seq = 0;   // New inserted sequence
            //if (m.Id == 0)
            //{
            //    m.LoginAt = ts;
            //    seq = SqlFacade.Connection.Insert(m, true);
            //}
            //else
            //{
            //    SqlFacade.Connection.UpdateOnly(m, p => new { p.Username }, p => p.Id == m.Id);
            //}
            return seq;
        }

        public static void UpdateLogout(Session m)
        {
            //DateTime? ts = SqlFacade.GetCurrentTimeStamp();
            //m.LogoutAt = ts;
            //SqlFacade.Connection.UpdateOnly(m, p => new { p.LogoutAt }, p => p.Id == m.Id);
        }

        ////public static Session Select(long Id)
        ////{
        ////    return SqlFacade.Connection<Session>(Id);
        ////}

        public static void SetStatus(long Id, string s)
        {
            ////DateTime? ts = SqlFacade.GetCurrentTimeStamp();
            ////SqlFacade.Connection.UpdateOnly(new Session { Status = s }, p => p.Id == Id);
        }
    }

    static class SessionLogFacade
    {
        ////      public static DataTable GetDataTable(string where, string filter = "")
        ////{
        ////    var sql = "select l.id, username, login_at, logout_at, version, machine_name, machine_user_name, log_at, priority, module, type, message\n" +
        ////        "from sm_session s\nleft join sm_session_log l on s.id = l.session_id where 1 = 1";
        ////    //if (status.Length > 0)
        ////    //    sql += " and status = '" + status + "'";
        ////    if (filter.Length > 0)
        ////        sql += " and (message ~* :filter or type ~* :filter or module ~* :filter)";
        ////    sql += "\norder by login_at desc, log_at desc";
        ////    var cmd = new NpgsqlCommand(sql, new NpgsqlConnection(SqlFacade.ConnectionString));
        ////    if (filter.Length > 0)
        ////        cmd.Parameters.AddWithValue(":filter", filter);
        ////    var da = new NpgsqlDataAdapter(cmd);
        ////    var dt = new DataTable();
        ////    da.Fill(dt);
        ////    return dt;
        ////}        

        private static void Save(SessionLog m)
        {            
            try
            {
                if (m.Id == 0)
                {
                    var sql = "insert into sm_session_log (log_at, session_id, priority, module, type, message)\n" +
                        "values (@LogAt, session_id=@SessionId, priority=@Priority, module=@Module, type=@Type)";
                    m.SessionId = SYS.App.session.Id;                    
                    SqlFacade.Connection.Execute(sql,m);
                }
                else
                {
                    //SqlFacade.Connection.UpdateOnly(m, p => new { p.Username }, p => p.Id == m.Id);
                }
            }
            catch (Exception ex)
            {
                SYS.ErrorLogFacade.LogToFile(ex);
            }
        }

        //public static SessionLog Select(long Id)
        //{
        //    return SqlFacade.Connection.SingleById<SessionLog>(Id);
        //}

        public static void SetStatus(long Id, string s)
        {
            
        }

        public static void Log(string priority, string module, string type, string message)
        {
            var log = new SessionLog()
            {
                Priority = priority,
                Module = module,
                Type = type,
                Message = message
            };
            Log(log);
        }

        public static void Log(SessionLog log)
        {
            Save(log);
        }
    }

    public class Lock
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public string Username { get; set; }
        public long LockId { get; set; }
        public DateTime? LockAt { get; set; }
    }
}