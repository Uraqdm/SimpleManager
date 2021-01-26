namespace SimpleManager.Models
{
    class Employee : Person
    {
        public int EmployeeId { get; set; }
        public string Login { get; set; }
        public string Password  { get; set; }
        /// <summary>
        /// Возвращает или задает должность работника
        /// </summary>
        public Role Role { get; set; }
    }
}
