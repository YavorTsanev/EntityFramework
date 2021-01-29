using EFDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFDemo
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicXContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=MusicX;Integrated Security=true;");

            var db = new MusicXContext(optionsBuilder.Options);
            
        }
    }
}
