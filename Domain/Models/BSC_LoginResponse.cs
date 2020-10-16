namespace evoting.Domain.Models
{
    public class BSC_LoginResponse
    {
        public string Error { get; set; }
        public string Token { get; set; }
        public string Name  { get; set; }
        public string EmailID { get; set; }
        public string Audience { get; set; }
    }
}
