using SpotifyExplode;
using SpotifyExplode.Search;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace TelegramMusicBotService.Logic;

public partial class UpdateHandler
{
    private readonly SpotifyClient _spotify = new();
    
    private async Task<Message> SearchSong(Message message, string? trackName )
    {
        if (trackName is "")
            return await _botClient.SendMessage(message.Chat.Id, "Please enter the track name");
        
        
        var tracks = await _spotify.Search.GetTracksAsync(trackName, limit: 20);

        InlineKeyboardMarkup GenerateSongsKeyboard(List<TrackSearchResult> Songs)
        {
            var buttons = Songs
                .Select(song => InlineKeyboardButton.WithCallbackData($"{song.Title}", $"download_{song.Url}"))
                .Chunk(1)
                .Select(chunk => chunk.ToArray())
                .ToArray();
            return new InlineKeyboardMarkup(buttons);
        }

        Message response = await _botClient.SendMessage(message.Chat.Id,
            $"songs with name {trackName}",
            replyMarkup: GenerateSongsKeyboard(tracks));
        return response;

    }
}