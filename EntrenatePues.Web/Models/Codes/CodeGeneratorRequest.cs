using System;

namespace EntrenatePues.Web.Models.Codes
{
    public class CodeGeneratorRequest
    {
        public int UserId { get; set; }
        public int Code { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
