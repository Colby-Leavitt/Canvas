using Library.Canvas.Services;
using Library.Canvas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Canvas.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService = new CourseService();

        public void CreateCourseRecord(Course? selectedCourse = null)
        {
            Console.WriteLine("What is the code of the course? ");
            var code = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the name of the course? ");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the description of the course? ");
            var description = Console.ReadLine() ?? string.Empty;

            bool isNewCourse = false;
            if(selectedCourse == null) 
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }


            selectedCourse.Code = code;
            selectedCourse.Name = name;
            selectedCourse.Description = description;

            if(isNewCourse) 
            { 
                courseService.Add(selectedCourse); 
            }
            
        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Enter the code for the course to update: ");
            ListCourses();

            var selection = Console.ReadLine();


            var selectedCourse = courseService.courseList.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateCourseRecord(selectedCourse);
            }
            
        }
    }
}
