/*TODO:
 * Option to auto switch keyboard to KH when in Unicode box
 * Loging in db and file
 */
using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using ServiceStack.OrmLite;
using System.Reflection;
using kPrasat.SM;

namespace kPrasat
{
    public static class App
    {
        public static Setting setting = new Setting();
        public static String version;
        public static long sessionId;

        public static void Init()
        {
            LoadSettings();
            try
            {
                Database.Factory = new OrmLiteConnectionFactory(Database.ConnectionString, PostgreSqlDialect.Provider);
                Database.Connection = Database.Factory.OpenDbConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
                return;
            }
            Database.PrepareDatabase();
            SetVersion();

            Login.Company_Id = 1;
            Login.Username = "Visal";

            // Session
            //session.Username = Login.Username;
            var session = new Session();
            session.MachineName = Environment.MachineName;
            session.MachinerUserName = Environment.UserName;
            session.Version = version;
            sessionId = SessionFacade.Save(session);
        }

        private static void SetVersion()
        {
            version = Common.RemoveLastDotZero(Assembly.GetEntryAssembly().GetName().Version.ToString());
        }

        private static void LoadSettings()
        {
            setting.Path = Path.Combine(Application.StartupPath, "setting.ini");
            Database.ConnectionString = setting.Get("ConnectionString", @"server=localhost;uid=kcheckout;pwd=kcheckout");
            //todo: test connection
        }

        public static void SaveSettings()
        {
            setting.Save();
        }
    }
}
