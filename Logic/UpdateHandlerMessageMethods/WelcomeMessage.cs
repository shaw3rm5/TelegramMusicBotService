using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramMusicBotService.Logic;



public partial class UpdateHandler
{


    private static readonly string WelcomeMessage = 
        """
        Welcome to Music Bot!
        Here you can find all music from Spotify.
        I have 5 commands:
        /start
        /search <name> or <link>
        /createPlaylist <playlist name>
        /deletePlaylist <playlist name>
        /addSong <playlist> <link>
        Remember, all songs are in the Spotify area!                                    
        """;
    
    public async Task<Message> SendWelcomeMessage(Message message)
    {
        Console.WriteLine(WelcomeMessage);
        return await _botClient.SendMessage(
            chatId:message.From.Id, 
            text: WelcomeMessage);
    }
}