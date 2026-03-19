using SystemLosowaniaOsobyDoOdpowiedziV4.Models;
using SystemLosowaniaOsobyDoOdpowiedziV4.Services;

namespace SystemLosowaniaOsobyDoOdpowiedziV4.Views;

public partial class EditClassPage : ContentPage
{
    SchoolClass schoolClass = new SchoolClass();
    FileService fileService = new FileService();

    public EditClassPage()
    {
        InitializeComponent();
        LoadClasses();
        studentsList.ItemsSource = schoolClass.Students;
    }

    void LoadClasses()
    {
        classPicker.ItemsSource = fileService.GetClasses();
    }

    private void OnClassSelected(object sender, EventArgs e)
    {
        if (classPicker.SelectedItem == null)
            return;

        string className = classPicker.SelectedItem.ToString();

        schoolClass = fileService.LoadClass(className);

        classNameEntry.Text = className;

        studentsList.ItemsSource = null;
        studentsList.ItemsSource = schoolClass.Students;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nameEntry.Text) ||
            string.IsNullOrWhiteSpace(numberEntry.Text))
            return;

        int number = int.Parse(numberEntry.Text);

        if (schoolClass.Students.Any(s => s.Number == number))
        {
            await DisplayAlert("Błąd", "Numer już istnieje w tej klasie", "OK");
            return;
        }

        schoolClass.Students.Add(new Students
        {
            Name = nameEntry.Text,
            Number = number
        });

        studentsList.ItemsSource = null;
        studentsList.ItemsSource = schoolClass.Students;

        nameEntry.Text = "";
        numberEntry.Text = "";
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var selected = studentsList.SelectedItem as Students;

        if (selected == null)
        {
            await DisplayAlert("Błąd", "Najpierw wybierz ucznia", "OK");
            return;
        }

        schoolClass.Students.Remove(selected);

        studentsList.ItemsSource = null;
        studentsList.ItemsSource = schoolClass.Students;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(classNameEntry.Text))
            return;

        string className = classNameEntry.Text;

        if (schoolClass.ClassName != className &&
            fileService.ClassExists(className))
        {
            await DisplayAlert("Błąd", "Klasa o tej nazwie już istnieje", "OK");
            return;
        }

        schoolClass.ClassName = className;

        fileService.SaveClass(schoolClass);

        await DisplayAlert("OK", "Klasa zapisana", "OK");
    }
}