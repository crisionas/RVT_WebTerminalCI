using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVT_WebTerminal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RVT_WebTerminal.Services;

namespace RVT_WebTerminal.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Blockchain()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Email-ul nu este valid. Introduceți altă adresă.");
                return View(model);
            }

            TempData["Results"] = "Mesajul a fost transmis cu succes.";
            return View();

        }
    }
}
