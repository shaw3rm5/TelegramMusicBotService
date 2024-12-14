using Telegram.Bot;
using TelegramMusicBotService.Logic;

namespace TelegramMusicBotService;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = Host.CreateApplicationBuilder(args);
        var secretJsonPath = Directory.GetCurrentDirectory() + "/config/secrets.json";
        builder.Configuration.AddJsonFile(secretJsonPath, optional: false, reloadOnChange: true);
        
        
        var services = builder.Services;
        services.AddTransient<ILogger, Logger<Program>>();
        services.AddSingleton<TelegramToken>();
        services.AddSingleton<ITelegramBotClient, TelegramBotClient>(sp =>
        {
            var botToken = sp.GetRequiredService<TelegramToken>().GetToken();
            return new TelegramBotClient(botToken);
        });
        services.AddTransient<UpdateHandler>();
        services.AddHostedService<BotService>();

        
        var host = builder.Build();
        host.Run();
    }
}