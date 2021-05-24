using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Threading;

namespace SeleniumTest
{
    public class Tests
    {
        IWebDriver driver;
        
        [SetUp]
        public void SetupBrowser()
        {
            driver = new ChromeDriver("C:\\selenium");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void GoogleSearchTest()
        {
            const string expectedText = "SMARTIZ 2020 - jelentkezés 10. évfolyamos középiskolás";

            driver.Url = "https://www.google.com";
            Thread.Sleep(3000);

            IWebElement agreeButton = driver.FindElement(By.XPath("//*[text()='Egyetértek']"));
            agreeButton.Click();
            Thread.Sleep(3000);

            IWebElement searchText = driver.FindElement(By.CssSelector("[name = 'q']"));
            searchText.SendKeys("Smartiz");
            Thread.Sleep(3000);

            IWebElement searchButton = driver.FindElement(By.XPath("//input[@name='btnK']"));
            searchButton.Click();
            Thread.Sleep(3000);

            IList<IWebElement> H3Elements = driver.FindElements(By.XPath("//h3"));
            foreach (IWebElement elem in H3Elements)
            {
                if (elem.Text.Contains(expectedText))
                    return;
            }
            Assert.Fail("Searched element was not found:" + expectedText);
        }

        [Test]
        public void SmartizPageTest_signupButtons()
        {
            const string page = "http://nokatud.hu/smartizgirls/";
            const string expectedText = "Köszönjük az érdeklõdést, de az idei SMARTIZ programra jelentkezési határidõ lejárt.";

            driver.Url = page;
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[text()='Értem']")).Click();
            Thread.Sleep(1000);

            IList<IWebElement> signupButtons = driver.FindElements(By.XPath("//span[text()='JELENTKEZEM!']"));
            for (int i=0; i< signupButtons.Count; i++)
            {
                openOnNewTab(signupButtons[i]);
                verifyOpenedPage(expectedText);
            }

        }
        private void openOnNewTab(IWebElement button)
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(button).Perform();
            Thread.Sleep(1000);
            builder.KeyDown(Keys.Control).Click(button).KeyUp(Keys.Control).Build().Perform();
            Thread.Sleep(2000);
        }

        private void verifyOpenedPage(string expectedText)
        {
            int newTabId = driver.WindowHandles.Count - 1;
            SwitchToOpenedTab(newTabId);
            Thread.Sleep(1000);

            IList<IWebElement> observedTexts = driver.FindElements(By.XPath(string.Format("//*[contains(text(),'{0}')]", expectedText)));
            Assert.Greater(observedTexts.Count, 0, string.Format("Expected element is not on the {0} opened tab: {1}", newTabId, expectedText));
            Assert.Less(observedTexts.Count, 2, "To many element on the page!");

            SwichBackToMainTab();
        }

        private void SwitchToOpenedTab(int newTabId)
        {
            driver.SwitchTo().Window(driver.WindowHandles[newTabId]);
            Thread.Sleep(1000);
        }

        private void SwichBackToMainTab()
        {
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            Thread.Sleep(1000);
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Quit();
        }
    }
}