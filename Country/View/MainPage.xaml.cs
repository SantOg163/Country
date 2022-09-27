using Country.View;
using Country.ViewModel;
using System.Text.Json;

namespace Country;

public partial class MainPage : ContentPage
{
	public static CountryViewModel context = new CountryViewModel();
	public MainPage()
	{
		InitializeComponent();
		BindingContext = context;

    }

    private void AddButton(object sender, EventArgs e)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        Navigation.PushAsync(new AddEditCountry(null));
    }

    private void DeleteButton(object sender, EventArgs e)
    {
        context.Countries.Remove(context.SelectedItem);
        Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        var dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0-windows10.0.19041.0\\win10-x64\\AppX", "\\Resources\\Raw\\Countries.json");
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonStr = JsonSerializer.Serialize(context.Countries, options);
        File.WriteAllText(dir, jsonStr);
    }
}

