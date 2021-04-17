using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SongShoutOut
    {
        public int PlaylistId { get; set; }
        public int SongId { get; set; }
        public int ShoutOutId { get; set; }
        public string ShoutOutMessage { get; set; }
    }
}
