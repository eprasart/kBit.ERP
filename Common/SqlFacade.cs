using System;
using System.Data;
using System.Windows.Forms;
using kBit.ERP;
using Npgsql;
using Dapper;
using System.Text;
using System.IO;

namespace kBit.ERP
{
    class SqlFacade
    {
        public static string ConnectionString = "";
        public static NpgsqlConnection Connection = null;

        public static void EnsureDBSetup()
        {
            var ScriptPath = Path.Combine(Application.StartupPath, "script_init.sql");
            var sql = Util.ReadTextFile(ScriptPath);
            if (sql.Length == 0) return;
            SqlFacade.Connection.Execute(sql);
        }

        public static void OpenConnection()
        {
            //var connection = new NpgsqlConnection(ConnectionString);
            //connection.Open();
            //return connection;            
            Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();
        }

        public static DateTime GetCurrentTimeStamp()
        {                        
            return Connection.ExecuteScalar<DateTime>("select now()");
        }

        public static string ExecuteString(string sql)
        {
            return Connection.ExecuteScalar<string>(sql);
        }

        public static DataTable GetDataTable(string sql)
        {
            return GetDataTable(new NpgsqlCommand(sql));
        }

        public static DataTable GetDataTable(NpgsqlCommand cmd)
        {
            cmd.Connection = new NpgsqlConnection(SqlFacade.ConnectionString);
            var da = new NpgsqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static NpgsqlDataReader GetDataReader(string sql)
        {
            return GetDataReader(new NpgsqlCommand(sql));
        }

        public static NpgsqlDataReader GetDataReader(NpgsqlCommand cmd)
        {
            cmd.Connection = new NpgsqlConnection(SqlFacade.ConnectionString);
            cmd.Connection.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static int ExportToCSV(string sql, string delimiter = ",", bool open = true)
        {
            //sql = "COPY (" + sql + ") TO '" + path + "' DELIMITER '" + delimiter + "' CSV HEADER ENCODING 'UTF8';";
            //Connection.ExecuteNonQuery(sql);

            //todo: delimiter and open var should be from table
            var result = 0;

            var sfd = new SaveFileDialog
            {
                CheckPathExists = true,
                OverwritePrompt = true,
                Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*",
                Title = "Export to CSV"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return result;
            var path = sfd.FileName;

            Application.DoEvents();
            StringBuilder sb = new StringBuilder();
            var dr = GetDataReader(sql);
            var sLine = "";
            if (!dr.HasRows) return 1;  // No record
            for (int i = 0; i < dr.FieldCount; i++) // Column headers
            {
                sLine += dr.GetName(i);
                if (i < dr.FieldCount - 1) sLine += delimiter;
            }
            sb.AppendLine(sLine);

            while (dr.Read())   // Rows
            {
                sLine = "";
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    sLine += "\"" + dr[i].ToString() + "\"";
                    if (i < dr.FieldCount - 1) sLine += delimiter;
                }
                sb.AppendLine(sLine);
            }
            dr.Close();

            while (Util.IsFileLocked(path))   // Check if file is being used
            {
                if (MessageBox.Show("'" + Path.GetFileName(path) + "' is being used by another process.\nPlease close it and try again.", "Export", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return 0;
                continue;
            }
            using (var sw = new StreamWriter(path, false, Encoding.UTF8))   // Write to file  
            {
                sw.Write(sb);
            }
            if (open) System.Diagnostics.Process.Start(path);   // Open file
            return result;
        }

        public static string SqlSelect(string table, string columns, string where = "", string orderby = "")
        {
            var sql = "select " + columns + " from " + table;
            if (where.Length > 0) sql += "\nwhere " + where;
            if (orderby.Length > 0) sql += "\norder by " + orderby;
            return sql;
        }

        public static string SqlExists(string table, string where)
        {
            var sql = SqlSelect(table, "1", where);
            sql = "select exists(" + sql + ")";
            return sql;
        }

        public static string SqlInsert(string table, string columns, string values, bool returnSeq = false)
        {
            var sql = string.Format("insert into {0} ({1})\nvalues ({2})", table, columns, values);
            if (returnSeq) sql += "\nreturning id";
            return sql;
        }

        public static string SqlUpdate(string table, string sets, string where = "")
        {
            var sql = "update " + table + " set " + sets;
            if (where.Length > 0) sql += "\nwhere " + where;
            return sql;
        }
    }
}
