namespace Core.Models.Doctor
{
    public class DayInMonthModel
    {
        public DayInMonthModel(string date, bool isAvailable)
        {
            Date = date;
            IsAvailable = isAvailable;
        }

        public string Date { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }
}
