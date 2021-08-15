namespace iAGE_CRUD
{
    public class Person
    {
        public Person(int id, string firstName, string lastName) { Id = id; FirstName = firstName; LastName = lastName; }
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual string GetInfo()
        {
            return $"Id = {Id},\tFirstName = {FirstName},\tLastName = {LastName}";
        }
    }
}
