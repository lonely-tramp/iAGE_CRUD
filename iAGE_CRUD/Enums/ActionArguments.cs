using System;

namespace iAGE_CRUD.Enums
{
    [Flags]
    public enum ActionArgumetsEnum
    {
        None = 0,
        Id = 1,
        FirstName = 2,
        LastName = 4,
        Salary = 8
    }
}
