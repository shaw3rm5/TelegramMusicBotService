using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramMusicBotService.Logic;

namespace TelegramMusicBotService;

public class BotService : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<BotService> _logger;
    private readonly UpdateHandler _updateHandler;
    

    public BotService(
        ITelegramBotClient botClient, 
        ILogger<BotService> logger,
        UpdateHandler updateHandler)
    {
        _botClient = botClient;
        _logger = logger;
        _updateHandler = updateHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = [
                UpdateType.Message,
                UpdateType.InlineQuery,
                UpdateType.CallbackQuery,
            ] 
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await _botClient.ReceiveAsync(
                updateHandler: _updateHandler,
                receiverOptions: receiverOptions, 
                cancellationToken: stoppingToken
                );
        }
    }
}

