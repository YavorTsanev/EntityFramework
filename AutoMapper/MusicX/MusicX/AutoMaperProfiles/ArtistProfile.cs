using AutoMapper;
using MusicX.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicX.Data.Profiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            this.CreateMap<Artist, ArtistWithSongs>();
        }
    }
}
