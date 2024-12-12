namespace TelegramMusicBotService;


public class TelegramToken
{
    private readonly IConfiguration _configuration;

    public TelegramToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string ReturnToken()
    {
        return _configuration["MyBotSettings:BotToken"]!;
    }
}