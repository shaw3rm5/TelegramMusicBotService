using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramMusicBotService.Logic;

public partial class UpdateHandler: IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger _logger;

    public UpdateHandler(ITelegramBotClient botClient, ILogger logger)
    {
        _botClient = botClient;
        _logger = logger;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient _botClient, Update update,
        CancellationToken cancellationToken)
    {

        InlineKeyboardMarkup inlineKeyboardButtons = new([
            [
                InlineKeyboardButton.WithCallbackData("искать музыку", "sosat-xui"),
            ],
            [
                InlineKeyboardButton.WithCallbackData("1234", "sat-xu3")
            ]
            
        ]);
        if (update.Message.Text is { } message)
        {

            Message sentMessage = await (message.Split(" ")[0] switch
            {
                "/start" => SendWelcomeMessage(update.Message),
                "/search" => SearchSong(update.Message),
                _ => throw new ArgumentOutOfRangeException()
            });
            
            _logger.LogInformation($"message was sent from: {sentMessage.From.Id}");
        }
        
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource errorSource, CancellationToken cancellationToken)
    {
        
        return Task.CompletedTask;
    }
    
}