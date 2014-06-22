using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kPrasat
{
    public static class StatusType
    {
        public static string Active = "A";
        public static string InActive = "I";
        public static string Deleted = "X";
    }

    public static class LockType
    {
        public static string Locked = "L";
        public static string Unlock = "";
    }

    public static class Priority
    {
        public static string Information = "I";
        public static string Caution = "C";
        public static string Warning = "W";
        public static string Error = "E";
    }

    public static class LogType
    {
        public static string Insert = "INS";
        public static string Update = "UPD";
        public static string Delete = "DEL";
        public static string Unlock = "UNL";
    }
}
