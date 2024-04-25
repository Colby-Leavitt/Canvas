using Library.Canvas.Models;
using Library.Canvas.Services;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

[QueryProperty(nameof(PersonId), "personId")]
public partial class PersonDetailView : ContentPage
{
    public PersonDetailView()
    {
        InitializeComponent();
    }

    public int PersonId
    {
        set; get;
    }

    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as PersonDetailViewModel).AddPerson();
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new PersonDetailViewModel(PersonId);
    }
}