using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestsSelenium
{
    public class TestsWebSiteSeleniumPractice
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        private By emailInputLocator = By.Name("email");
        private By sendButtonLocator = By.Id("sendMe");
        private By emailResultTextLocator = By.ClassName("your-email");
        private By anotherEmailLinkLocator = By.Id("anotherEmail");
        private By formErrorLocator = By.ClassName("form-error");
        private By resultTextLocator = By.ClassName("result-text");
        private By boyLocator = By.Id("boy");
        private By girlLocator = By.Id("girl");
        private string emailCorrect = "test@test.ru";
        [Test]
        public void ComputerSite_FillFormWithEmail_Success()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys(emailCorrect);
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual(emailCorrect, driver.FindElement(emailResultTextLocator).Text, "Сделали заявку не на тот e-mail");
        }
        [Test]
        public void ComputerSite_FillFormWithDoubleEmail_ErrorInvalidEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys(emailCorrect + emailCorrect);
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Некорректный email", driver.FindElement(formErrorLocator).Text, "Отсутствует сообщение ,,Некорректный email,, при введении двух e-mail");
        }
        [Test]
        public void ComputerSite_EmailInputIsEmpty_ErrorInvalidEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
         //driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Введите email", driver.FindElement(formErrorLocator).Text, "Отсутствует сообщение о необходимости ввести e-mail");
        }
        [Test]
        public void ComputerSite_CheckedGirlRadiobutton_SendGirlName()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys(emailCorrect);
            driver.FindElement(girlLocator).Click();
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Хорошо, мы пришлём имя для вашей девочки на e-mail:", driver.FindElement(resultTextLocator).Text, "Отправилось письмо c именем мальчика при выборе девочки/не отправилось письмо");
        }
        [Test]
        public void ComputerSite_CheckedBoyRadiobutton_SendBoyName()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys(emailCorrect);
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Хорошо, мы пришлём имя для вашего мальчика на e-mail:", driver.FindElement(resultTextLocator).Text, "Отправилось письмо с именем девочки при выборе мальчика/не отправилось письмо");
        }
        [Test]
        public void ComputerSite_FillFormWithEmailWithoutTopLevelDomainWithDot_ErrorInvalidEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("test@test.");
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Некорректный email", driver.FindElement(formErrorLocator).Text, "Отправилось присьмо при указании почты с отсутствующим верхним доменным именем с точкой");
        }
        [Test]
        public void ComputerSite_FillFormWithEmailWithoutTopLevelDomainWithoutDot_ErrorInvalidEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("test@test");
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Некорректный email", driver.FindElement(formErrorLocator).Text, "Отправилось присьмо при указании почты с отсутствующим верхним доменным именем без точки");
        }
        [Test]
        public void ComputerSite_FillFormWithEmailWithDot_ErrorInvalidEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("test@test.ru.");
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Некорректный email", driver.FindElement(formErrorLocator).Text, "Отправилось присьмо при указании почты с точкой в конце");
        }
        [Test]
        public void ComputerSite_FillFormWithEmailWithCommaInLocalPart_ErrorInvalidEmail()
        {
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");
            driver.FindElement(emailInputLocator).SendKeys("test,@test.ru");
            driver.FindElement(sendButtonLocator).Click();
            Assert.AreEqual("Некорректный email", driver.FindElement(formErrorLocator).Text, "Отправилось присьмо при указании почты с некорректными спец.символами(запятая)");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
