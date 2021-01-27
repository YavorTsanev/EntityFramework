using System;
using System.Collections.Generic;
using System.Text;

namespace _8._Increase_Minion_Age
{
    public static class Queries
    {
        public const string UpdateToLower = @"update minions
                            set Name = LOWER(Name)";

        public const string UpdateMinions = @" 
 
                            update Minions
                            set Age += 1,
                            Name = upper(LEFT(Name,1)) + SUBSTRING(Name, 2, len(Name))
                            where id in (@Id)";


        public const string PrintResult = @"select Name, Age from Minions";
    }


}
