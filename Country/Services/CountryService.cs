using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Country.Model;
using System.Threading.Tasks;

namespace Country.Services
{
    internal class CountryService
    {
        // Создаем список для хранения данных из источника
        List<Model.Country> CountryList = new();
        // Метод GetCountry() служит для извлечения и сруктурирования данных
        // в соответсвии с существующей моделью данных
        public async Task<IEnumerable<Model.Country>> GetCountry()
        {
            // Если список содержит какие-то элементы
            // то вернется последовательность с содержимым этого списка
            if (CountryList?.Count > 0)
                return CountryList;
            // В данном блоке кода осуществляется подключение, чтение
            // и дессериализация файла - источника данных
            using var stream = await FileSystem.OpenAppPackageFileAsync("Countries.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            CountryList = JsonSerializer.Deserialize<List<Model.Country>>(contents);
            return CountryList;
        }
    }
}
