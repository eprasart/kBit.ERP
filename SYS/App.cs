/*TODO:
 * Option to auto switch keyboard to KH when in Unicode box
*/
using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using ServiceStack.OrmLite;
using System.Reflection;
using kBit.ERP.SM;

namespace kBit.ERP.SYS
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

        public static FileLog AccessLog = new FileLog();

        public static bool Init()
        {
            SetVersion();

            // Splash screen
            fSplash = new frmSplash();
            fSplash.SetAppName(" v " + SYS.App.version);
            fSplash.Show();
            fSplash.ShowMsg("Initializing the application...");
            fSplash.TopMost = true;
            // Database connection string from setting.ini
            try
            {
                fSplash.ShowMsg("Loading settings...");
                LoadSettings();
            }
            catch (Exception ex)
            {
                ErrorLogFacade.LogToFile(ex);
            }
            // Database connection: open/test
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
            // Create tables if not exist
            Database.PrepareDatabase();

            //Session            
            session.Username = "Visal"; //todo: to be removed
            session.MachineName = Environment.MachineName;
            session.MachineUserName = Environment.UserName;
            session.Version = version;
            // Log
            ErrorLogFacade.logFile.FileName = Path.Combine(Application.StartupPath, "Error.log");
            AccessLog.FileName = Path.Combine(Application.StartupPath, "Access.log");
            AccessLog.Write(DateTime.Now.ToString("yyy-MM-dd ddd hh:mm:ss tt") + " Application started. Machine: " + session.MachineName + ", machine username: " + session.MachineUserName + ", version: " + session.Version);
            session.Id = SessionFacade.Save(session);
            SessionLogFacade.Log(Type.Priority_Information, "Application", Type.Log_Launch, "Application started");

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
