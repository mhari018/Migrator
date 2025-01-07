using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigratorABC
{
    public class AppConfig
    {
        public string? ELASTIC_URL {  get; set; } 
        public string? POSTGRES_URL { get; set; }
    }
}
