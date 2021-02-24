using AutoMapper;
using MusicX.Data.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MusicX
{
    public class StarUp
    {
        static void Main()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Artist,ArtistWithSongs>();
            });

            var mapper = config.CreateMapper();

            var db = new MusicXContext();

            var artist = db.Artists.FirstOrDefault();

            var artistViewModel = mapper.Map<ArtistWithSongs>(artist);



            Print(artistViewModel);
        }

        public static void Print(object artists)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artists, Formatting.Indented));
        }

        class ArtistWithSongs
        {
            public string Name { get; set; }

            public int SongsCount { get; set; }
        }
    }
}
