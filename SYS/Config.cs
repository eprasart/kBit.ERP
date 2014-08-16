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
        public string LockBy { get; set; }
        public DateTime? LockAt { get; set; }
        public string InsertBy { get; set; }
        public DateTime? InsertAt { get; set; }
        public string ChangeBy { get; set; }
        public DateTime? ChangeAt { get; set; }
    }

    static class ConfigFacade
    {

    
    }
}