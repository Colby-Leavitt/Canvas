﻿using Library.Canvas.Services;
using Library.Canvas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.InteropServices;

namespace App.Canvas.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        private StudentService studentService;

        public CourseHelper() 
        { 
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public void CreateCourseRecord(Course? selectedCourse = null)
        {
            bool isNewCourse = false;
            if (selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            var choice = "Y";
            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course code? (Y/N)");
                choice = Console.ReadLine() ?? "N";
            }

            if(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What is the prefix of the course? ");
                selectedCourse.Prefix = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course name? (Y/N)");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What is the name of the course? ");
                selectedCourse.Name = Console.ReadLine() ?? string.Empty;
            }

            if(!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course description? (Y/N)");
                choice = Console.ReadLine() ?? "N";
            }
            else 
            {
                choice = "Y";
            }
            
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {

                Console.WriteLine("What is the description of the course? ");
                selectedCourse.Description = Console.ReadLine() ?? string.Empty;
            }   

            if (isNewCourse)
            {
                SetupRoster(selectedCourse);
                SetupAssignments(selectedCourse);
                SetupModules(selectedCourse);
            }


            if(isNewCourse) 
            { 
                courseService.Add(selectedCourse); 
            }
            
        }
        private Module CreateModule(Course c)
        {
            Console.WriteLine("Name: ");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Description: ");
            var description = Console.ReadLine() ?? string.Empty;

            var module = new Module
            {
                Name = name,
                Description = description
            };

            Console.WriteLine("Would you like to add content?");
            var choice = Console.ReadLine() ?? "N";
            while(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What type of content would you like to add?");
                Console.WriteLine("1. Assignment");
                Console.WriteLine("2. File");
                Console.WriteLine("3. Page");
                var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                switch (contentChoice)
                {
                    case 1:
                        var newAssignmentContent = CreateAssignmentItem(c);
                        if (newAssignmentContent != null)
                        {
                            module.Content.Add(newAssignmentContent);
                        }
                        break;
                    case 2:
                        var newFileContent = CreateFileItem(c);
                        if (newFileContent != null)
                        {
                            module.Content.Add(newFileContent);
                        }
                        break;
                    case 3:
                        var newPageContent = CreatePageItem(c);
                        if (newPageContent != null)
                        {
                            module.Content.Add(newPageContent);
                        }
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Would you like to add more content?");
                choice = Console.ReadLine() ?? "N";
            }

            return module;
        }        
        
        private Announcement CreateAnnouncement(Course c)
        {
            Console.WriteLine("Enter the name of the announcement:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter the description of the announcement:");
            var description = Console.ReadLine();

            return new Announcement
            {
                Name = name,
                Description = description
            };

        }


        public void SearchCourses(string? query = null)
        {
            if(string.IsNullOrEmpty(query))
                courseService.Courses.ForEach(Console.WriteLine);
            else
                courseService.Search(query).ToList().ForEach(Console.WriteLine);

            Console.WriteLine("Select a course: ");
            var code = Console.ReadLine() ?? string.Empty;
            var selectedCourse = courseService.Courses.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine(selectedCourse.DetailDisplay);
            }
        }


        public void AddStudent()
        {
            Console.WriteLine("Enter the code for the course to add the student to: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                studentService.Students.Where(s => !selectedCourse.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                if (studentService.Students.Any(s => !selectedCourse.Roster.Any(s2 => s2.Id == s.Id)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }
                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);
                    if (selectedStudent != null)
                    {
                        selectedCourse.Roster.Add(selectedStudent);
                    }
                }
            }
        }

        public void AddAssignment()
        {
            Console.WriteLine("Enter the code for the course to add the assignment to: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Assignments.Add(CreateAssignment());
            }
        }
        
        public void AddModule()
        {
            Console.WriteLine("Enter the code for the course to add the assignment to: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Modules.Add(CreateModule(selectedCourse));
            }
        }
        
        public void AddAnnouncement()
        {

            Console.WriteLine("Enter the code for the course to add the assignment to: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Announcements.Add(CreateAnnouncement(selectedCourse));
            }
        }

        
        public void RemoveStudent() 
        {
            Console.WriteLine("Enter the code for the course to remove the student from: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Roster.ForEach(Console.WriteLine);
                if (selectedCourse.Roster.Any())
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }
                else
                {
                    selection = null;
                }
                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);
                    if (selectedStudent != null)
                    {
                        selectedCourse.Roster.Remove(selectedStudent);
                    }
                }
            }
        }

        public void RemoveAssignment()
        {
            Console.WriteLine("Enter the code for the course to remove the assignment from: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to remove: ");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    selectedCourse.Assignments.Remove(selectedAssignment);
                }
            }
        }

        public void RemoveModule()
        {
            Console.WriteLine("Enter the code for the course to remove the module from: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose module to remove: ");
                selectedCourse.Modules.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedModule = selectedCourse.Modules.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedModule != null)
                {
                    selectedCourse.Modules.Remove(selectedModule);
                }
            }

        }
        
        public void RemoveAnnouncement()
        {
            Console.WriteLine("Enter the code for the course to remove the announcement from: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an announcement to remove: ");
                selectedCourse.Announcements.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAnnouncement = selectedCourse.Announcements.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAnnouncement != null)
                {
                    selectedCourse.Announcements.Remove(selectedAnnouncement);
                }
            }
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Enter the code for the course to update: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateCourseRecord(selectedCourse);
            }
            
        }

        public void UpdateAssignment()
        {
            Console.WriteLine("Enter the code for the course to update the assignment for: ");
            courseService.Courses.ForEach(Console.WriteLine);

            var selection = Console.ReadLine();


            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to update: ");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    var index = selectedCourse.Assignments.IndexOf(selectedAssignment);
                    selectedCourse.Assignments.RemoveAt(index);
                    selectedCourse.Assignments.Insert(index, CreateAssignment());
                }
            }
        }

        public void UpdateModule()
                {
                    Console.WriteLine("Enter the code for the course to update the assignment for: ");
                    courseService.Courses.ForEach(Console.WriteLine);
                    var selection = Console.ReadLine();
                    var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            
                    if(selectedCourse != null && selectedCourse.Modules.Any())
                    {
                        Console.WriteLine("Enter the id for the module to update:");
                        selectedCourse.Modules.ForEach(Console.WriteLine);

                        selection = Console.ReadLine();
                        var selectedModule = selectedCourse.Modules.FirstOrDefault(m => m.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));

                        if (selectedModule != null)
                        {
                            Console.WriteLine("Would you like to modify the module name? (Y/N)");
                            selection = Console.ReadLine();
                            if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                            {
                                Console.WriteLine("Name:");
                                selectedModule.Name = Console.ReadLine();
                            }

                            Console.WriteLine("Would you like to modify the module description? (Y/N)");
                            selection = Console.ReadLine();
                            if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                            {
                                Console.WriteLine("Description:");
                                selectedModule.Description = Console.ReadLine();
                            }

                            Console.WriteLine("Would you like to delete content from this module?");
                            selection = Console.ReadLine();
                            if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                            {
                                var keepRemoving = true;
                                while (keepRemoving)
                                {
                                    selectedModule.Content.ForEach(Console.WriteLine);
                                    selection = Console.ReadLine();

                                    var contentToRemove = selectedModule.Content.FirstOrDefault(c => c.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));
                                    if (contentToRemove != null)
                                    {
                                        selectedModule.Content.Remove(contentToRemove);
                                    }

                                    Console.WriteLine("Would you like to remove more content? (Y/N)");
                                    selection = Console.ReadLine();
                                    if(selection?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false)
                                    {
                                        keepRemoving = false;
                                    }
                                }
                            }

                            Console.WriteLine("Would you like to add content?");
                            var choice = Console.ReadLine() ?? "N";
                            while (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                            {
                                Console.WriteLine("What type of content would you like to add?");
                                Console.WriteLine("1. Assignment");
                                Console.WriteLine("2. File");
                                Console.WriteLine("3. Page");
                                var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                                switch (contentChoice)
                                {
                                    case 1:
                                        var newAssignmentContent = CreateAssignmentItem(selectedCourse);
                                        if (newAssignmentContent != null)
                                        {
                                            selectedModule.Content.Add(newAssignmentContent);
                                        }
                                        break;
                                    case 2:
                                        var newFileContent = CreateFileItem(selectedCourse);
                                        if (newFileContent != null)
                                        {
                                            selectedModule.Content.Add(newFileContent);
                                        }
                                        break;
                                    case 3:
                                        var newPageContent = CreatePageItem(selectedCourse);
                                        if (newPageContent != null)
                                        {
                                            selectedModule.Content.Add(newPageContent);
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                Console.WriteLine("Would you like to add more content?");
                                choice = Console.ReadLine() ?? "N";
                            }
                        }

                    }

                }


        private void SetupRoster(Course c)
        {
            
            Console.WriteLine("Which students should be enrolled in this course? ('Q' to quit)");
            bool continueAdding = true;
            while (continueAdding)
            {
                studentService.Students.Where(s => !c.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                var selection = "Q";

                if (studentService.Students.Any(s => !c.Roster.Any(s2 => s2.Id == s.Id)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = false;
                }
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);

                    if (selectedStudent != null)
                    {
                        c.Roster.Add(selectedStudent);
                    }
                }
            }
        }

        private void SetupAssignments(Course c)
        {
            Console.WriteLine("Would you like to add assignments? (Y/N) ");
            bool continueAdding;
            var assignResponse = Console.ReadLine() ?? "N";
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    c.Assignments.Add(CreateAssignment());

                    Console.WriteLine("Add more assignments? (Y,N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }

        }

        private void SetupModules(Course c)
        {
            Console.WriteLine("Would you like to add modules? (Y/N) ");
            bool continueAdding;
            var assignResponse = Console.ReadLine() ?? "N";
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    c.Modules.Add(CreateModule(c));

                    Console.WriteLine("Add more modules? (Y,N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }
        }

        


        private AssignmentItem? CreateAssignmentItem(Course c)
        {
            Console.WriteLine("Name: ");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Description: ");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Which assignment should be added?");
            c.Assignments.ForEach(Console.WriteLine);
            var choice = int.Parse(Console.ReadLine() ?? "-1");
            if (choice >= 0)
            {
                var assignment = c.Assignments.FirstOrDefault(a => a.Id == choice);
                return new AssignmentItem
                {
                    Assignment = assignment,
                    Name = name,
                    Description = description
                };
            }
            return null;
        }

        private FileItem? CreateFileItem(Course c)
        {
            Console.WriteLine("Name: ");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Description: ");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a path to the file:");
            var filepath = Console.ReadLine();

            return new FileItem 
            { 
                Path = filepath, 
                Name = name, 
                Description = description 
            };
        }

        private PageItem? CreatePageItem(Course c)
        {
            Console.WriteLine("Name: ");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Description: ");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter page content:");
            var body = Console.ReadLine();

            return new PageItem
            {
                HtmlBody = body,
                Name = name,
                Description = description
            };
        }

        private Assignment CreateAssignment()
        {
            Console.WriteLine("Name: ");
            var assignmentName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Description: ");
            var assignmentDescription = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Total Points: ");
            var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");
            Console.WriteLine("Due Date: ");
            var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1900");

            return new Assignment
            {
                Name = assignmentName,
                Description = assignmentDescription,
                TotalAvailablePoints = totalPoints,
                DueDate = dueDate
            };
        }

        
    }
}
