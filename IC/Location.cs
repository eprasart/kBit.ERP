using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using Dapper;
using kBit.ERP.SYS;
using kBit.ERP.SM;

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
        public string Insert_By { get; set; }
        public DateTime? Insert_At { get; set; }
        public string Change_By { get; set; }
        public DateTime? Change_At { get; set; }
    }

    static class LocationFacade
    {
        const string TableName = "ic_location";

        public static DataTable GetDataTable(string filter = "", string status = "")
        {
            var sql = "select id, code, description, name, phone, fax, email, address from " + TableName + "\nwhere 1 = 1";
            if (status.Length == 0)
                sql += " and status <> '" + Type.RecordStatus_Deleted + "'";
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
                m.Insert_By = App.session.Username;
                sql = SqlFacade.SqlInsert(TableName, "code, description, address, name, phone, fax, email, note, insert_by",
                    ":Code, :Description, :Address, :Name, :Phone, :Fax, :Email, :Note, :Insert_By", true);
                m.Id = SqlFacade.Connection.ExecuteScalar<long>(sql, m);
            }
            else
            {
                m.Change_By = App.session.Username;
                sql = SqlFacade.SqlUpdate(TableName, "code=:Code, description=:Description, address=:Address, name=:Name, phone=:Phone, fax=:Fax, email=:Email, " +
                    "note=:Note, change_by=:Change_By, change_at=now()", "id=:Id");
                SqlFacade.Connection.Execute(sql, m);
                ReleaseLock(m.Id);  // Unlock
            }
            return m.Id;
        }

        public static Location Select(long Id)
        {
            var sql = SqlFacade.SqlSelect(TableName, "*", "id=@Id");
            return SqlFacade.Connection.Query<Location>(sql, new { Id }).FirstOrDefault();
        }

        public static void SetStatus(long Id, string status)
        {
            var sql = SqlFacade.SqlUpdate(TableName , "status=:Status, change_by=:Change_By, change_at=now()", "id=:Id");
            SqlFacade.Connection.Execute(sql, new { Status = status, Change_By = App.session.Username, Id });
        }

        public static Lock GetLock(long Id)
        {
            return LockFacade.Select(TableName, Id);
        }

        public static void Lock(long Id, string code)
        {
            var m = new Lock { Table_Name = TableName, Lock_Id = Id, Ref=code };
            LockFacade.Save(m);
        }

        public static void ReleaseLock(long Id)
        {
            LockFacade.Delete(TableName, Id);
        }

        public static bool Exists(string Code, long Id = 0)
        {
            var sql = SqlFacade.SqlExists(TableName, "id<>:Id and status <> '" + Type.RecordStatus_Deleted + "' and code = :Code");
            return SqlFacade.Connection.ExecuteScalar<bool>(sql, new { Id, Code });
        }

        public static void Export()
        {
            string sql = SqlFacade.SqlSelect(TableName, "id \"Id\", code \"Code\", description \"Description\", address \"Address\", name \"Contact Name\", phone \"Phone\", fax \"Fax\", " +
                "email \"Email\", note \"Note\", status \"Status\", insert_by \"Inserted By\", insert_at \"Inserted At\", change_by \"Changed By\", change_at \"Changed At\"",
                "status <> '" + Type.RecordStatus_Deleted + "'", "code");
            SqlFacade.ExportToCSV(sql);
        }
    }
}