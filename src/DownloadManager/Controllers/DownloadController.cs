using DownloadManager.Entities;
using DownloadManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DownloadManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly Context _context;

        public DownloadController(Context context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task SubmitDownload(DownloadRequest request)
        {
            Console.WriteLine(request.URL);
        }

        [Authorize]
        [HttpGet]
        public string GetTest()
        {
            return "you are authorized";
        }
    }
}