using System;

namespace Project6.Models
{
    public enum ServiceCategory
    {
        Hair,       
        Nails,      
        Makeup,     
        Massage     
    }

    public class BeautyService
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }
        public ServiceCategory Category { get; set; }

        public string CategoryName => Category switch
        {
            ServiceCategory.Hair => "Перукарські послуги",
            ServiceCategory.Nails => "Нігтьовий сервіс",
            ServiceCategory.Makeup => "Макіяж та візаж",
            ServiceCategory.Massage => "Масаж та релакс",
            _ => "Загальні послуги"
        };

        public string FormattedPrice => $"{Price:F0} грн";
        public string FormattedDuration => $"{DurationMinutes} хв";
    }

    public class Master
    {
        public string Name { get; set; } = string.Empty;
        public ServiceCategory Category { get; set; }
        public string SpecializationTitle { get; set; } = string.Empty;
    }

    public class Booking
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ServiceTitle { get; set; } = string.Empty;
        public string MasterName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientPhone { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string FormattedDate => BookingDate.ToString("dd.MM.yyyy HH:mm");
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string FormattedPrice => $"{Price:F0} грн";
    }
}