using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleManager.Models
{
    class Record
    {
        public int Id { get; set; }
        [ForeignKey("Employee_EmployeeId")]
        public Employee Employee { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
