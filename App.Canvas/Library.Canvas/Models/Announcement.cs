using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Canvas.Models
{
    public class Announcement
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Id { get; private set; }
        private static int lastId = 0;

        public Announcement() 
        {
            Id = ++lastId;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}";
        }

    }
}
