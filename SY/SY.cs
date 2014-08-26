﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using Dapper;
using kBit.ERP.IC;
using System.Drawing;

namespace kBit.ERP.SYS
{
    class Config
    {
        const string TableName = "sy_config";

        private string _value;

        private bool Changed { get; set; }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Code { get; set; }
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value && value != null)
                {
                    _value = value.ToUpper();
                    Changed = true;
                }
            }
        }

        public string Note { get; set; }
        public String Status { get; set; }


        public Config() { }

        public Config(string username, string code, string defaultValue, string note)
        {
            Username = username;
            Code = code;
            Value = defaultValue;
            Note = note;
            Get();
            Changed = false;
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

        public Point ValuePoint
        {
            get
            {
                if (Value == "") return new Point(-1, -1);
                Value = Util.RemoveCharacters(Value, "{}XY= "); // X Y
                string[] coords = Value.Split(',');
                return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
            }
        }

        public Size ValueSize
        {
            get
            {
                if (Value == "") return new Size(-1, -1);
                Value = Util.RemoveCharacters(Value, "{}WIDTHEG= ");    // WIDTH HEIGH
                string[] coords = Value.Split(',');
                return new Size(int.Parse(coords[0]), int.Parse(coords[1]));
            }
        }

        private void Add()
        {
            var sql = SqlFacade.SqlInsert(TableName, "username, code, value, note", "", true);
            Id = SqlFacade.Connection.ExecuteScalar<long>(sql, new { Username, Code, Value, Note });
        }

        private void Get()
        {
            var sWhere = "code ~* :code";
            if (Username.Length > 0)
                sWhere = "username ~* :username and " + sWhere;
            var sql = SqlFacade.SqlSelect(TableName, "id, upper(value) as value", sWhere);

            Config result = null;
            if (Username.Length > 0)
                result = SqlFacade.Connection.Query<Config>(sql, new { Username, Code }).FirstOrDefault();
            else
                result = SqlFacade.Connection.Query<Config>(sql, new { Code }).FirstOrDefault();
            if (result == null)
                Add();
            else
            {
                Id = result.Id;
                Value = result.Value;
            }
        }

        public void Save()
        {
            if (!Changed) return;
            var sql = SqlFacade.SqlUpdate(TableName, "value", "", "id = :id");
            SqlFacade.Connection.Execute(sql, new { Value, Id });
        }
    }

    static class ConfigFacade
    {
        static string syPrefix = "sy_";
        static string Username = App.session.Username;
        static string spliterDistance = "_splitter_distance";
        static string window_state = "_window_state";
        static string location = "_location";
        static string size = "_size";


        static Config _sy_select_limit = new Config("", syPrefix + "select_limit", "1000", "Maximum number of row [1000] display in data grid");
        static Config _sy_toolbar_icon_display_type = new Config(Username, syPrefix + "toolbar_icon_display_type", "IT", "Icon display type. [IT]=ImageAndText, I=Image, T=Text");
        static Config _sy_export_delimiter = new Config(Username, syPrefix + "export_delimiter", ",", "Export delimiter [,]");
        static Config _sy_export_open_file_after = new Config(Username, syPrefix + "export_open_file_after", "Y", "Open file after export. [Y]=Yes or N=No");
        static Config _sy_code_casing = new Config("", syPrefix + "code_casing", "U", "Code character casing. [U]=Upper, L=Lower or N=Normal");
        static Config _sy_language = new Config(Username, syPrefix + "language", "ENG", "Language. e.g ENG or KHM");

        static Config _ic_location_spitter_distance = new Config(Username, LocationFacade.TableName + spliterDistance, "207", "Data grid splitter distance [228]");
        static Config _ic_location_window_state = new Config(Username, LocationFacade.TableName + window_state, "0", "Window state. Normal, Maximize and Minimize");
        static Config _ic_location_location = new Config(Username, LocationFacade.TableName + location, "-1, -1", "Window location");
        static Config _ic_location_size = new Config(Username, LocationFacade.TableName + size, "1024, 601", "Form size [1024, 601]");


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

        public static string sy_language
        {
            get { return _sy_language.Value; }
            set { _sy_language.Value = value; }
        }


        // ic_location
        public static int ic_location_splitter_distance
        {
            get { return _ic_location_spitter_distance.ValueInt; }
            set { _ic_location_spitter_distance.Value = value.ToString(); }
        }

        public static int ic_location_window_state
        {
            get { return _ic_location_window_state.ValueInt; }
            set { _ic_location_window_state.Value = value.ToString(); }
        }

        public static Point ic_location_location
        {
            get { return _ic_location_location.ValuePoint; }
            set { _ic_location_location.Value = value.ToString(); }
        }

        public static Size ic_location_size
        {
            get { return _ic_location_size.ValueSize; }
            set { _ic_location_size.Value = value.ToString(); }
        }



        // Save configs back to table
        public static void SaveAll()
        {
            _sy_select_limit.Save();
            _sy_toolbar_icon_display_type.Save();
            _sy_export_delimiter.Save();
            _sy_export_open_file_after.Save();
            _sy_code_casing.Save();

            _ic_location_spitter_distance.Save();
            _ic_location_window_state.Save();
            _ic_location_location.Save();
            _ic_location_size.Save();


        }
    }

    class Label
    {


        Dictionary<string, string> labels = new Dictionary<string, string>();

        public Label()
        { }

        public Label(string function, string language)
        {
            //var sql = SqlFacade.SqlSelect(TableName, "field_name, value","function_code = :function and language = :language");
            //var labels = SqlFacade.Connection.query
        }

        private void Load(string function, string language)
        {

        }
    }

    class LabelFacade
    {
        const string TableName = "sy_label";

        public static string sy_button_new;
        public static string sy_button_copy;
        public static string sy_button_cancel;
        public static string sy_button_unlock;
        public static string sy_button_save;
        public static string sy_button_save_new;
        public static string sy_button_active;
        public static string sy_button_inactive;
        public static string sy_button_delete;
        public static string sy_button_mode;
        public static string sy_button_export;

        public static string sy_button_find;
        public static string sy_button_clear;
        public static string sy_button_filter;


        public static void LoadSystemButtonLabel()
        {
            var funCode = "sybtn";
            var lang = ConfigFacade.sy_language;
            sy_button_new = LabelFacade.GetLabel(funCode, lang, "new");
            sy_button_copy = LabelFacade.GetLabel(funCode, lang, "copy");
            sy_button_cancel = LabelFacade.GetLabel(funCode, lang, "cancel");
            sy_button_unlock = LabelFacade.GetLabel(funCode, lang, "unlock");
            sy_button_save = LabelFacade.GetLabel(funCode, lang, "save");
            sy_button_save_new = LabelFacade.GetLabel(funCode, lang, "save_new");
            sy_button_active = LabelFacade.GetLabel(funCode, lang, "active");
            sy_button_inactive = LabelFacade.GetLabel(funCode, lang, "inactive");
            sy_button_delete = LabelFacade.GetLabel(funCode, lang, "delete");
            sy_button_mode = LabelFacade.GetLabel(funCode, lang, "mode");
            sy_button_export = LabelFacade.GetLabel(funCode, lang, "export");

            sy_button_find = LabelFacade.GetLabel(funCode, lang, "find");
            sy_button_clear = LabelFacade.GetLabel(funCode, lang, "clear");
            sy_button_filter = LabelFacade.GetLabel(funCode, lang, "filter");
        }

        public static string GetLabel(string function_code, string language, string field_name)
        {
            var sql = SqlFacade.SqlSelect(TableName, "value", "function_code = :function_code and language = :language and field_name = :field_name");
            return SqlFacade.Connection.ExecuteScalar<string>(sql, new { function_code, language, field_name });
        }


    }
}