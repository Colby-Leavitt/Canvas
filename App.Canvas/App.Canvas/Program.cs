﻿using System;
using App.Canvas.Helpers;
using Library.Canvas.Models;
using Library.Canvas.Services;
namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("1. Maintain People");
                Console.WriteLine("2. Maintain Courses");
                Console.WriteLine("3. Exit");                           //sys
                var input = Console.ReadLine();            
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        ShowStudentMenu(studentHelper);
                    }
                    else if (result == 2)
                    {
                        ShowCourseMenu(courseHelper);
                    }
                    else if (result == 3)
                    {
                        cont = false;
                    }
                }
            }
        }

        static void ShowStudentMenu(StudentHelper studentHelper)
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Add a new person");               
            Console.WriteLine("2. Update a person");    
            Console.WriteLine("3. List all people");     
            Console.WriteLine("4. Search for a person");
            Console.WriteLine("5. Get a student's GPA");

            var input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    studentHelper.CreateStudentRecord();
                }
                else if (result == 2)
                {
                    studentHelper.UpdateStudentRecord();
                }
                else if (result == 3)
                {
                    studentHelper.ListStudents();
                }
                else if (result == 4)
                {
                    studentHelper.SearchStudents();
                }
                else if (result == 5)
                {
                    studentHelper.GetGPA();
                }
            }
        }

        static void ShowCourseMenu(CourseHelper courseHelper)
        {
            Console.WriteLine("1. Add a new course");               
            Console.WriteLine("2. Update a course");                
            Console.WriteLine("3. Add a student to a course");
            Console.WriteLine("4. Remove a student from a course");
            Console.WriteLine("5. Get a course grade for a student");
            Console.WriteLine("6. Add an assignment");
            Console.WriteLine("7. Update an assignment");
            Console.WriteLine("8. Remove an assignment");
            Console.WriteLine("9. Create a student submission");
            Console.WriteLine("10. List all submissions for a course");
            Console.WriteLine("11. Update a student submission");
            Console.WriteLine("12. Remove a student submission");
            Console.WriteLine("13. Grade a student submission");
            Console.WriteLine("14. Add a module to a course");
            Console.WriteLine("15. Remove a module from a course");
            Console.WriteLine("16. Update a module in a course");
            Console.WriteLine("17. Add an announcement to a course");
            Console.WriteLine("18. Update an announcement in a course");
            Console.WriteLine("19. Remove an announcement from a course");
            Console.WriteLine("20. List all courses");               
            Console.WriteLine("21. Search for a course");            

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    courseHelper.CreateCourseRecord();
                }
                else if (result == 2)
                {
                    courseHelper.UpdateCourseRecord();
                }
                else if(result == 3)
                {
                    courseHelper.AddStudent();
                }
                else if(result == 4)
                {
                    courseHelper.RemoveStudent();
                }
                else if(result == 5)
                {
                    courseHelper.GetStudentGrade();
                }
                else if(result == 6)
                {
                    courseHelper.AddAssignment();
                }
                else if(result == 7)
                {
                    courseHelper.UpdateAssignment();
                }
                else if(result == 8)
                {
                    courseHelper.RemoveAssignment();
                }
                else if(result == 9)
                {
                    courseHelper.AddSubmission();
                }
                else if(result == 10)
                {
                    courseHelper.ListSubmissions();
                }
                else if(result == 11)
                {
                    courseHelper.UpdateSubmission();
                }
                else if(result == 12)
                {
                    courseHelper.RemoveSubmission();
                }
                else if(result == 13)
                {
                    courseHelper.GradeSubmission();
                }
                else if(result == 14)
                {
                    courseHelper.AddModule();
                }
                else if(result == 15)
                {
                    courseHelper.RemoveModule();
                }
                else if(result == 16)
                {
                    courseHelper.UpdateModule();
                }
                else if(result == 17)
                {
                    courseHelper.AddAnnouncement();
                }
                else if(result == 18)
                {
                    courseHelper.UpdateAnnouncement();
                }
                else if(result == 19)
                {
                    courseHelper.RemoveAnnouncement();
                }
                else if (result == 20)
                {
                    courseHelper.SearchCourses();
                }
                else if (result == 21)
                {
                    Console.WriteLine("Enter a query: ");
                    var query = Console.ReadLine() ?? string.Empty;
                    courseHelper.SearchCourses(query);
                }
            }
        }
    }


}