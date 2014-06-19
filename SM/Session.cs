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
    class Session
    {
        [AutoIncrement]
        public long Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Default(typeof(DateTime), "now()")]
        public DateTime LoginAt { get; set; }
        public DateTime? LougoutAt { get; set; }
        public string Version { get; set; }
        public string ComputerName { get; set; }
        public string ComputerUserName { get; set; }
        public String Status { get; set; }
        public string LockBy { get; set; }
        public DateTime? LockAt { get; set; }
        public string InsertBy { get; set; }
        [Default(typeof(DateTime), "now()")]
        public DateTime? InsertAt { get; set; }
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
        public static List<User> Select(string filter = "")
        {
            SqlExpression<User> e = OrmLiteConfig.DialectProvider.SqlExpression<User>();
            e.Where(q => q.Status == StatusType.Active && (q.Username.Contains(filter) || q.FullName.Contains(filter)))
                .OrderBy(q => q.Username);
            //System.Windows.Forms.MessageBox.Show(e.SelectExpression + "\n" + e.WhereExpression);           
            return Database.Connection.Select<User>(e);
        }

        public static DataTable GetDataTable(string filter = "", string status = "")
        {
            var sql = "select id, username, full_name, phone, email from sm_user where 1 = 1";
            if (status.Length > 0)
                sql += " and status = '" + status + "'";
            if (filter.Length > 0)
                sql += " and (username ~* :filter or full_name ~* :filter or phone ~* :filter or email ~* :filter or note ~* :filter)";
            sql += "\norder by username";
            var cmd = new NpgsqlCommand(sql, new NpgsqlConnection(Database.ConnectionString));
            if (filter.Length > 0)
                cmd.Parameters.AddWithValue(":filter", filter);
            var da = new NpgsqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static long Save(User m)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            long seq = 0;   // New inserted sequence
            if (m.Id == 0)
            {
                m.Status = StatusType.Active;
                m.InsertBy = Login.Username;
                m.InsertAt = ts;
                seq = Database.Connection.Insert(m, true);
            }
            else
            {
                m.ChangeBy = Login.Username;
                m.ChangeAt = ts;

                Database.Connection.UpdateOnly(m, p => new { p.Username, p.FullName, p.Phone, p.Email, p.Note, ChangeBy = p.ChangeBy, ChangeAt = p.ChangeAt },
                    p => p.Id == m.Id);
                // If record is lock then unlock
                if (IsLocked(m.Id)) ReleaseLock(m.Id);
            }
            return seq;
        }

        public static User Select(long Id)
        {
            return Database.Connection.SingleById<User>(Id);
        }

        public static void SetStatus(long Id, string s)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new User { Status = s, ChangeBy = Login.Username, ChangeAt = ts }, p => new { p.Status, p.ChangeBy, p.ChangeAt }, p => p.Id == Id);
        }

        public static bool IsLocked(long Id)
        {
            return Database.Connection.Exists<User>("Id = @Id and Lock_By = @LockBy", new { Id = Id, LockBy = Login.Username });
        }

        public static LockInfo GetLockInfo(long Id)
        {
            var m = Select(Id);
            var l = new LockInfo();
            l.Id = Id;
            l.LockBy = m.LockBy;
            l.LockAt = m.LockAt;
            return l;
        }

        public static void Lock(long Id)
        {
            DateTime ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new User { LockBy = Login.Username, LockAt = ts }, p => new { p.LockBy, p.LockAt }, p => p.Id == Id);
        }

        public static void ReleaseLock(long Id)
        {
            if (Id == 0) return;
            DateTime ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new User { LockBy = null }, p => p.LockBy, p => p.Id == Id);
        }

        public static bool IsExist(string Username, long Id = 0)
        {
            return Database.Connection.Exists<User>("Id <> @Id and Username = @Username", new { Id = Id, Username = Username });
        }
    }
}