﻿namespace EntrenatePues.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
        public int Role { get; set; }

    }
}
