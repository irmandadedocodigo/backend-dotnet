namespace IrmandadeDoCodigo.Hub.Api
{
    public static class Configuration
    {
        public static string JwtKey { get; set; } = "$2a$12$8CckRcjb0naFkJ01wara2eB9ld7yg1XWKDqY81IqQLwZUW2GbylJa";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
