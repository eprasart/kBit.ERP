using System;

namespace kPrasat
{
    public static class Type
    {
        // Record status type
        public static string RecordStatus_Active = "A";
        public static string RecordStatus_InActive = "I";
        public static string RecordStatus_Deleted = "X";

        // Record lock type
        public static string Lock_Locked = "L";
        public static string Lock_Unlock = "";

        // Session log priority
        public static string Priority_Information = "I";
        public static string Priority_Caution = "C";
        public static string Priority_Warning = "W";
        public static string Priority_Error = "E";

        // Session log type
        public static string Log_Insert = "Insert";
        public static string Log_Update = "Update";
        public static string Log_Delete = "Delete";
        public static string Log_New = "New";
        public static string Log_Lock = "Lock";
        public static string Log_Unlock = "Unlock";
        public static string Log_Open = "Open";
        public static string Log_Copy = "Copy";
        public static string Log_Save = "Save";
        public static string Log_SaveAndNew = "Save and New";
        public static string Log_Active = "Active";
        public static string Log_Inactive = "Inactive";
        public static string Log_ResetPwd = "Password Reset";
        public static string Log_Login = "Login";
        public static string Log_Launch = "Launch";
    }
}
