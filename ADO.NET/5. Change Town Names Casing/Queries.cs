using System;
using System.Collections.Generic;
using System.Text;

namespace _5._Change_Town_Names_Casing
{
    public static class Queries
    {
        public const string GetCountryId = @"select Id from Countries where Name = @CountryName";

        public const string GetTowns = @"select Name from Towns where CountryCode = @CountryId";
    }
}
