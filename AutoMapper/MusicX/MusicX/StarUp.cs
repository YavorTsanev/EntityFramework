using AutoMapper;
using AutoMapper.QueryableExtensions;
using MusicX.Data.Models;
using MusicX.Data.Profiles;
using MusicX.View;
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
                cfg.AddProfile(new ArtistProfile());
                cfg.AddProfile(new SongProfile());
            });

            var db = new MusicXContext();

            var artistView = db.Artists.ProjectTo<ArtistWithSongs>(config).Take(5);

            var songview = db.Songs.ProjectTo<SongView>(config).Take(10);

            Print(songview);

            //var inputModel = new SongView
            //{
            //    Name = "Yoo",

            //    SourceName = "YouTube",

            //    Artists = "Ooooo"
            //};

            //var mapper = config.CreateMapper();

            //var song = mapper.Map<Song>(inputModel);

            //Print(song);

        }

        public static void Print(object artists)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artists, Formatting.Indented));
        }
    }

}
