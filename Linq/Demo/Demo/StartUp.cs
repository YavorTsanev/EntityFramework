using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Demo
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var db = new MusicXContext();

            db.Songs.Include(s => s.SongMetadata).Take(20).Select(s => new { }).ToList().ForEach(s => Console.WriteLine(string.Join(", ",s)));
        }
    }
}
