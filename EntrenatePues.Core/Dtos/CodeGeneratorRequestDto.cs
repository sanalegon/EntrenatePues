using System;

namespace EntrenatePues.Core.Dtos
{
    public class CodeGeneratorRequestDto
    {
        public int UserId { get; set; }
        public string Codigo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
