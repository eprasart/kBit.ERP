using System;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using System.Linq;
using System.Data;
using Npgsql;

namespace kPrasat.SM
{
    [Alias("SmSession")]
    public class Session
    {
        [AutoIncrement]
        public long Id { get; set; }        
        public string Username { get; set; }
        [Default(typeof(DateTime), "now()")]
        public DateTime? LoginAt { get; set; }
        public DateTime? LougoutAt { get; set; }
        public string Version { get; set; }
        public string MachineName { get; set; }
        public string MachinerUserName { get; set; }
        public String Status { get; set; }
    }

    [Alias("SmSessionLog")]
    class SessionLog
    {
        [AutoIncrement]
        public long Id { get; set; }
        [Default(typeof(DateTime), "now()")]
        public DateTime LogAt { get; set; }
        [References(typeof(Session))]
        public long SessionId { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        public string Module { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Message { get; set; }
    }

    static class SessionFacade
    {
        public static List<Session> Select(string filter = "")
        {
            SqlExpression<Session> e = OrmLiteConfig.DialectProvider.SqlExpression<Session>();
            e.Where(q => q.Username.Contains(filter) || q.MachineName.Contains(filter) || q.MachinerUserName.Contains(filter))
                .OrderBy(q => q.Username);
            //System.Windows.Forms.MessageBox.Show(e.SelectExpression + "\n" + e.WhereExpression);           
            return Database.Connection.Select<Session>(e);
        }

        public static DataTable GetDataTable(string filter = "", string status = "")
        {
            var sql = "select id, username, login_at, logout_at, version, machine_name, machine_user_name from sm_session where 1 = 1";
            if (status.Length > 0)
                sql += " and status = '" + status + "'";
            //if (filter.Length > 0)
            //    sql += " and (username ~* :filter or full_name ~* :filter or phone ~* :filter or email ~* :filter or note ~* :filter)";
            sql += "\norder by username";
            var cmd = new NpgsqlCommand(sql, new NpgsqlConnection(Database.ConnectionString));
            if (filter.Length > 0)
                cmd.Parameters.AddWithValue(":filter", filter);
            var da = new NpgsqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static long Save(Session m)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            long seq = 0;   // New inserted sequence
            if (m.Id == 0)
            {
                m.LoginAt = ts;
                seq = Database.Connection.Insert(m, true);
            }
            else
            {
                Database.Connection.UpdateOnly(m, p => new { p.Username }, p => p.Id == m.Id);
            }
            return seq;
        }

        public static Session Select(long Id)
        {
            return Database.Connection.SingleById<Session>(Id);
        }

        public static void SetStatus(long Id, string s)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new Session { Status = s }, p => p.Id == Id);
        }
    }
}