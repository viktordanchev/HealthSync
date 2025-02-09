namespace Core.Models.Account
{
    public class UserDataModel
    {
        public UserDataModel()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; }
    }
}
