using SystemLosowaniaOsobyDoOdpowiedziV4.Models;
using SystemLosowaniaOsobyDoOdpowiedziV4.Services;

namespace SystemLosowaniaOsobyDoOdpowiedziV4.Views;

public partial class MainPage : ContentPage
{
    FileService fileService = new FileService();
    Random random = new Random();

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        LoadClasses();
    }

    void LoadClasses()
    {
        classPicker.ItemsSource = fileService.GetClasses();
    }

    private void OnDrawStudentClicked(object sender, EventArgs e)
    {
        if (classPicker.SelectedItem == null)
            return;

        var schoolClass = fileService.LoadClass(classPicker.SelectedItem.ToString());

        if (schoolClass.Students.Count == 0)
        {
            resultLabel.Text = "Brak uczniów.";
            return;
        }

        var student = schoolClass.Students[random.Next(schoolClass.Students.Count)];

        resultLabel.Text = $"{student.Number} - {student.Name}";
    }

    private async void OnEditClassClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditClassPage());
    }
}