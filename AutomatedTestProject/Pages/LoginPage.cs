using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomatedTestProject.Pages
{
    [TestClass]
    public class LoginPage
    {
        private IWebDriver _driver;
        public void Initialize()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public void OpenUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void EndTest()
        {
            _driver.Close();
        }

        public bool FillForm(string name, string email)
        {
            var elementName =_driver.FindElement(By.Id("name"));
            elementName.SendKeys(name);

            var elementEmail =_driver.FindElement(By.Id("email"));
            elementEmail.SendKeys(email);

            var elementPassword =_driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("TestUnique12345!");

            var elementConfirmPassword =_driver.FindElement(By.Id("confirmationPassword"));
            elementConfirmPassword.SendKeys("TestUnique12345!");

            var elementBtnSubmit = _driver.FindElement(By.CssSelector("#registrationForm > fieldset > div.form-actions > button"));
            elementBtnSubmit.Click();

            if (_driver.FindElements(By.CssSelector(@"#user\2e name\2e error")).Any() || _driver.FindElements(By.CssSelector(@"#user\2e email\2e error")).Any())
            {
                return false;
            }

            return true;
        }

        [TestMethod]
        public void LoginMethod()
        {
            Initialize();
            OpenUrl("http://85.93.17.135:9000/user/new");

            var name = @"TestUnique";

            var result = FillForm($"{name}" ,$"{name}@gmail.com");

            while (!result)
            {
                name = $"{name}{DateTime.Now.Ticks}";

                result = FillForm($"{name}", $"{name}@gmail.com");
            }

            EndTest();

        }
    }
}
