/*TODO:
 * Option to auto switch keyboard to KH when in Unicode box
*/
using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using ServiceStack.OrmLite;
using System.Reflection;
using kPrasat.SM;

namespace kPrasat.SYS
{
    public static class App
    {
        public static frmSplash fSplash = null;

        public static Setting setting = new Setting();
        public static String version;
        public static Session session = new Session();

        public static IC.frmLocationList fLocationList;
        public static SM.frmUserList fUserList;
        public static SM.frmAuditLog fAuditLog;

        public static bool Init()
        {
            SetVersion();

            // Splash screen
            fSplash = new frmSplash();
            fSplash.SetAppName(" v " + SYS.App.version);
            fSplash.Show();
            fSplash.ShowMsg("Initializing the application...");
            Application.DoEvents();

            fSplash.TopMost = true;
            try
            {
                fSplash.ShowMsg("Loading settings...");
                LoadSettings();
            }
            catch (Exception ex)
            {
                ErrorLogFacade.LogToFile(ex);
            }
            try
            {
                fSplash.ShowMsg("Connecting to the database...");
                Database.Factory = new OrmLiteConnectionFactory(Database.ConnectionString, PostgreSqlDialect.Provider);
                Database.Connection = Database.Factory.OpenDbConnection();
            }
            catch (Npgsql.NpgsqlException ex)
            {
                fSplash.ShowError(ex.Message);
                fSplash.Visible = false;
                fSplash.ShowDialog();
                ErrorLogFacade.LogToFile(ex);
                return false;
            }

            Database.PrepareDatabase();

            session.Username = "Visal";

            // Session
            //session.Username = Login.Username;

            session.MachineName = Environment.MachineName;
            session.MachineUserName = Environment.UserName;
            session.Version = version;
            session.Id = SessionFacade.Save(session);           
            return true;
        }

        private static void SetVersion()
        {
            version = Common.RemoveLastDotZero(Assembly.GetEntryAssembly().GetName().Version.ToString());
        }

        private static void LoadSettings()
        {
            setting.Path = Path.Combine(Application.StartupPath, "setting.ini");
            Database.ConnectionString = setting.Get("ConnectionString", @"server=localhost;uid=erp;pwd=erp");            
        }

        public static void SaveSettings()
        {
            setting.Save();
        }
    }
}
