namespace iAGE_CRUD
{
    public class Employee : Person
    {
        public Employee(int id, string firstName, string lastName, decimal salary) : base(id , firstName, lastName) { SalaryPerHour = salary; } 

        public decimal SalaryPerHour { get; set; }

        public override string GetInfo()
        {
            return $"Id = {Id},\tFirstName = {FirstName},\tLastName = {LastName},\tSalaryPerHour = {SalaryPerHour}";
        }
    }
}
