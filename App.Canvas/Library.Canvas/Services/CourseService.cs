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
        private List<Course> courseList = new List<Course>();
        private static CourseService? _instance;


        private CourseService()
        {
            courseList = new List<Course>();

        }

        public static CourseService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseService();
                }
                return _instance;
            }
        }
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

        public IEnumerable<Course> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
                 || s.Description.ToUpper().Contains(query.ToUpper())
                 || s.Code.ToUpper().Contains(query.ToUpper()));
        }
    }
}
