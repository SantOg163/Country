using Country.ViewModel;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.Text.Json;

namespace Country.View;

public partial class AddEditCountry : ContentPage
{
	public Model.Country _currentCountry = new Model.Country();
	public AddEditCountry(Model.Country selectedCountry)
	{
		InitializeComponent();
        if (selectedCountry != null)
            _currentCountry = selectedCountry;
        BindingContext = _currentCountry;
    }

	private async void AddCountry(object sender, EventArgs e)
    { 
        _currentCountry.Area = Convert.ToInt32(Area.Text);
        _currentCountry.Population = Convert.ToInt32(Population.Text);
        _currentCountry.Name = Name.Text;
        _currentCountry.Image = "noimg.png";
		CountryViewModel countryViewModel = new CountryViewModel();
        StringBuilder errors = new StringBuilder();

        if (!int.TryParse(_currentCountry.Area.ToString(), out int num))
            errors.AppendLine("error: The area must be a number");
        if (!int.TryParse(_currentCountry.Population.ToString(), out int num2))
            errors.AppendLine("error: The population must be a number");
		if(_currentCountry.Name.Any(char.IsDigit))
            errors.AppendLine("error: The name cannot contain numbers");
        if (countryViewModel.Countries.Where(c => c.Name == _currentCountry.Name).Count() != 0)
			errors.AppendLine("error: Such a country already exists");

		if (errors.Length > 0)
		{
			App.Current.MainPage.DisplayAlert("Errors", errors.ToString(), "OK");
			return;
		}

        _currentCountry.Id = await countryViewModel.GetCountryAsync()+1;
        
        Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        var dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0-windows10.0.19041.0\\win10-x64\\AppX", "\\Resources\\Raw\\Countries.json");
        countryViewModel.Countries.Add(_currentCountry);
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonStr = JsonSerializer.Serialize(countryViewModel.Countries, options);
        File.WriteAllText(dir, jsonStr);
        await Navigation.PopAsync();
        MainPage.context.Countries.Add(_currentCountry);
    }

    private async void NewImage_Clicked(object sender, EventArgs e,PickOptions options)
    {
       // openfiledialog нельзя использовать
    }
}