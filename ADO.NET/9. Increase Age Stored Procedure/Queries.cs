using System;
using System.Collections.Generic;
using System.Text;

namespace _9._Increase_Age_Stored_Procedure
{
    public static class Queries
    {
        public const string ExecStoredProcedure = @"exec sp_IncreaseMinionAge @id";

        public const string PrintResult = @"select * from Minions where id = @id";

    }
}
