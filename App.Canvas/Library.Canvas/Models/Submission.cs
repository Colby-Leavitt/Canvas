using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Canvas.Models
{
    public class Submission
    {
        public int Id { get; private set; }
        private static int lastId = 0;

        public Student Student { get; set; }
        public Assignment Assignment { get; set; }
        public string Content { get; set; }
        public decimal Grade { get; set; }


        public Submission() 
        {
            Id = ++lastId;
            Content = string.Empty;
        }

        public override string ToString()
        {
            return $"[{Id}] ({Grade}) {Student.Name}: {Assignment.Name}";
        }
    }
}
