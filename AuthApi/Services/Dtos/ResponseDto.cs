namespace AuthApi.Services.Dtos
{
    public class ResponseDto
    {
        public string Message { get; set; }

        public object Value { get; set; }
    }

    public class LoginResponseDto : ResponseDto
    {
        public object Token { get; set; }

    }


}
