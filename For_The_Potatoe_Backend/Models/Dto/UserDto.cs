namespace For_The_Potatoe_Backend.Models.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
