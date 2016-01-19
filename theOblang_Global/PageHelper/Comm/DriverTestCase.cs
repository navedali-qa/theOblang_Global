using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using Selenium;
using theOblang_Global.PageHelper.Comm;
using System.Data;
using OpenQA.Selenium.Chrome;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;


namespace theOblang_Global.PageHelper.Comm
{
    public abstract class DriverTestCase
    {
        private IWebDriver driver;
        private ISelenium selenium;
        public StringBuilder verificationErrors;
        protected string URL;
        public string browserType;
        private XMLParse oWA_XMLData = new XMLParse();

        [TestFixtureSetUp]
        public void SetupTest()
        {
            oWA_XMLData.LoadXML("../../Config/ApplicationSetting.xml");

            URL = oWA_XMLData.getNodeValue("settings/URL/Application");

            browserType = oWA_XMLData.getNodeValue("settings/browserdata/browser");

            if (browserType.ToLower().Equals("firefox") || browserType.ToLower().Equals("ff"))
            {
                driver = new FirefoxDriver();
            }
            else if (browserType.ToLower().Equals("internet explorer") || browserType.ToLower().Equals("ie"))
            {
                driver = new InternetExplorerDriver();
            }
            else if (browserType.ToLower().Equals("chrome"))
            {
                driver = new ChromeDriver(getPathToDrivers());
            }
            else if (browserType.Equals(""))
            {
                driver = new FirefoxDriver();
            }
            else
            {
                driver = new FirefoxDriver();
            }

            driver.Manage().Window.Maximize();

            selenium = new WebDriverBackedSelenium(driver, URL);

            driver.Navigate().GoToUrl(URL);
        }

        public IWebDriver GetWebDriver()
        {
            return driver;
        }

        public ISelenium GetSelenium()
        {
            return selenium;
        }

        [TestFixtureTearDown]
        public void TeardownTest()
        {
            driver.Quit();
        }

        public string GetRandomNumber()
        {
            string number = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return number;
        }

        //Verify the page title
        public void verifyTitle(string title)
        {
            for (int i = 1; i <= 10; i++)
            {
                string text = GetWebDriver().Title;
                if (text.Contains(title) || text.Contains("Index"))
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            Assert.IsTrue(GetWebDriver().Title.Contains(title));
            Thread.Sleep(2000);
        }

        //Get path
        public string getPathToDrivers()
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "Drivers\\";
            return fPath;
        }

        //Login into the application
        public void login(string username,string password)
        {
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            loginHelper.type("username", username);
            loginHelper.type("password", password);
            loginHelper.ClickElement("Login");
        }

        public void logout()
        {
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());
            loginHelper.ClickElement("Logout");
            loginHelper.WaitForTextVisible("Map", 30);
        }

        public void pressEnter()
        {
            Actions builder = new Actions(driver);
            builder.SendKeys(Keys.Enter);
        }
    }  
}
