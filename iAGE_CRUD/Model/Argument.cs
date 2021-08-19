namespace iAGE_CRUD.Model
{
    public class Argument
    {
        public Argument(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }

    }
}
