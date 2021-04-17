using System;
namespace Capstone.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }
        public string ImgUrl { get; set; }
        public bool HasUpVoted { get; set; } = false;
        public bool HasDownVoted { get; set; } = false;
        public int? SongScore { get; set; }
    }

    public class SongVote
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int VoteValue { get; set; }
    }

    public class PossibleSong
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }
        public string ImgUrl { get; set; }
        public bool HasUpVoted { get; set; } = false;
        public bool HasDownVoted { get; set; } = false;
        public int SongScore { get; set; }
    }
}