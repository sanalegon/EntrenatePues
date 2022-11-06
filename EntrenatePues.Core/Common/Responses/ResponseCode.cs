using System.Net;

namespace EntrenatePues.Core.Common.Responses
{
    public class ResponseCode
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }

        public ResponseCode(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
