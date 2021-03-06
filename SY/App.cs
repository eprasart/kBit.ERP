﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Npgsql;
using Dapper;
using System.Reflection;
using kBit.ERP.SM;
using System.Diagnostics;

namespace kBit.ERP.SYS
{
    public static class App
    {
        public static frmSplash fSplash = null;

        public static Setting setting = new Setting();
        public static String version;
        public static Session session = new Session();

        public static IC.frmLocation fLocation;
        public static IC.frmCategory fCategory;
        public static IC.frmUnitMeasure fUnitMeasure;
        public static IC.frmItem fItem;

        public static SM.frmUserList fUserList;
        public static SM.frmAuditLog fAuditLog;

        public static FileLog AccessLog = new FileLog();

        public static string ProcessID = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

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

                fSplash.ShowMsg("Connecting to the database ...");
                SqlFacade.OpenConnection(); // Database connection: open/test

                fSplash.ShowMsg("Ensuring database setup ...");
                SqlFacade.EnsureDBSetup();  // Create tables if not exist
            }
            catch (Exception ex)
            {
                fSplash.ShowError(ex.Message);
                fSplash.Visible = false;
                fSplash.ShowDialog();
                ErrorLogFacade.LogToFile(ex);
                return false;
            }

            //Session            
            session.Username = "Visal"; //todo: logged in user (to be removed)
            session.Branch_Code = "000";
            session.Machine_Name = Environment.MachineName;
            session.Machine_User_Name = Environment.UserName;
            session.Version = version;

            LabelFacade.LoadSystemLabel();
            MessageFacade.LoadSystemMessage();

            // Log
            ErrorLogFacade.logFile.FileName = Path.Combine(Application.StartupPath, "Error.log");
            AccessLog.FileName = Path.Combine(Application.StartupPath, "Access.log");
            AccessLog.Write(DateTime.Now.ToString("yyy-MM-dd ddd hh:mm:ss tt") + " Application started. Process Id: " + ProcessID + ", Machine: " + session.Machine_Name + ", machine's username: " + session.Machine_User_Name + ", version: " + session.Version);
            session.Id = SessionFacade.Save(session);
            SessionLogFacade.Log(Type.Priority_Information, "Application", Type.Log_Launch, "Application started");

            return true;
        }

        private static void SetVersion()
        {
            //version = Util.RemoveLastDotZero(Assembly.GetEntryAssembly().GetName().Version.ToString());
            version = Util.RemoveLastDotZero(Application.ProductVersion);
        }

        private static void LoadSettings()
        {
            setting.Path = Path.Combine(Application.StartupPath, "setting.ini");
            try
            {
                SqlFacade.ConnectionString = setting.Get("ConnectionString", @"server=localhost;uid=erp;pwd=erp");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void Close()
        {
            App.AccessLog.Write(DateTime.Now.ToString("yyy-MM-dd ddd hh:mm:ss tt") + " Application quit. Process Id: " + App.ProcessID);
            SM.SessionFacade.UpdateLogout(App.session);

            setting.Save();
            ConfigFacade.SaveAll();
        }
    }
}