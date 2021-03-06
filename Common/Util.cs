﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kBit.ERP
{
    class Util
    {
        public static string RemoveLastDotZero(string v)
        {
            string s = v;
            if (v.EndsWith(".0"))
                return RemoveLastDotZero(v.Substring(0, v.Length - 2));
            else
                return s;
        }

        public static bool IsFileLocked(string path)
        {
            if (!File.Exists(path)) return false;
            FileStream stream = null;
            try
            {
                stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked
            return false;
        }

        public static string ReadTextFile(string path)
        {
            if (!File.Exists(path)) return "";
            var content = "";
            using (var sr = new StreamReader(path))
            {
                content = sr.ReadToEnd();
                sr.Close();
            }
            return content;
        }

        public static string RemoveCharacters(string value, string characters)
        {
            return new Regex("[" + characters + "]").Replace(value, "");
        }

        public static bool IsEmailValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

        public static string EscapeNewLine(string value)
        {
            return value.Replace(@"\r\n", Environment.NewLine);
        }

        public static bool IsInteger(string value)
        {
            int isNumber = 0;
            return int.TryParse(value, out isNumber);
        }

        public static bool IsDecimal(string value)
        {
            decimal isNumber = 0;
            return decimal.TryParse(value, out isNumber);
        }
    }
}
