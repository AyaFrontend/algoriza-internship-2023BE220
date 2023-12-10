using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vizeeta.Presentation.ConnectionData
{
    public static class ServerConnection
    {
        public static string ConnectionString {  get; } = "Server= PC\\SQLEXPRESS; Database = Vizeeta ; Integrated Security = true ";
        public static string Redis {get;} ="localhost";
    }
}
