namespace campusuno.API.DTOs
{
    public class AuthenticationResponseDTO
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
