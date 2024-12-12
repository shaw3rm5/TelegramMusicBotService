using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramMusicBotService.Logic;



public partial class UpdateHandler
{

    private static string[] WelcomeMessage =
    {
        "<h1>Welcome to Music Bot!</h1>",
        "<p>Here you can find all music from Spotify.</p>",
        "<h2>I have 5 commands:</h2>",
        "<ul>",
        "  <li><b>/start</b></li>",
        "  <li><b>/search</b> &lt;'name'&gt; or &lt;link&gt;</li>",
        "  <li><b>/createPlaylist</b> &lt;playlist name&gt;</li>",
        "  <li><b>/deletePlaylist</b> &lt;playlist name&gt;</li>",
        "  <li><b>/addSong</b> &lt;playlist&gt; &lt;link&gt;</li>",
        "</ul>",
        "<p><i>Remember, all songs are in the Spotify area!</i></p>",
    };

    private string combinedMessage = string.Join("\n", WelcomeMessage);
        
        
    public async Task<Message> SendWelcomeMessage(Message message)
    {
        return await _botClient.SendMessage(message.From.Id, text: combinedMessage, ParseMode.Html );
    }
}