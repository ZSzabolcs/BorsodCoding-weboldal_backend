namespace AuthApi.Services.Dtos
{
    public class RegisterRequestDto
    {
        public RegisterRequestDto() { }

        public string UserName { get;  set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
    public record LoginRequestDto(string UserName, string Password);

    public record Response(string Message);
}
