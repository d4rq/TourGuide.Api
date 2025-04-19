namespace TourGuide.Api.Web.Services;

public class EmailOptions
{
    public string Host { get; set; }
    
    public int Port { get; set; }
    
    public string Login { get; set; }
    
    public string Password { get; set; }

    public EmailOptions(string host, int port, string login, string password)
    {
        Host = host;
        Port = port;
        Login = login;
        Password = password;
    }
    
    public EmailOptions() {}
}