using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.TMDbModel.AdditionalModelObjects
{
    public class Credentials
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Username { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Password { get; set; }
    }
}
