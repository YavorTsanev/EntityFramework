using System;
using System.Collections.Generic;
using System.Text;

namespace _4._Add_Minion
{
    public static class Queries
    {
        public const string GetTownId = @"select Id from Towns where Name = @TownName";

        public const string AddTown = @"insert into Towns(Name,CountryCode) values(@TownName,1)";

        public const string GetVillainId = @"select Id from Villains where Name = @VillainName";

        public const string AddVillain = @"insert into Villains(Name,EvilnessFactorId) values(@VillainName,4)";

        public const string AddMinion = @"insert into Minions(Name,Age,TownId) values(@MinionName, @MinionAge, @TownId)";

        public const string GetMinionId = @"select Id from Minions where Name = @MinionName and Age = @MinionAge";

        public const string AddMinionToMVTable = @"insert into MinionsVillains(MinionId,VillainId) values(@MinionId, @VillainId)";

    }
}
