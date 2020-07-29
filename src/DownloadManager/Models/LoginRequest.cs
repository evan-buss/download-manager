using System.ComponentModel.DataAnnotations;

namespace DownloadManager.Models
{
    /// <summary>
    /// LoginRequest sent when a user logs in or registers a new account.
    /// </summary>
    public class LoginRequest
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}