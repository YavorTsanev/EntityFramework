using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.DataProcessor.Dto.Import;

namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var gamesDto = JsonConvert.DeserializeObject<List<GameImportDto>>(jsonString);

            foreach (var item in gamesDto)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var game = new Game
                {
                    Name = item.Name,
                    Price = item.Price,
                    ReleaseDate = DateTime.Parse(item.ReleaseDate, CultureInfo.InvariantCulture),
                    Developer = context.Developers.FirstOrDefault(d => d.Name == item.Developer) ?? new Developer { Name = item.Developer },
                    Genre = context.Genres.FirstOrDefault(g => g.Name == item.Genre) ?? new Genre { Name = item.Genre }
                };


                foreach (var tagg in item.Tags)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.Name == tagg) ?? new Tag { Name = tagg };

                    game.GameTags.Add(new GameTag
                    {
                        Tag = tag
                    });
                }

                context.Games.Add(game);
                context.SaveChanges();
                sb.AppendLine($"Added {item.Name} ({item.Genre}) with {item.Tags.Count} tags");

            }

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var usersCardsDtos = JsonConvert.DeserializeObject<ICollection<UserCardImportDto>>(jsonString);
            var sb = new StringBuilder();

            foreach (var itemDto in usersCardsDtos)
            {
                if (!IsValid(itemDto) || !itemDto.Cards.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var user = Mapper.Map<User>(itemDto);
                context.Users.Add(user);

                sb.AppendLine($"Imported {itemDto.Username} with {itemDto.Cards.Length} cards");
            }
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            return "TODO";
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}