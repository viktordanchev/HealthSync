namespace Core.Models.Doctor
{
    public class DayOfWeekModel
    {
        public DayOfWeekModel(string date, bool isWorkDay)
        {
            Date = date;
            IsWorkDay = isWorkDay;
        }

        public string Date { get; set; } = null!;

        public bool IsWorkDay { get; set; }
    }
}
