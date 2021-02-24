﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                cfg.CreateMap<Artist, ArtistWithSongs>();
                cfg.CreateMap<Song, SongView>().ForMember(m => m.Artists, opt => opt.MapFrom(s => string.Join(" , ",s.SongArtists.Select(s => s.Artist.Name))));
            });

            var db = new MusicXContext();

            var artistView = db.Artists.ProjectTo<ArtistWithSongs>(config).Take(5);

            var songview = db.Songs.ProjectTo<SongView>(config).Take(10);

            Print(songview);
        }

        public static void Print(object artists)
        {
            Console.WriteLine(JsonConvert.SerializeObject(artists, Formatting.Indented));
        }

        class ArtistWithSongs
        {
            public string Name { get; set; }

            public int ArtistMetadataCount { get; set; }

        }

        class SongView
        {
            public string Name { get; set; }

            public string Artists { get; set; }
        }
    }
   


}
