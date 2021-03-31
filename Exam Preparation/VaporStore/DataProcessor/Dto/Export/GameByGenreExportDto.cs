using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Export
{
    public class GameByGenreExportDto
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public GameExportDto[] Games { get; set; }
        public int TotalPlayers { get; set; }
    }

    public class GameExportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Tags { get; set; }
        public int Players { get; set; }
    }

}
