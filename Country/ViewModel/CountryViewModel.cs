using Country.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Country.ViewModel
{
    internal class CountryViewModel : BindableObject
    {
        // Переменная для хранения состояния
        // выбранного элемента коллекции
        private Model.Country _selectedItem;
        // Объект с логикой по извлечению данных
        // из источника
        CountryService countryService = new();
        // Коллекция извлекаемых объектов
        public ObservableCollection<Model.Country> Countries { get; } = new ();
        // Конструктор с вызовом метода
        // получения данных
    public CountryViewModel()
    {
        GetCountryAsync();
    }
    // Публичное свойство для представления
    // описания выбранного элемента из коллекции
    public string Desc { get; set; }
    // Свойство для представления и изменения
    // состояния выбранного объекта
    public Model.Country SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            Desc = $"Area: {value?.Area} \nPopulation: {value?.Population}";
            // Метод отвечает за обновление данных
            // в реальном времени
            OnPropertyChanged(nameof(Desc));
        }
        }
        // Метод получения коллекции объектов
        async Task GetCountryAsync()
{
            try
            {
                var country = await countryService.GetCountry();
                if (Countries.Count != 0)
                    Countries.Clear();
                foreach (var c in country)
                {
                    Countries.Add(c);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!", $"Что - то пошло не так: { ex.Message}","OK");
            }
        }
    }
}