using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using Dapper;
using kBit.ERP.SYS;

namespace kBit.ERP.IC
{
    class Location
    {
        public long Id { get; set; }
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
        public DateTime? InsertAt { get; set; }
        public string ChangeBy { get; set; }
        public DateTime? ChangeAt { get; set; }
    }

    static class LocationFacade
    {
        public static DataTable GetDataTable(string filter = "", string status = "")
        {
            var sql = "select id, code, description, name, phone, fax, email, address from ic_location where 1 = 1";
            if (status.Length == 0)
                sql += " and status <> 'X'";
            else
                sql += " and status = '" + status + "'";
            if (filter.Length > 0)
                sql += " and (code ilike :filter or description ilike :filter or phone ilike :filter or fax ilike :filter or email ilike :filter or address ilike :filter or note ilike :filter)";
            sql += "\norder by code\nlimit 1000";   // todo: in db

            var cmd = new NpgsqlCommand(sql);
            if (filter.Length > 0)
                cmd.Parameters.AddWithValue(":filter", "%" + filter + "%");

            return SqlFacade.GetDataTable(cmd);
        }

        public static long Save(Location m)
        {
            string sql = "";
            if (m.Id == 0)
            {
                m.Status = Type.RecordStatus_Active;
                ////m.insert_by = App.session.Username;
                sql = "insert into ic_location (code, description, address, name, phone, fax, email, note, insert_by)\n" +
                    "values (@code, @description, @address, @name, @phone, @fax, @email, @note, @insertby) returning id";
                m.Id = SqlFacade.Connection.Query<long>(sql, m).Single(); // New inserted sequence                    
            }
            else
            {
                ////m.change_by = App.session.Username;
                ////m.change_at = ts;
                sql = "update ic_location set code=@code, description=@description, address=@address, name=@name, phone=@phone, fax=@fax, email=@email, " +
                    "note=@note, change_by=@changeby, change_at=now() where id=@id";
                SqlFacade.Connection.Execute(sql, m);
                ////if (IsLocked(m.id)) ReleaseLock(m.id);  // If record is locked then unlock
            }
            return m.Id;
        }

        public static Location Select(long Id)
        {
            return SqlFacade.Connection.Query<Location>("select * from ic_location where id=@id", new { id = Id }).Single();
        }

        public static void SetStatus(long Id, string s)
        {
            var sql = "update ic_location set status=@status, change_by=@changeby, change_at=now() where id=@id";
            SqlFacade.Connection.Execute(sql, new { Status = s, ChangeBy = App.session.Username, id = Id });
        }

        public static bool IsLocked(long Id)
        {
            return SqlFacade.Connection.Exists<Location>("Id = @Id and Lock_By = @LockBy", new { Id = Id, LockBy = App.session.Username });
        }

        public static LockInfo GetLockInfo(long Id)
        {
            var m = Select(Id);
            var l = new LockInfo();
            l.Id = Id;
            l.LockBy = m.lock_by;
            l.LockAt = m.lock_at;
            return l;
        }

        public static void Lock(long Id)
        {
            DateTime ts = SqlFacade.GetCurrentTimeStamp();
            SqlFacade.Connection.UpdateOnly(new Location { lock_by = App.session.Username, lock_at = ts }, p => new { p.LockBy, p.LockAt }, p => p.Id == Id);
        }

        public static void ReleaseLock(long Id)
        {
            if (Id == 0) return;
            DateTime ts = SqlFacade.GetCurrentTimeStamp();
            SqlFacade.Connection.UpdateOnly(new Location { lock_by = null }, p => p.LockBy, p => p.Id == Id);
        }

        public static bool IsExist(string Code, long Id = 0)
        {
            return false; ////return SqlFacade.Connection.Exists<Location>("Id <> @Id and Status <> 'X' and Code = @Code", new { Id = Id, Code = Code });  // Also check in 'Inactive', except 'X' (Deleted)
        }

        public static void Export()
        {
            string sql = "select id \"Id\", code \"Code\", description \"Description\", address \"Address\", name \"Contact Name\", phone \"Phone\", fax \"Fax\"," +
                "email \"Email\", note \"Note\", status \"Status\", insert_by \"Inserted By\", insert_at \"Inserted At\", change_by \"Changed By\", change_at \"Changed At\"\n" +
                "from ic_location\nwhere status <> 'X'\norder by code";
            SqlFacade.ExportToCSV(sql);
        }
    }
}