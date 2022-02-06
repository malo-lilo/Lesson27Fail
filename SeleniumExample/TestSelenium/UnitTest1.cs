using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium
{

    public class Tests
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private string login, passwod;
        private bool isAuth = false;
        
        const string Url = "https://old.kzn.opencity.pro/";
        
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            (login, passwod) = Utils.ClientRegistration(driver, wait);
        }

       [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

       /* [Test]
        public void CheckAuthTesting()
        {
            Authorization(Url);
            Assert.AreEqual("https://old.kzn.opencity.pro/cabinet/", driver.Url, "Не перешли в личный кабинет");
            isAuth = true;
        }*/

        [Test]
        public void ChangeProfileDataTesting()
        {
            Authorization(Url);
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/cabinet/myprofile");
            
            IWebElement inputLastname = driver.FindElement(By.XPath("//*[@id='contentArea']/div/div[2]/div[1]/form/div/div[1]/div[2]/input"));
            inputLastname.SendKeys("Farhullina");
            IWebElement inputName = wait.Until(e => e.FindElement(By.XPath("//*[@data-ui='name']")));
            inputName.SendKeys("Leyla");
            IWebElement inputPatronymic = wait.Until(e => e.FindElement(By.XPath("//*[@data-ui='patronymic']")));
            inputPatronymic.SendKeys("Ilshatovna");
            IWebElement inputPhone = wait.Until(e => e.FindElement(By.XPath("//*[@data-ui='phone']")));
            inputPhone.SendKeys("89274342764");
            driver.FindElement(By.XPath("//*[@data-ui='formBtn']")).Click();
            
            
        }

       /* [Test]
        public void CheckMyProfileTesting()
        {
            if (!isAuth) Authorization(Url);
            driver.FindElement(By.CssSelector("#profile_data > div:nth-child(1) > div.username.active > a")).Click();
            Assert.AreEqual("https://old.kzn.opencity.pro/cabinet/myprofile", driver.Url, "Не открылась страница редактирования профиля");
        }*/
        
        private void Authorization(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            var auth = driver.FindElement(By.XPath("//a[@data-ui='auth']"));
            auth.Click();
            IWebElement inputEmail = wait.Until(e => e.FindElement(By.Name("username")));
            inputEmail.SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(passwod);
            driver.FindElement(By.CssSelector("button.inputSubmit")).Click();
        }


    }
}