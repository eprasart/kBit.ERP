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
            ////using (var cnn = GetOpenConnection())
            ////{
            ////    var cmd = cnn.CreateCommand();
            ////    cmd.CommandText = Properties.Resources.Script;
            ////    cmd.Connection = cnn;
            ////    cmd.ExecuteNonQuery();
            ////}
        }

        public static void OpenConnection()
        {
            //var connection = new NpgsqlConnection(ConnectionString);
            //connection.Open();
            //return connection;            
            Connection = new NpgsqlConnection(ConnectionString);            
            Connection.Open();
        }



        //public static DateTime GetCurrentTimeStamp()
        //{
        //    DateTime? ts=null;
        //    using(var cnn = GetOpenConnection())
        //    {
        //       ts = cnn.Query<DateTime>("select now()");
        //        cnn.Close();
        //    }
        //    return ts; // Connection.Scalar<DateTime>("select now()");
        //}

        public static string ExcuteString(string sql)
        {
            return "";  //todo: Connection.Scalar<string>(sql);
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
}
