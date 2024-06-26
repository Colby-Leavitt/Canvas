﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Canvas.Models
{
    public class AssignmentGroup
    {
        public List<Assignment> Assignments { get; set; }
        public int Id { get; private set; }
        private static int lastId = 0;

        public string Name { get; set; }

        private decimal weight;
        public decimal Weight 
        {
            get { return weight;} 
            set
            {
                if (value > 1)
                    value /= 100;
                weight = value;
            }
        }

        public AssignmentGroup() 
        { 
            Assignments = new List<Assignment>();
            Name = string.Empty;
            Weight = 1;

            Id = ++lastId;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} ({Weight})%\n\t{string.Join("\n", Assignments.Select(s => s.ToString()).ToArray())}";
        }

    }
}
