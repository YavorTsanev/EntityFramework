﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Data.Models
{
    public class Projection
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public DateTime DateTime { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
