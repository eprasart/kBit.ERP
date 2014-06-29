/* TODO
 * Pwd encrypt
 * Reset pwd
 * 
*/
using System;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using System.Linq;
using System.Data;
using Npgsql;
using kPrasat.SYS;

namespace kPrasat.IC
{
    [Alias("IcLocation")]
    class Location
    {
        [AutoIncrement]
        public long Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public String Status { get; set; }
        public string LockBy { get; set; }
        public DateTime? LockAt { get; set; }
        public string InsertBy { get; set; }
        [Default(typeof(DateTime), "now()")]
        public DateTime? InsertAt { get; set; }
        public string ChangeBy { get; set; }
        public DateTime? ChangeAt { get; set; }
    }

    static class LocationFacade
    {
        public static List<Location> Select(string filter = "")
        {
            SqlExpression<Location> e = OrmLiteConfig.DialectProvider.SqlExpression<Location>();
            e.Where(q => q.Status == Type.RecordStatus_Active && (q.Code.Contains(filter) || q.Description.Contains(filter)))
                .OrderBy(q => q.Code);
            return Database.Connection.Select<Location>(e);
        }

        public static DataTable GetDataTable(string filter = "", string status = "")
        {
            var sql = "select id, code, description, name, phone, fax, email, address from ic_location where 1 = 1";
            if (status.Length > 0)
                sql += " and status = '" + status + "'";
            if (filter.Length > 0)
                sql += " and (code ~* :filter or description ~* :filter or phone ~* :filter or fax ~* :filter or email ~* :filter or address ~* :filter or note ~* :filter)";
            sql += "\norder by code";
            var cmd = new NpgsqlCommand(sql, new NpgsqlConnection(Database.ConnectionString));
            if (filter.Length > 0)
                cmd.Parameters.AddWithValue(":filter", filter);
            var da = new NpgsqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static long Save(Location m)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            if (m.Id == 0)
            {
                m.Status = Type.RecordStatus_Active;
                m.InsertBy = App.session.Username;
                m.InsertAt = ts;
                m.Id = Database.Connection.Insert(m, true); // New inserted sequence
            }
            else
            {
                m.ChangeBy = App.session.Username;
                m.ChangeAt = ts;

                Database.Connection.UpdateOnly(m, p => new { p.Code, p.Description, p.Address, p.Name, p.Phone, p.Fax, p.Email, p.Note, p.ChangeBy, p.ChangeAt },
                    p => p.Id == m.Id);
                if (IsLocked(m.Id)) ReleaseLock(m.Id);  // If record is locked then unlock
            }
            return m.Id;
        }

        public static Location Select(long Id)
        {
            return Database.Connection.SingleById<Location>(Id);
        }

        public static void SetStatus(long Id, string s)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new Location { Status = s, ChangeBy = App.session.Username, ChangeAt = ts }, p => new { p.Status, p.ChangeBy, p.ChangeAt }, p => p.Id == Id);
        }

        public static bool IsLocked(long Id)
        {
            return Database.Connection.Exists<Location>("Id = @Id and Lock_By = @LockBy", new { Id = Id, LockBy = App.session.Username });
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
            Database.Connection.UpdateOnly(new Location { LockBy = App.session.Username, LockAt = ts }, p => new { p.LockBy, p.LockAt }, p => p.Id == Id);
        }

        public static void ReleaseLock(long Id)
        {
            if (Id == 0) return;
            DateTime ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new Location { LockBy = null }, p => p.LockBy, p => p.Id == Id);
        }

        public static bool IsExist(string Code, long Id = 0)
        {
            return Database.Connection.Exists<Location>("Id <> @Id and Code = @Code", new { Id = Id, Code = Code });
        }
    }
}