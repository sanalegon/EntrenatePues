using System;

namespace EntrenatePues.Core.Domain
{
    public class User_Routine
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdRoutine { get; set; }
        public string FrequencyDays { get; set; }
        public DateTime DateMade { get; set; }
    }
}
