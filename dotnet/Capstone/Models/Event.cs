using System;
using System.Collections.Generic;

namespace Capstone.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int DjUserId { get; set; }
        public int? HostUserId { get; set; }
        public int? PlaylistId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string? DjName { get; set; }
        public string? HostName { get; set; }
        public int? LoggedInUser { get; set; }
    }
}