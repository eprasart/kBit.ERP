using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Windows.Forms;
using kBit.ERP;
using Npgsql;
using System.Text;
using System.IO;

namespace kBit.ERP
{
    class Database
    {
        public static string ConnectionString = "";
        public static OrmLiteConnectionFactory Factory = null;
        public static IDbConnection Connection = null;

        public static void PrepareDatabase()
        {
            //Connection.DropTables(typeof(SM.SessionLog), typeof(SM.Session));
            Connection.CreateTableIfNotExists(typeof(IC.Location), typeof(SM.Session), typeof(SM.SessionLog), typeof(SYS.ErrorLog));
            Connection.CreateTableIfNotExists(typeof(SM.User), typeof(SM.Role), typeof(SM.UserRole), typeof(SM.Function), typeof(SM.UserFunction),
                typeof(SM.RoleFunction));
        }

        public static DateTime GetCurrentTimeStamp()
        {
            return Connection.Scalar<DateTime>("select now()");
        }

        public static string ExcuteString(string sql)
        {
            return Connection.Scalar<string>(sql);
        }

        public static DataTable GetDataTable(string sql)
        {
            return GetDataTable(new NpgsqlCommand(sql));
        }

        public static DataTable GetDataTable(NpgsqlCommand cmd)
        {
            cmd.Connection = new NpgsqlConnection(Database.ConnectionString);
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
            cmd.Connection = new NpgsqlConnection(Database.ConnectionString);
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
                    sLine += dr[i].ToString();
                    if (i < dr.FieldCount - 1) sLine += delimiter;
                }
                sb.AppendLine(sLine);
            }
            dr.Close();
                       
            while (Common.IsFileLocked(path))   // Check if file is being used
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
    }

    class SQL
    {
        public static string BuildWhere(params string[] colNames)
        {
            string sWhere = "";
            //foreach (var col in colNames)
            //{
            //    sWhere+=col + " ~* :filter or "
            //}

            return sWhere;
        }
    }
}
