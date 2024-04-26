
using Library.Canvas.Models;
using Library.Canvas.Services;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

[QueryProperty(nameof(CourseId), "courseId")]
public partial class CourseDetailView : ContentPage
{
    public CourseDetailView()
    {
        InitializeComponent();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddCourse();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDetailViewModel(CourseId);
    }

    public int CourseId
    {
        set; get;
    }
}
