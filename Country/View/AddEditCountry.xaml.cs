using Country.ViewModel;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.Text.Json;

namespace Country.View;

public partial class AddEditCountry : ContentPage
{
	public Model.Country _newCountry = new Model.Country();
	public AddEditCountry()
	{
		InitializeComponent();
        BindingContext = _newCountry;
    }

	private async void AddCountry(object sender, EventArgs e)
    { 
        _newCountry.Area = Convert.ToInt32(Area.Text);
        _newCountry.Population = Convert.ToInt32(Population.Text);
        _newCountry.Name = Name.Text;
		CountryViewModel countryViewModel = new CountryViewModel();
        StringBuilder errors = new StringBuilder();

        if (!int.TryParse(_newCountry.Area.ToString(), out int num))
            errors.AppendLine("error: The area must be a number");
        if (!int.TryParse(_newCountry.Population.ToString(), out int num2))
            errors.AppendLine("error: The population must be a number");
		if(_newCountry.Name.Any(char.IsDigit))
            errors.AppendLine("error: The name cannot contain numbers");
        if (countryViewModel.Countries.Where(c => c.Name == _newCountry.Name).Count() != 0)
			errors.AppendLine("error: Such a country already exists");

		if (errors.Length > 0)
		{
			App.Current.MainPage.DisplayAlert("Errors", errors.ToString(), "OK");
			return;
		}

        _newCountry.Id = await countryViewModel.GetCountryAsync()+1;
        
        Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        var dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0-windows10.0.19041.0\\win10-x64\\AppX", "\\Resources\\Raw\\Countries.json");
        countryViewModel.Countries.Add(_newCountry);
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonStr = JsonSerializer.Serialize(countryViewModel.Countries, options);
        File.WriteAllText(dir, jsonStr);
        await Navigation.PopAsync();

    }

    private async void NewImage_Clicked(object sender, EventArgs e,PickOptions options)
    {
       // openfiledialog нельзя использовать
    }
}