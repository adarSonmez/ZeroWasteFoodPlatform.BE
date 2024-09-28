using Core.Domain.Abstract;

namespace Domain.DTOs.Analytics
{

    public class ReportGetDto : IDto
    {
        public Guid Id { get; set; }

        public string ReportName { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string Manufacturer { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int SuppliedAmount { get; set; }

        public int SoldAmount { get; set; }
    }
}