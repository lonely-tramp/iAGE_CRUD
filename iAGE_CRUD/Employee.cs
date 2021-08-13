namespace iAGE_CRUD
{
    public class Employee : Person
    {
        public Employee(int id, string firstName, string lastName, decimal salary) : base(id , firstName, lastName) { SalaryPerHour = salary; } 

        public decimal SalaryPerHour { get; set; }

        public override string GetInfo()
        {
            return $"{base.GetInfo()},\tSalaryPerHour = {SalaryPerHour}";
        }
    }
}
