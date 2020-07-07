using System.ComponentModel;

namespace Algolia.Application.infrastructure
{
    public abstract class ResponseModelBase
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public ResponseModelBase(string message,bool success)
        {
            this.Message = message;
            this.Success = success;
        }
    }
}