namespace WebAPITesting.Dtos.User
{
    public class AuthResponseDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefresToken { get; set; }
    }
}
