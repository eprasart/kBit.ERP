using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using Dapper;
using kBit.ERP.IC;

namespace kBit.ERP.SYS
{
    class Config
    {
        const string TableName = "sy_config";

        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Note { get; set; }
        public String Status { get; set; }

        public Config() { }

        public Config(string name, string defaultValue, string note)
        {
            Name = name;
            Value = defaultValue;
            Note = note;
            Get();
        }

        public override string ToString()
        {
            return Value;
        }

        public int ValueInt
        {
            get { return int.Parse(Value); }
        }

        public bool ValueBool
        {
            get { return Value == "Y" || Value == "T" ? true : false; }
        }

        private void Add()
        {
            var sql = SqlFacade.SqlInsert(TableName, "name, value, note", "");
            SqlFacade.Connection.Execute(sql, new { Name, Value, Note });
        }

        private void Get()
        {
            var sql = SqlFacade.SqlSelect(TableName, "upper(value)", "name = :name");
            var result = SqlFacade.Connection.ExecuteScalar(sql, new { Name });
            if (result == null)
                Add();
            else
                Value = result.ToString();
        }

        public void Save()
        {
            //todo: only save if change
            var sql = SqlFacade.SqlUpdate(TableName, "value", "", "name = :name");
            SqlFacade.Connection.Execute(sql, new { Value, Name });
        }
    }

    static class ConfigFacade
    {
        static string syPrefix = "sy_";

        static string spliterDistance = "_splitter_distance";

        static Config _sy_select_limit = new Config(syPrefix + "select_limit", "1000", "Maximum number of row [1000] display in data grid");
        static Config _sy_toolbar_icon_display_type = new Config(syPrefix + "toolbar_icon_display_type", "IT", "Icon display type. [IT]=ImageAndText, I=Image, T=Text");
        static Config _sy_export_delimiter = new Config(syPrefix + "export_delimiter", ",", "Export delimiter [,]");
        static Config _sy_export_open_file_after = new Config(syPrefix + "export_open_file_after", "Y", "Open file after export. [Y]=Yes or N=No");
        static Config _sy_code_casing = new Config(syPrefix + "code_casing", "U", "Code character casing. [U]=Upper, L=Lower or N=Normal");

        static Config _ic_location_spitter_distance = new Config(LocationFacade.TableName + spliterDistance, "228", "Data grid splitter distance [228]");

        public static int sy_select_limit
        {
            get { return _sy_select_limit.ValueInt; }
            set { _sy_select_limit.Value = value.ToString(); }
        }

        public static string sy_toolbar_icon_display_type
        {
            get { return _sy_toolbar_icon_display_type.Value; }
            set { _sy_toolbar_icon_display_type.Value = value; }
        }

        public static string sy_export_delimiter
        {
            get { return _sy_export_delimiter.Value; }
            set { _sy_export_delimiter.Value = value; }
        }

        public static bool sy_export_open_file_after
        {
            get { return _sy_export_open_file_after.ValueBool; }
            set { _sy_export_open_file_after.Value = value == true ? "Y" : "N"; }
        }

        public static string sy_code_casing
        {
            get { return _sy_code_casing.Value; }
            set { _sy_export_open_file_after.Value = value; }
        }




        // ic_location
        public static int ic_location_splitter_distance
        {
            get { return _ic_location_spitter_distance.ValueInt; }
            set { _ic_location_spitter_distance.Value = value.ToString(); }
        }

        public static void SaveAll()
        {
            _sy_select_limit.Save();
            _sy_toolbar_icon_display_type.Save();
            _sy_export_delimiter.Save();
            _sy_export_open_file_after.Save();
            _sy_code_casing.Save();

            _ic_location_spitter_distance.Save();


        }
    }
}