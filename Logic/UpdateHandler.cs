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
        cancellationToken.ThrowIfCancellationRequested();

        await (update switch
        {
            { Message: { } message } => OnMessage(message),
            {CallbackQuery: {} callbackQuery} => OnCallbackQuery(callbackQuery)
        });

    }
    
    
    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource errorSource, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task OnMessage(Message message)
    {
        
        var commandText = message.Text.Split(' ');
        var songSegment = new ArraySegment<string>(
                commandText, 1, 
                commandText.Length - 1).ToArray();
        
        
        Message sentMessage = await (commandText[0] switch
            {
                "/start" => SendWelcomeMessage(message),
                "/search" => SearchSong(message, string.Join(' ', songSegment)),
                _ => throw new ArgumentOutOfRangeException()
            });
                    
        _logger.LogInformation($"message was sent from: {sentMessage.From.Username}");
    }

    private async Task OnCallbackQuery(CallbackQuery callbackQuery)
    {
        var data = callbackQuery.Data.Split('_');
        await (data[0] switch
        {
            "download" => SendMusic(callbackQuery, data[1])
        });
    }

    private async Task SendMusic(CallbackQuery callbackQuery, string musicUrl)
    {
        
        //idea: realise "add to playlist" button when song is sent 
        
        
        await _botClient.AnswerCallbackQueryAsync(
            callbackQuery.Id
        );
        
        await _botClient.SendMessage(callbackQuery.From.Id, "music will be soon...");
        
    }
    
    
}