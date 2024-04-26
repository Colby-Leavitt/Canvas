using Library.Canvas.Models;
using Library.Canvas.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Canvas.ViewModels
{
    internal class CourseDetailViewModel
    {
        public CourseDetailViewModel(int id=0)
        {
            if (id > 0)
            {
                LoadById(id);
            }
        }

        /*
        public string Name
        {
            get => course?.Name ?? string.Empty;
            set { if (course != null) course.Name = value; }
        }
        public string Description
        {
            get => course?.Description ?? string.Empty;
            set { if (course != null) course.Description = value; }
        }
        public string Prefix
        {
            get => course?.Prefix ?? string.Empty;
            set { if (course != null) course.Prefix = value; }
        }
        public int Id { get; set; }

        public string CourseCode
        {
            get => course?.Code ?? string.Empty;
        }
        */

        public string Name { get; set; }
        public string Description { get; set; }
        public string Prefix { get; set; }
        public string CourseCode { get; set; }
        public int Id { get; set; }

        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var course = CourseService.Current.GetById(id) as Course;
            if (course != null)
            {
                Name = course.Name;
                Id = course.Id;
                Description = course.Description;
                Prefix = course.Prefix;
                CourseCode = course.Code;
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Description));
            NotifyPropertyChanged(nameof(Prefix));
            NotifyPropertyChanged(nameof(CourseCode));

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Course course;

        public void AddCourse()
        {
            if (Id <= 0)
            {
                CourseService.Current.Add(new Course { Name = Name, Prefix = Prefix, Description = Description });
            }
            else
            {
                var refToUpdate = CourseService.Current.GetById(Id) as Course;
                refToUpdate.Name = Name;
                refToUpdate.Prefix = Prefix;
                refToUpdate.Description = Description;
            }
            Shell.Current.GoToAsync("//Instructor");
        }

    }
}
