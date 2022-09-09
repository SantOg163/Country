using Country.ViewModel;

namespace Country;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		BindingContext = new CountryViewModel();
	}
}

