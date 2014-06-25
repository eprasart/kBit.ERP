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

namespace kPrasat.SYS
{
    public static class App
    {
        public static Setting setting = new Setting();
        public static String version;
        public static Session session = new Session();

        public static IC.frmLocationList fLocationList;
        public static SM.frmUserList fUserList;
        public static SM.frmAuditLog fAuditLog;

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


            session.Username = "Visal";

            // Session
            //session.Username = Login.Username;

            session.MachineName = Environment.MachineName;
            session.MachineUserName = Environment.UserName;
            session.Version = version;
            session.Id = SessionFacade.Save(session);
        }

        private static void SetVersion()
        {
            version = Common.RemoveLastDotZero(Assembly.GetEntryAssembly().GetName().Version.ToString());
        }

        private static void LoadSettings()
        {
            setting.Path = Path.Combine(Application.StartupPath, "setting.ini");
            Database.ConnectionString = setting.Get("ConnectionString", @"server=localhost;uid=erp;pwd=erp");
            //todo: test connection
        }

        public static void SaveSettings()
        {
            setting.Save();
        }
    }
}
