using System.Diagnostics;

namespace MovieTracker.Model.ModelObjects
{
    public class Credentials
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Username { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Password { get; set; }
    }
}
