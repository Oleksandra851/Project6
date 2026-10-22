using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Project6.Data;
using Project6.Models;

namespace Project6.ViewModels
{
    public class BookingsViewModel : BindableObject
    {
        public ObservableCollection<Booking> Bookings => SalonRepository.Instance.Bookings;
        public Command<string> CancelBookingCommand { get; }

        public BookingsViewModel()
        {
            CancelBookingCommand = new Command<string>(async (id) => await CancelBookingAsync(id));
        }

        private async Task CancelBookingAsync(string bookingId)
        {
            if (string.IsNullOrEmpty(bookingId) || Shell.Current?.CurrentPage == null) return;
            var page = Shell.Current.CurrentPage;

            bool confirm = await page.DisplayAlertAsync("Скасування візиту", "Ви дійсно бажаєте скасувати цей запис?", "Так, скасувати", "Ні");
            if (confirm)
            {
                SalonRepository.Instance.RemoveBooking(bookingId);
                await page.DisplayAlertAsync("Скасовано", "Запис успішно вилучено зі списку.", "OK");
            }
        }
    }
}