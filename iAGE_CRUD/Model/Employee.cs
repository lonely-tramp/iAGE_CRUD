namespace iAge_CRUD.Model
{
    public class Employee
    {
        public Employee(int id, string firstName, string lastName, decimal salary)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SalaryPerHour = salary;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal SalaryPerHour { get; set; }

        public override string ToString()
        {
            return $"Id = {Id},\tFirstName = {FirstName},\tLastName = {LastName},\tSalaryPerHour = {SalaryPerHour}";
        }
    }
}
