using Microsoft.VisualStudio.TestTools.UnitTesting;
using RVT_WebTerminal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_WebTerminal.Controllers.Tests
{
    [TestClass()]
    public class AuthControllerTests
    {
        [TestMethod()]
        public void RegistrationTest()
        {
            var controller = new AuthController();
            var model = new RegisterModel
            {
                Name = "Ionas",
                Surname = "Cristian",
                Birth_date = "01.01.1998",
                IDNP = "12345678",
                Email = "cris.ionas@gmail.com",
                Gender = "Male",
                Phone_Number = "12345678",
                Region = "Ialoveni"
            };

            var response = controller.Registration(model);

            Assert.IsNotNull(response);
        }
    }
}