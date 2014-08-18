using System;
using System.Collections.Generic;

using System.Linq;
using System.Data;
using Npgsql;

namespace kBit.ERP.SYS
{    
    class Config
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Note { get; set; }
        public String Status { get; set; }
        public string Insert_By { get; set; }
        public DateTime? Insert_At { get; set; }
        public string Change_By { get; set; }
        public DateTime? Change_At { get; set; }
    }

    static class ConfigFacade
    {

    
    }
}