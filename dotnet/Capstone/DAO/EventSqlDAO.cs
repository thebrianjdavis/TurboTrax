using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using Capstone.Models;

namespace Capstone.DAO
{
    public class EventSqlDAO : IEventDAO
    {
        private readonly string connectionString;

        public EventSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void addEvent(Event newEvent)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "INSERT INTO playlists (playlist_name) VALUES ('@playlist_name'); INSERT INTO events(dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time) VALUES (@dj_user_id, @host_user_id, @playlist_id, @event_name, @description, @event_date, @start_time, @end_time);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dj_user_id", newEvent.DjUserId);
                    cmd.Parameters.AddWithValue("@host_user_id", newEvent.HostUserId);
                    cmd.Parameters.AddWithValue("@playlist_id", newEvent.PlaylistId);
                    cmd.Parameters.AddWithValue("@event_name", newEvent.EventName);
                    cmd.Parameters.AddWithValue("@description", newEvent.EventDescription);
                    cmd.Parameters.AddWithValue("@event_date", newEvent.EventDate);
                    cmd.Parameters.AddWithValue("@start_time", newEvent.StartTime);
                    cmd.Parameters.AddWithValue("@end_time", newEvent.EndTime);
                    cmd.Parameters.AddWithValue("@playlist_name", newEvent.EventName);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void updateEvent(int id, Event updatedEvent)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "UPDATE events SET dj_user_id = @dj_user_id, host_user_id = @host_user_id, playlist_id = @playlist_id, event_name = @event_name, description = @description, event_date = @event_date, start_time = @start_time, end_time = @end_time WHERE event_id = @event_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@dj_user_id", updatedEvent.DjUserId);
                    cmd.Parameters.AddWithValue("@host_user_id", updatedEvent.HostUserId);
                    cmd.Parameters.AddWithValue("@playlist_id", updatedEvent.PlaylistId);
                    cmd.Parameters.AddWithValue("@event_name", updatedEvent.EventName);
                    cmd.Parameters.AddWithValue("@description", updatedEvent.EventDescription);
                    cmd.Parameters.AddWithValue("@event_date", updatedEvent.EventDate);
                    cmd.Parameters.AddWithValue("@start_time", updatedEvent.StartTime);
                    cmd.Parameters.AddWithValue("@end_time", updatedEvent.EndTime);
                    cmd.Parameters.AddWithValue("@event_id", id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public List<Event> getEvents()
        {
            List<Event> allEvents = new List<Event>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    string sql = "SELECT event_id, dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time, dj.first_name AS djfirstname, dj.last_name AS djlastname, host.first_name AS hostfirstname, host.last_name AS hostlastname FROM events JOIN users dj ON dj_user_id = dj.user_id JOIN users host ON host_user_id = host.user_id;";
                    //string sql = "SELECT event_id, dj_user_id, host_user_id, playlist_id, event_name, description, event_date, start_time, end_time FROM events;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allEvents.Add(GetEventFromReader(reader));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return allEvents;
        }

        public List<SongShoutOut> getShoutouts(int eventId)
        {
            List<SongShoutOut> allShoutouts = new List<SongShoutOut>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    
                    string sql = "SELECT pss.playlist_id, pss.song_id, pss.shoutout_id, pss.shoutout_message FROM playlist_songs_shoutouts pss JOIN songs s ON s.song_id = pss.song_id JOIN events e ON pss.playlist_id = e.event_id WHERE e.event_id = @event_id;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@event_id", eventId);
                    SqlDataReader reader = cmd.ExecuteReader();                    

                    while (reader.Read())
                    {
                        allShoutouts.Add(GetSongShoutOutFromReader(reader));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return allShoutouts;
        }

        private SongShoutOut GetSongShoutOutFromReader(SqlDataReader reader)
        {
            SongShoutOut sso = new SongShoutOut()
            {
                PlaylistId = Convert.ToInt32(reader["playlist_id"]),
                SongId = Convert.ToInt32(reader["song_id"]),
                ShoutOutId = Convert.ToInt32(reader["shoutout_id"]),
                ShoutOutMessage = Convert.ToString(reader["shoutout_message"])
            };

            return sso;
        }

        private Event GetEventFromReader(SqlDataReader reader)
        {
            Event e = new Event()
            {
                EventId = Convert.ToInt32(reader["event_id"]),
                DjUserId = Convert.ToInt32(reader["dj_user_id"]),
                HostUserId = Convert.ToInt32(reader["host_user_id"]),
                PlaylistId = Convert.ToInt32(reader["playlist_id"]),
                EventName = Convert.ToString(reader["event_name"]),
                EventDescription = Convert.ToString(reader["description"])
            };
            // Dealing with dates and times
            string tempStart = Convert.ToString(reader["start_time"]);
            TimeSpan tempStartTime = TimeSpan.Parse(tempStart);
            e.StartTime = timeConverter(tempStartTime);
            string tempEnd = Convert.ToString(reader["end_time"]);
            TimeSpan tempEndTime = TimeSpan.Parse(tempEnd);
            e.EndTime = timeConverter(tempEndTime);
            string tempDate = Convert.ToString(reader["event_date"]);
            string[] temp = tempDate.Split(' ');
            e.EventDate = temp[0];

            // Dj and host names
            string djFirst = Convert.ToString(reader["djfirstname"]);
            string djLast = Convert.ToString(reader["djlastname"]).Substring(0, 1);
            e.DjName = $"{djFirst} {djLast}";
            string hostFirst = Convert.ToString(reader["hostfirstname"]);
            string hostLast = Convert.ToString(reader["hostlastname"]).Substring(0, 1);
            e.HostName = $"{hostFirst} {hostLast}";

            return e;
        }
        private string timeConverter(TimeSpan time)
        {
            int hours = 0;
            string tT = "AM";
            if (time.Hours > 12)
            {
                hours = time.Hours - 12;
                tT = "PM";
            }
            else
            {
                hours = time.Hours;
            }
            string minutes = "00";
            if (time.Minutes > 9)
            {
                minutes = time.Minutes.ToString();
            }
            else if (time.Minutes >= 1 && time.Minutes <= 9)
            {
                minutes = $"0{time.Minutes}";
            }
            return $"{hours}:{minutes} {tT}";
        }
    }
}
