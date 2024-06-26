﻿using Library.Canvas.Models;
using Library.Canvas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Canvas.ViewModels
{
    public class InstructorViewViewModel : INotifyPropertyChanged
    {
        public InstructorViewViewModel()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
        }
        public ObservableCollection<Person> People
        {
            get
            {

                var filteredList = StudentService
                    .Current
                    .Students
                    .Where(
                    s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Person>(filteredList);

            }
        }

        public ObservableCollection<Course> Courses
        {
            get
            {
                var filteredCourseList = CourseService
                    .Current
                    .Courses
                    .Where(
                    s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Course>(filteredCourseList);
            }
        }

        public string Title { get => "Instructor / Administrator Menu"; }

        public bool IsEnrollmentsVisible
        {
            get; set;
        }

        public bool IsCoursesVisible
        {
            get; set;
        }

        public void ShowEnrollments()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }

        public void ShowCourses()
        {
            IsEnrollmentsVisible = false;
            IsCoursesVisible = true;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }
        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
                NotifyPropertyChanged(nameof(Courses));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void EditEnrollmentClick(Shell s)
        {
            var idParam = SelectedPerson?.Id ?? 0;
            s.GoToAsync($"//PersonDetail?personId={idParam}");
        }

        public void EditCourseClick(Shell s)
        {
            var idParam = SelectedCourse?.Id ?? 0;
            s.GoToAsync($"//CourseDetail?courseId={idParam}");
        }

        public void AddEnrollmentClick(Shell s)
        {
            s.GoToAsync($"//PersonDetail?personId=0");
        }

        public void AddCourseClick(Shell s)
        {
            s.GoToAsync($"//CourseDetail");
        }

        public void RemoveEnrollmentClick()
        {
            if (SelectedPerson == null) { return; }

            StudentService.Current.Remove(SelectedPerson);
            RefreshView();
        }

        public void RemoveCourseClick()
        {
            if (SelectedCourse == null) { return; }

            CourseService.Current.Remove(SelectedCourse);
            RefreshView();
        }

        public void ResetView()
        {
            Query = string.Empty;
            NotifyPropertyChanged(nameof(Query));
        }

        public void RefreshView()
        {

            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
        }

    }
}
