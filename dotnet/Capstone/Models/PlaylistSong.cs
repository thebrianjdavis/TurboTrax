using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PlaylistSong
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int SongScore { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }
        public string ImgUrl { get; set; }
    }
    public class AddRemoveSong
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
    }
}
