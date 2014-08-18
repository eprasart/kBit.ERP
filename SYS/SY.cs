using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using Dapper;

namespace kBit.ERP.SYS
{
    class Config
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Note { get; set; }
        public String Status { get; set; }
    }

    static class ConfigFacade
    {
        const string TableName = "sy_config";

        private static void Add(string name, string value)
        {

        }

        public static object Get(string name, object defaultValue = null)
        {
            var sql = SqlFacade.SqlSelect(TableName, "value", "name = @name");
            var result = SqlFacade.Connection.ExecuteScalar(sql, new { name });
            if (result == null)
                return defaultValue;
            return result;
        }

        public static void Set(string name, object value)
        {

        }

    }
}