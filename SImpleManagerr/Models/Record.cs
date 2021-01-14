using System;

namespace SimpleManager.Models
{
    class Record
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
