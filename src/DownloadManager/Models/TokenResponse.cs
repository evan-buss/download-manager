namespace DownloadManager.Controllers
{
    /// <summary>
    /// TokenResponse is sent when a user successfully authenticates
    /// </summary>
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}