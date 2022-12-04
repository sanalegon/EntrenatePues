using System;

namespace EntrenatePues.Core.Domain
{
    public class CodeGenerator
    {
        public int Id_Codigo { get; set; }
        public int UserId { get; set; }
        public string Codigo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
