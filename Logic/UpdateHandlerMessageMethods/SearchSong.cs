using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramMusicBotService.Logic;

public partial class UpdateHandler
{
    public async Task<Message> SearchSong(Message message)
    {
        return await _botClient.SendMessage(message.From.Id, "partial");
    }
}