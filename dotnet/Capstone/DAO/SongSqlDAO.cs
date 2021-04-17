using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;



namespace Capstone.DAO
{
    public class SongSqlDAO : ISongDAO
    {
        private readonly string connectionString;

        public SongSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool AddSongVote(SongVote songVote)
        {
            int currentSongVoteValue = 0;
            bool transactionCompleted = false;
            PossibleSong ps = new PossibleSong();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // sql statements
                    string sql = "SELECT playlist_id, song_id, song_name, artist_name, genre, spotify_id, img_url, song_score FROM potential_playlist_songs WHERE playlist_id = @playlistId AND song_id = @songId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlistId", songVote.PlaylistId);
                    cmd.Parameters.AddWithValue("@songId", songVote.SongId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ps = GetPossibleSongFromReader(reader);
                        currentSongVoteValue = ps.SongScore;
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            currentSongVoteValue += songVote.VoteValue;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "UPDATE potential_playlist_songs SET song_name = @song_name, artist_name = @artist_name, genre = @genre, img_url = @img_url, song_score = @song_score WHERE playlist_id = @playlist_id AND song_id = @song_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@song_name", ps.SongName);
                    cmd.Parameters.AddWithValue("@artist_name", ps.ArtistName);
                    cmd.Parameters.AddWithValue("@genre", ps.Genre);
                    cmd.Parameters.AddWithValue("@img_url", ps.ImgUrl);
                    cmd.Parameters.AddWithValue("@song_score", currentSongVoteValue);
                    cmd.Parameters.AddWithValue("@playlist_id", songVote.PlaylistId);
                    cmd.Parameters.AddWithValue("@song_id", songVote.SongId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        transactionCompleted = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return transactionCompleted;
        }
        public bool AddNewSong(Song newSong)
        {
            bool wasCreated = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO songs(song_name, artist_name, genre, spotify_id, img_url) VALUES (@song_name, @artist_name, @genre, @spotify_id, @img_url);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@song_name", newSong.SongName);
                    cmd.Parameters.AddWithValue("@artist_name", newSong.ArtistName);
                    cmd.Parameters.AddWithValue("@genre", newSong.Genre);
                    cmd.Parameters.AddWithValue("@spotify_id", newSong.SpotifyId);
                    cmd.Parameters.AddWithValue("@img_url", newSong.ImgUrl);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        wasCreated = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return wasCreated;
        }
        public bool AddSongToPlaylist(int playlistId, int songId)
        {
            bool wasAdded = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO playlist_songs(playlist_id, song_id) VALUES (@playlist_id, @song_id);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", playlistId);
                    cmd.Parameters.AddWithValue("@song_id", songId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        wasAdded = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM potential_playlist_songs WHERE playlist_id = @playlist_id AND song_id = @song_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", playlistId);
                    cmd.Parameters.AddWithValue("@song_id", songId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        wasAdded = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return wasAdded;

        }
        public bool RemoveSongFromPlaylist(int playlistId, int songId)
        {
            bool songRemoved = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM playlist_songs WHERE playlist_id = @playlist_id AND song_id = @song_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", playlistId);
                    cmd.Parameters.AddWithValue("@song_id", songId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        songRemoved = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return songRemoved;
        }
        public List<PlaylistSong> GetPlaylistSongs(int playlistId)
        {
            List<PlaylistSong> playlist = new List<PlaylistSong>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT ps.playlist_id, ps.song_id, s.song_name, s.artist_name, s.genre, s.spotify_id, s.img_url FROM playlist_songs ps JOIN songs s ON s.song_id = ps.song_id WHERE ps.playlist_id = @playlist_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", playlistId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        playlist.Add(GetPlaylistSongFromReader(reader));
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e + " - PC Load Letter");
                throw;
            }

            return playlist;
        }
        public bool CreatePossibleSongs(int eventId)
        {
            bool createSuccess = false;
            int rowsAffected = 0;
            List<string> excludedGenres = new List<string>();
            List<Song> allPossibleSongs = new List<Song>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT genre FROM excluded_genres WHERE event_id = @event_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@event_id", eventId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        excludedGenres.Add(Convert.ToString(reader["genre"]));
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e + " - PC Load Letter");
                throw;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT song_id, song_name, artist_name, genre, spotify_id, img_url FROM songs;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Song s = (GetSongFromReader(reader));
                        if (excludedGenres.Contains(s.Genre))
                        {
                            continue;
                        }
                        else
                        {
                            allPossibleSongs.Add(s);
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e + " - PC Load Letter");
                throw;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (Song song in allPossibleSongs)
                    {
                        string sql = "INSERT INTO potential_playlist_songs (playlist_id, song_id, song_name, artist_name, genre, spotify_id, img_url) VALUES (@playlist_id, @song_id, @song_name, @artist_name, @genre, @spotify_id, @img_url);";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@playlist_id", eventId);
                        cmd.Parameters.AddWithValue("@song_id", song.SongId);
                        cmd.Parameters.AddWithValue("@song_name", song.SongName);
                        cmd.Parameters.AddWithValue("@artist_name", song.ArtistName);
                        cmd.Parameters.AddWithValue("@genre", song.Genre);
                        cmd.Parameters.AddWithValue("@spotify_id", song.SpotifyId);
                        cmd.Parameters.AddWithValue("@img_url", song.ImgUrl);
                        int rowCreated = cmd.ExecuteNonQuery();
                        rowsAffected += rowCreated;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            if (rowsAffected == allPossibleSongs.Count)
            {
                createSuccess = true;
            }
            return createSuccess;
        }
        public List<PossibleSong> GetPossibleSongs(int eventId)
        {
            List<PossibleSong> possibleSongs = new List<PossibleSong>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT playlist_id, song_id, song_name, artist_name, genre, spotify_id, img_url, song_score FROM potential_playlist_songs WHERE playlist_id = @playlist_id ORDER BY song_score DESC;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", eventId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        possibleSongs.Add(GetPossibleSongFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e + " - PC Load Letter");
                throw;
            }
            return possibleSongs;
        }
        public bool AddSongShoutOut(SongShoutOut songShoutOut)
        {
            bool shoutOutSongWasAdded = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO playlist_songs_shoutouts(playlist_id, song_id, shoutout_message) VALUES (@playlist_id, @song_id, @shoutout_message);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@playlist_id", songShoutOut.PlaylistId);
                    cmd.Parameters.AddWithValue("@song_id", songShoutOut.SongId);
                    cmd.Parameters.AddWithValue("@shoutout_message", songShoutOut.ShoutOutMessage);

                    int rowsAdded = cmd.ExecuteNonQuery();
                    if (rowsAdded > 0)
                    {
                        shoutOutSongWasAdded = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return shoutOutSongWasAdded;
        }
        public bool ExcludeGenres(int eventId, string[] genres)
        {
            bool wasSuccessful = false;
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "DELETE FROM excluded_genres WHERE event_id = @thisId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@thisId", eventId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (string genre in genres)
                    {
                        string sql = "INSERT INTO excluded_genres (event_id, genre) VALUES (@event_id, @genre);";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@thisId", eventId);
                        cmd.Parameters.AddWithValue("@event_id", eventId);
                        cmd.Parameters.AddWithValue("@genre", genre);

                        int rowAffected = cmd.ExecuteNonQuery();
                        rowsAffected += rowAffected;
                    }

                    if (rowsAffected == genres.Length)
                    {
                        wasSuccessful = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return wasSuccessful;
        }
        private PlaylistSong GetPlaylistSongFromReader(SqlDataReader reader)
        {
            PlaylistSong pls = new PlaylistSong()
            {
                PlaylistId = Convert.ToInt32(reader["playlist_id"]),
                SongId = Convert.ToInt32(reader["song_id"]),
                SongName = Convert.ToString(reader["song_name"]),
                ArtistName = Convert.ToString(reader["artist_name"]),
                Genre = Convert.ToString(reader["genre"]),
                SpotifyId = Convert.ToString(reader["spotify_id"]),
                ImgUrl = Convert.ToString(reader["img_url"])
            };
            return pls;
        }
        private Song GetSongFromReader(SqlDataReader reader)
        {
            Song s = new Song()
            {
                SongId = Convert.ToInt32(reader["song_id"]),
                SongName = Convert.ToString(reader["song_name"]),
                ArtistName = Convert.ToString(reader["artist_name"]),
                Genre = Convert.ToString(reader["genre"]),
                SpotifyId = Convert.ToString(reader["spotify_id"]),
                ImgUrl = Convert.ToString(reader["img_url"])
            };
            return s;
        }
        private PossibleSong GetPossibleSongFromReader(SqlDataReader reader)
        {
            PossibleSong ps = new PossibleSong()
            {
                PlaylistId = Convert.ToInt32(reader["playlist_id"]),
                SongId = Convert.ToInt32(reader["song_id"]),
                SongName = Convert.ToString(reader["song_name"]),
                ArtistName = Convert.ToString(reader["artist_name"]),
                Genre = Convert.ToString(reader["genre"]),
                SpotifyId = Convert.ToString(reader["spotify_id"]),
                ImgUrl = Convert.ToString(reader["img_url"]),
                SongScore = Convert.ToInt32(reader["song_score"])
            };
            return ps;
        }
    }
}