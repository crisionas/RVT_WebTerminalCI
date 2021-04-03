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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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
            ViewBag.State = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<LoginModel, AuthMessage>());
            var mapper = new Mapper(config);
            var data = mapper.Map<AuthMessage>(model);
            //data.Ip_address = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            data.Time = DateTime.Now;
            try
            {
                var response = await user.Auth(data);
                
                if (response.Status == true)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.SerialNumber, EncryptHelper.HashGen(response.IDVN)),
                        new Claim(ClaimTypes.Name, response.IDVN)
                    };
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, "Auth", "test");
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                        IsPersistent = false,
                    };
                    var userPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);
                    ViewBag.Message = response.Message;
                    ViewBag.State = response.Status;
                }
                else
                {
                    ModelState.AddModelError("", response.Message);
                    ViewBag.Message = response.Message;
                    ViewBag.State = response.Status;
                }

                return View();

            }
            catch
            {
                return View("Error");
            }

        }


        /// <summary>
        /// Registration View
        /// </summary>
        /// <returns></returns>
        public IActionResult Registration()
        {
            ViewBag.State = false;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisterModel model)
        {
            ViewBag.State = false;
            if (ModelState.IsValid)
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
                data.Ip_address = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                data.Phone_Number = model.Phone_Number;
                try
                {

                    var response = await user.Registration(data);


                    if (response.Status == true)
                    {
                        ViewBag.Message = response.Message;
                        ViewBag.State = response.Status;
                    }
                    else
                    { 
                        ModelState.AddModelError("",response.Message);
                        ViewBag.Message = response.Message;
                        ViewBag.State = response.Status;
                    }

                    return View();
                }
                catch
                {
                    return View("Error");
                }
            }

            return View(model);
        }

        /// <summary>
        /// Vote View
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Vote()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Vote(VoteModel vote)
        {
            if (!ModelState.IsValid)
            {
                return View(vote);
            }
            var identity = (ClaimsIdentity)User.Identity;
            var data = new VoteMessage
            {
                IDVN= identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value,
                Party =Convert.ToInt32(vote.Party)
            };
            try
            {
                var response = await user.Vote(data);
                if (response.VoteStatus == true)
                {
                    ViewBag.Message = response.Message;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("",response.Message);
                    ViewBag.Message = response.Message;
                }
                return View();

            }
            catch
            {
                return View("Error");
            }

        }
    }
}
