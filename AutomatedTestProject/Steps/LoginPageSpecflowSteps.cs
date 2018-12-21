using System;
using System.Linq;
using System.Threading;
using AutomatedTestProject.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AutomatedTestProject.Steps
{
    [Binding]
    public class LoginPageSpecflowSteps
    {
        private IWebDriver _driver;
        private readonly long _ticks = DateTime.Now.Ticks;

        [Given(@"User initialize Driver")]
        public void GivenUserInitializeDriver()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        
        [Given(@"I Navigate to login page")]
        public void GivenINavigateToLoginPage()
        {
            _driver.Navigate().GoToUrl("http://85.93.17.135:9000/user/new");
        }
        
        [When(@"I fill the form with credentials")]
        public void WhenIFillTheFormWithCredentials(Table table)
        {
            var credentials = table.CreateInstance<Credentials>();
            
            var elementName =_driver.FindElement(By.Id("name"));
            elementName.SendKeys($"{DateTime.Now.Ticks}{credentials.Name}");
            Thread.Sleep(2000);

            var elementEmail =_driver.FindElement(By.Id("email"));
            elementEmail.SendKeys($"{DateTime.Now.Ticks}{credentials.Email}");
            Thread.Sleep(2000);

            var elementPassword =_driver.FindElement(By.Id("password"));
            elementPassword.SendKeys(credentials.Password);
            Thread.Sleep(2000);

            var elementConfirmPassword =_driver.FindElement(By.Id("confirmationPassword"));
            elementConfirmPassword.SendKeys(credentials.Password);
            Thread.Sleep(2000);
        }

        [When(@"press login")]
        public void WhenPressLogin()
        {
            var elementBtnSubmit = _driver.FindElement(By.CssSelector("#registrationForm > fieldset > div.form-actions > button"));
            elementBtnSubmit.Click();
        }

        [When(@"I fill the form with credentials that already exists")]
        public void WhenIFillTheFormWithCredentialsThatAlreadyExists(Table table)
        {
            
            var credentials = table.CreateSet<Credentials>().ToList();

            foreach (var credential in credentials)
            {
                var elementName =_driver.FindElement(By.Id("name"));
                elementName.SendKeys($"{_ticks}{credential.Name}");
                Thread.Sleep(2000);

                var elementEmail =_driver.FindElement(By.Id("email"));
                elementEmail.SendKeys($"{_ticks}{credential.Email}");
                Thread.Sleep(2000);

                var elementPassword =_driver.FindElement(By.Id("password"));
                elementPassword.SendKeys(credential.Password);
                Thread.Sleep(2000);

                var elementConfirmPassword =_driver.FindElement(By.Id("confirmationPassword"));
                elementConfirmPassword.SendKeys(credential.Password);
                Thread.Sleep(2000);

                WhenPressLogin();

                if (_driver.FindElements(By.CssSelector("body > div > div > div > a")).Any())
                {
                    var button = _driver.FindElement(By.CssSelector("body > div > div > div > a"));
                    button.Click();
                    Thread.Sleep(2000);
                }
            }
            
        }
        
        [Then(@"the login should be ok")]
        public void ThenTheLoginShouldBeOk()
        {
            Assert.IsTrue(!_driver.FindElements(By.CssSelector(@"#user\2e name\2e error")).Any());
            Assert.IsTrue(!_driver.FindElements(By.CssSelector(@"#user\2e email\2e error")).Any());
        }

        [Then(@"the login should not be ok")]
        public void ThenTheLoginShouldNotBeOk()
        {
            Assert.IsFalse(!_driver.FindElements(By.CssSelector(@"#user\2e name\2e error")).Any());
            Assert.IsFalse(!_driver.FindElements(By.CssSelector(@"#user\2e email\2e error")).Any());
        }

        [Then(@"I should close driver")]
        public void ThenIShouldCloseDriver()
        {
            _driver.Close();
        }
    }
}
