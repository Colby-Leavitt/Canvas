
using Library.Canvas.Models;
using Library.Canvas.Services;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class CourseDetailView : ContentPage
{
    public CourseDetailView()
    {
        InitializeComponent();
        BindingContext = new CourseDetailViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddCourse(Shell.Current);
    }
}
