using SpotifyExplode;
using TagLib;
using File = System.IO.File;

namespace TelegramMusicBotService;

public class SpotifyDownloader
{
    private readonly SpotifyClient _spotifyClient = new();
    private readonly HttpClient _httpClient = new();

    private string? _downloadUrl;
    private string _songPath = $"{Directory.GetCurrentDirectory()}/musicBuffer/{Guid.NewGuid().ToString()}.mp3";
    private string _songPhoto = $"{Directory.GetCurrentDirectory()}/musicBuffer/{Guid.NewGuid().ToString()}.jpeg";


    public void SetDownloadUrl(string? downloadUrl)
    {
        _downloadUrl = downloadUrl;
    }

    public async Task<string> DownloadFileAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                byte[] fileBytes = await client.GetByteArrayAsync(_downloadUrl);
                
                await File.WriteAllBytesAsync(_songPath, fileBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return _songPath;
    }


    public string ExtractAndSaveCover()
    {
        try
        {
            var file = TagLib.File.Create(_songPath);

            IPicture? picture = file.Tag.Pictures.Length > 0 ? file.Tag.Pictures[0] : null;
            
            File.WriteAllBytes(_songPhoto, picture.Data.Data);
            Console.WriteLine($"Saved cover to {_songPhoto}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        return _songPhoto;  
    }
}