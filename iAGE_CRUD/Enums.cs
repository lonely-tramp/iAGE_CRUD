using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iAGE_CRUD
{
    public enum OperationsEnum
    {
        Get,
        GetAll,
        Add,
        Update,
        Delete
    }

    [Flags]
    public enum ArgumetsEnum
    {
        None = 0,
        Id = 1,
        FirstName = 2,
        LastName = 4,
        Salary = 8
    }
}