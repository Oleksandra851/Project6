using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Project6.Data;
using Project6.Models;

namespace Project6.ViewModels
{
    public class ServicesViewModel : BindableObject
    {
        private readonly List<BeautyService> _allServices;
        private ObservableCollection<BeautyService> _filteredServices;

        public ObservableCollection<BeautyService> FilteredServices
        {
            get => _filteredServices;
            set { _filteredServices = value; OnPropertyChanged(); }
        }

        public Command<string> FilterCommand { get; }
        public Command<BeautyService> BookServiceCommand { get; }

        public ServicesViewModel()
        {
            _allServices = SalonRepository.Instance.Services.ToList();
            _filteredServices = new ObservableCollection<BeautyService>(_allServices);

            FilterCommand = new Command<string>(FilterServices);
            BookServiceCommand = new Command<BeautyService>(async (service) => await BookServiceAsync(service));
        }

        private void FilterServices(string categoryStr)
        {
            if (string.IsNullOrEmpty(categoryStr) || categoryStr == "All")
            {
                FilteredServices = new ObservableCollection<BeautyService>(_allServices);
            }
            else if (Enum.TryParse<ServiceCategory>(categoryStr, out var category))
            {
                FilteredServices = new ObservableCollection<BeautyService>(_allServices.Where(s => s.Category == category));
            }
        }

        private async Task BookServiceAsync(BeautyService service)
        {
            if (service == null || Shell.Current?.CurrentPage == null) return;
            var page = Shell.Current.CurrentPage;

            string name = await page.DisplayPromptAsync("Оформлення запису", "Введіть ваше ім'я:", "Далі", "Скасувати", " ");
            if (string.IsNullOrWhiteSpace(name)) return;

            string phone = await page.DisplayPromptAsync("Оформлення запису", "Введіть ваш номер телефону:", "Далі", "Скасувати", "+380");
            if (string.IsNullOrWhiteSpace(phone)) return;

            var matchingMasters = SalonRepository.Instance.Masters.Where(m => m.Category == service.Category).ToList();
            string[] masterOptions = matchingMasters.Count > 0
                ? matchingMasters.Select(m => $"{m.Name} ({m.SpecializationTitle})").ToArray()
                : new[] { "Черговий майстер" };

            string master = await page.DisplayActionSheetAsync("Оберіть майстра:", "Скасувати", null, masterOptions);
            if (master == "Скасувати" || string.IsNullOrEmpty(master)) return;

            var newBooking = new Booking
            {
                ServiceTitle = service.Title,
                CategoryName = service.CategoryName,
                Price = service.Price,
                ClientName = name,
                ClientPhone = phone,
                MasterName = master,
                BookingDate = DateTime.Now.AddDays(1)
            };

            SalonRepository.Instance.AddBooking(newBooking);
            await page.DisplayAlertAsync("Успішно! 🎉", $"Ви успішно записані на «{service.Title}»!\nМайстер: {master}\nПереглянути запис можна у вкладці «Мої Записи».", "OK");
        }
    }
}