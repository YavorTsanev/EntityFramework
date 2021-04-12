using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Cinema.Data.Models.Enums;

namespace Cinema.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }

        [Range(0,9)]
        public Genre Genre { get; set; }

        public TimeSpan Duration { get; set; }

        [Range(1,10)]
        public double Rating { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Director{ get; set; }

        public virtual ICollection<Projection> Projections { get; set; } = new HashSet<Projection>();


    }
}
