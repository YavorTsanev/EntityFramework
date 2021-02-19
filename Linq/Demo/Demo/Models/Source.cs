using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Source
    {
        public Source()
        {
            ArtistMetadata = new HashSet<ArtistMetadata>();
            SongMetadata = new HashSet<SongMetadata>();
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ArtistMetadata> ArtistMetadata { get; set; }
        public virtual ICollection<SongMetadata> SongMetadata { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
