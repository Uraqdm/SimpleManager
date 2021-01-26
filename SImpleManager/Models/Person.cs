namespace SimpleManager.Models
{
    abstract class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
