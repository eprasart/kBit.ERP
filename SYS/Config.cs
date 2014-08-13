using System;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using System.Linq;
using System.Data;
using Npgsql;

namespace kBit.ERP.SYS
{
    [Alias("SysConfig")]
    class Config
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
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

    static class ConfigFacade
    {
        public static DataTable GetDataTable(string filter = "", string status = "")
        {
            var sql = "select id, name, value, note from sys_config where 1 = 1";
            if (status.Length > 0)
                sql += " and status = '" + status + "'";
            if (filter.Length > 0)
                sql += " and (name ~* :filter or value ~* :filter)";
            sql += "\norder by name";
            var cmd = new NpgsqlCommand(sql, new NpgsqlConnection(Database.ConnectionString));
            if (filter.Length > 0)
                cmd.Parameters.AddWithValue(":filter", filter);
            var da = new NpgsqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static long Save(Config m)
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

                Database.Connection.UpdateOnly(m, p => new { p.Name, p.Value, p.Note, p.ChangeBy, p.ChangeAt }, p => p.Id == m.Id);
            }
            return m.Id;
        }

        public static Config Select(long Id)
        {
            return Database.Connection.SingleById<Config>(Id);
        }

        public static string Select(string name, string defaultValue = "")
        {
            return Database.Connection.Scalar<Config, string>(q => q.Name);
        }

        public static void SetStatus(long Id, string s)
        {
            DateTime? ts = Database.GetCurrentTimeStamp();
            Database.Connection.UpdateOnly(new Config { Status = s, ChangeBy = App.session.Username, ChangeAt = ts }, p => new { p.Status, p.ChangeBy, p.ChangeAt }, p => p.Id == Id);
        }

        public static bool IsExist(string Code, long Id = 0)
        {
            return Database.Connection.Exists<Config>("Id <> @Id and Name = @Name", new { Id = Id, Code = Code });
        }
    }
}