using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTracker.TMDbModel.ModelObjects
{
    public class OperationResult
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;

        public OperationResult(bool success)
        {
            Success = success;
        }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
