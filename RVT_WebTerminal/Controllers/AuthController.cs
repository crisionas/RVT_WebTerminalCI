using AutoMapper;
using BusinessLayer;
using BusinessLayer.Helpers;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RVT_WebTerminal.Models;
using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.UserIdentity;
using RVTLibrary.Models.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RVT_WebTerminal.Controllers
{
    public class AuthController : Controller
    {
        public IUser user;
        public AuthController()
        {
            var bl = new BusinessManager();
            user = bl.GetUser();
        }

        /// <summary>
        /// Login View
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<LoginModel, AuthMessage>());
            var mapper = new Mapper(config);
            var data = mapper.Map<AuthMessage>(model);

            // TREBUIE DE SCHIMBAT!!!


            TempData["pass"] = data.VnPassword;

            var response = await user.Auth(data);
            if (response.Status == true)
            {
                var authclaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.SerialNumber, EncryptHelper.HashGen(response.IDVN)),
                    new Claim(ClaimTypes.Name, data.IDNP)
                };

                var authIdentity = new ClaimsIdentity(authclaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { authIdentity });
                await HttpContext.SignInAsync(userPrincipal);
                TempData["AlertMessage"] = response.Message;
                return RedirectToAction("Vote", "Auth");
            }
            else
            {
                TempData["AlertMessage"] = response.Message;
                return View();
            }
        }


        /// <summary>
        /// Registration View
        /// </summary>
        /// <returns></returns>
        public IActionResult Registration()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisterModel model)
        {
            var data = new RegistrationMessage();
            data.IDNP = model.IDNP;
            data.Name = model.Name;
            data.Surname = model.Surname;
            data.Gender = model.Gender;
            data.Region = model.Region;
            data.Email = model.Email;
            data.Birth_date = DateTime.Parse(model.Birth_date);
            data.RegisterDate = DateTime.Now;
            //data.Ip_address = Request.HttpContext.Connection.RemoteIpAddress.ToString(); ;
            data.Phone_Number = model.Phone_Number;

            var response = await user.Registration(data);

            if (response.Status == true)
            {
                TempData["AlertMessage"] = response.Message;
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                TempData["AlertMessage"] = response.Message;
                return RedirectToAction("Index", "Home");
            }


        }

        /// <summary>
        /// Vote View
        /// </summary>
        /// <returns></returns>
        public IActionResult Vote()
        {
            return View();
        }

        public async Task<ActionResult> Vote(VoteModel vote)
        {
            var config = new MapperConfiguration(cfg =>
cfg.CreateMap<VoteModel, VoteMessage>());
            var mapper = new Mapper(config);
            var model = mapper.Map<VoteMessage>(vote);

            var pass = TempData["pass"];
            var identity = (ClaimsIdentity)User.Identity;
            
            var response = await user.Vote(model);
            if (response.VoteStatus == true)
            {
                TempData["AlertMessage"] = response.Message;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["AlertMessage"] = response.Message;
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}
