using Microsoft.AspNetCore.Mvc;
using ServerBrowser.Data;
using ServerBrowser.Data.Models;
using ServerBrowser.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace ServerBrowser.Controllers
{

    public class ErrorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ErrorController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("404");
            }

            if (statusCode == 500)
            {
                return View("500");
            }

            return View("Error"); // Generic error view
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
