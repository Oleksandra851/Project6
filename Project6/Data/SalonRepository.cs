using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Project6.Models;

namespace Project6.Data
{
    public class SalonRepository
    {
        private static SalonRepository? _instance;
        public static SalonRepository Instance => _instance ??= new SalonRepository();

        public ObservableCollection<BeautyService> Services { get; private set; }
        public ObservableCollection<Master> Masters { get; private set; }
        public ObservableCollection<Booking> Bookings { get; private set; }

        private SalonRepository()
        {
            Services = new ObservableCollection<BeautyService>
            {
                // Перукарські послуги
                new BeautyService { Title = "Жіноча стрижка та укладка", Description = "Елегантна стрижка з урахуванням форми обличчя та професійний догляд.", Price = 650, DurationMinutes = 60, Category = ServiceCategory.Hair },
                new BeautyService { Title = "Складне фарбування AirTouch", Description = "М'який і природний перехід кольору без шкоди для волосся.", Price = 2200, DurationMinutes = 180, Category = ServiceCategory.Hair },
                new BeautyService { Title = "Кератинове відновлення волосся", Description = "Глибоке живлення, інтенсивне вирівнювання та дзеркальний блиск.", Price = 1500, DurationMinutes = 120, Category = ServiceCategory.Hair },
                new BeautyService { Title = "Чоловіча класична стрижка", Description = "Сучасна моделювання зачіски та професійний догляд.", Price = 450, DurationMinutes = 45, Category = ServiceCategory.Hair },

                // Нігтьовий сервіс
                new BeautyService { Title = "Манікюр з покриттям Gel-LAK", Description = "Апаратний манікюр, вирівнювання нігтьової пластини та стійкий колір.", Price = 550, DurationMinutes = 90, Category = ServiceCategory.Nails },
                new BeautyService { Title = "Smart-Педикюр з обробкою стоп", Description = "Безпечна та делікатна обробка стоп і пальчиків з оздоровчим ефектом.", Price = 750, DurationMinutes = 90, Category = ServiceCategory.Nails },
                new BeautyService { Title = "Нарощування нігтів (гель/акригель)", Description = "Моделювання довжини та форми нігтів за вашим бажанням.", Price = 900, DurationMinutes = 120, Category = ServiceCategory.Nails },
                new BeautyService { Title = "Художній дизайн нігтів", Description = "Авторський розпис, френч, втирка або інкрустація стразами.", Price = 380, DurationMinutes = 30, Category = ServiceCategory.Nails },

                // Макіяж та візаж
                new BeautyService { Title = "Денний / Nude макіяж", Description = "Легкий макіяж, що підкреслює вашу природну красу та свіжість.", Price = 700, DurationMinutes = 45, Category = ServiceCategory.Makeup },
                new BeautyService { Title = "Вечірній / Смокі макіяж", Description = "Виразний макіяж для важливих подій із високою стійкістю.", Price = 1000, DurationMinutes = 75, Category = ServiceCategory.Makeup },
                new BeautyService { Title = "Ламінування та фарбування брів", Description = "Моделювання, надання об'єму та ідеальної форми бровам.", Price = 450, DurationMinutes = 45, Category = ServiceCategory.Makeup },
                new BeautyService { Title = "Ламінування вій + ботокс", Description = "Довготривалий вигин, насичений чорний колір та живлення вій.", Price = 550, DurationMinutes = 50, Category = ServiceCategory.Makeup },

                // Масаж та релакс
                new BeautyService { Title = "Релаксуючий масаж обличчя", Description = "Знімає напругу, покращує тонус шкіри та лімфодренаж.", Price = 600, DurationMinutes = 45, Category = ServiceCategory.Massage },
                new BeautyService { Title = "Класичний масаж спини та шиї", Description = "Пропрацювання затиснутих м'язів для зняття втоми.", Price = 800, DurationMinutes = 60, Category = ServiceCategory.Massage },
                new BeautyService { Title = "Спортивний інтенсивний масаж", Description = "Глибокий міофасціальний масаж для відновлення після навантажень.", Price = 950, DurationMinutes = 75, Category = ServiceCategory.Massage },
                new BeautyService { Title = "SPA-догляд «Оксамитове тіло»", Description = "Скрабування, зволожувальне обгортання та ароматерапія.", Price = 1300, DurationMinutes = 90, Category = ServiceCategory.Massage }
            };

            Masters = new ObservableCollection<Master>
            {
                new Master { Name = "Олена", Category = ServiceCategory.Hair, SpecializationTitle = "Топ-Майстер перукар-стиліст" },
                new Master { Name = "Максим", Category = ServiceCategory.Hair, SpecializationTitle = "Перукар-модельєр" },
                new Master { Name = "Марія", Category = ServiceCategory.Nails, SpecializationTitle = "Майстер нігтьового сервісу" },
                new Master { Name = "Христина", Category = ServiceCategory.Nails, SpecializationTitle = "Провідний nail-дизайнер" },
                new Master { Name = "Анастасія", Category = ServiceCategory.Makeup, SpecializationTitle = "Візажист-стиліст" },
                new Master { Name = "Софія", Category = ServiceCategory.Makeup, SpecializationTitle = "Бровист-ламімейкер" },
                new Master { Name = "Ігор", Category = ServiceCategory.Massage, SpecializationTitle = "Спортивний реабілітолог" },
                new Master { Name = "Катерина", Category = ServiceCategory.Massage, SpecializationTitle = "SPA-спеціаліст" }
            };

            Bookings = new ObservableCollection<Booking>
            {
                new Booking
                {
                    ServiceTitle = "Манікюр з покриттям Gel-LAK",
                    MasterName = "Марія (Майстер нігтьового сервісу)",
                    ClientName = "Вікторія",
                    ClientPhone = "+380991234567",
                    BookingDate = DateTime.Now.AddDays(1).AddHours(2),
                    CategoryName = "Нігтьовий сервіс",
                    Price = 550
                }
            };
        }

        public void AddBooking(Booking booking) => Bookings.Add(booking);

        public void RemoveBooking(string bookingId)
        {
            var item = Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (item != null) Bookings.Remove(item);
        }
    }
}