using Country.View;
using Country.ViewModel;

namespace Country;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new CountryViewModel();
		
	}

	private void AddButton(object sender, EventArgs e)
	{
		NavigationPage.SetHasNavigationBar(this,false);
		Navigation.PushAsync(new AddEditCountry());
	}
}

