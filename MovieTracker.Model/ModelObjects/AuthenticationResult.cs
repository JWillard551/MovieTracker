using System;

namespace MovieTracker.Model.ModelObjects
{
	class AuthenticationResult
	{
		public string Token { get; set; }
		public string Session { get; set; }
		public string Guest { get; set; }
		public DateTime? ExpiresAt { get; set; }
		public bool Success { get; set; }
	}
}
