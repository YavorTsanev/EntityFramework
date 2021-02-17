using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data;
using System;

namespace P01_HospitalDatabase 
{
    public class StarUp
    {
        public static void Main(string[] args)
        {
            var db = new HospitalContext();

            db.Database.Migrate();
        }
    }
}
