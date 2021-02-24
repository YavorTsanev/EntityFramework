using AutoMapper;
using MusicX.Data.Models;
using MusicX.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicX.Data.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            this.CreateMap<Song, SongView>().ForMember(m => m.Artists, opt => opt.MapFrom(s => string.Join(" , ", s.SongArtists.Select(s => s.Artist.Name)))).ReverseMap();
        }
    }
}
