namespace VaporStore.DataProcessor
{
	using System;
	using Data;

	public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            return "TODO";
		}

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {

            var users = context.Users.ToList().Where(u => u.Cards.Any(c => c.Purchases.Any(p => p.Type.ToString() == storeType))).Select(u =>
                new UserPurchesExportDto
                {
                    Username = u.Username,
                    Purchases = u.Cards.SelectMany(c => c.Purchases).Where(p => p.Type.ToString() == storeType).Select(p => new UserPurchesExportDto.PurchaseExportDto
                    {
                        Card = p.Card.Number,
                        Cvc = p.Card.Cvc,
                        Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                        Game = new UserPurchesExportDto.PurchaseExportDto.GameXmlExportDto
                        {
                            Name = p.Game.Name,
                            Genre = p.Game.Genre.Name,
                            Price = p.Game.Price
                        }

                    }).OrderBy(x => x.Date).ToArray(),
                    TotalSpent = u.Cards.Select(c =>
                            c.Purchases.Where(p => p.Type.ToString() == storeType).Select(p => p.Game.Price).Sum())
                            .Sum()
                }).OrderByDescending(x => x.TotalSpent).ThenBy(x => x.Username).ToList();

            return XmlConverter.Serialize(users, "Users");
        }
    }
}