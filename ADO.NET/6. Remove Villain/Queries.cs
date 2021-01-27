using System;
using System.Collections.Generic;
using System.Text;

namespace _6._Remove_Villain
{
    public static class Queries
    {
        public const string Transaction = @"begin transaction select Name from Villains where Id = 2 select COUNT(*) from MinionsVillains where VillainId = @Id delete from MinionsVillains where VillainId = @Id delete from Villains where Id = @Id commit";

        public const string GetVillanName = @"select Name from Villains where Id = @Id";

        public const string GetCountOfDeletedMinions = @"select COUNT(*) from MinionsVillains where VillainId = @Id";

    }
}
