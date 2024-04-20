using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Canvas.Models
{
    public class Module
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ContentItem> Content {  get; set; }

        private static int lastId = 0;
        private int id = 0;
        public int Id
        {
            get
            {
                if (id == 0)
                {
                    id = ++lastId;
                }
                return id;
            }
        }

        public Module() 
        { 
            Content = new List<ContentItem>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\n" +
                $"{string.Join("\n\t", Content.Select(c => c.ToString()).ToArray())}";
        }
    }
}
