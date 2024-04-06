using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Canvas.Models;

namespace Library.Canvas.Services
{
    public class CourseService
    {
        public List<Course> courseList = new List<Course>();

        public void Add(Course course)
        {
            courseList.Add(course);
        }
        public List<Course> Courses
        {
            get
            {
                return courseList;
            }
        }
    }
}
