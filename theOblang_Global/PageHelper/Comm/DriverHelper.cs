using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Threading;
using Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing.Imaging;
using OpenQA.Selenium.Interactions;

namespace theOblang_Global.PageHelper.Comm
{
    public abstract class DriverHelper
    {
        private IWebDriver driver;
        private ISelenium selenium;

        public DriverHelper(IWebDriver idriver)
        {
            driver = idriver;
        }

        public IWebDriver GetWebDriver()
        {
            return driver;
        }

        public By ByLocator(string locator)
        {
            By result = null;

            if (locator.StartsWith("//"))
            {
                result = By.XPath(locator);
            }
            else if (locator.StartsWith("xpath="))
            {
                result = By.CssSelector(locator.Replace("xpath=", ""));
            }
            else if (locator.StartsWith("css="))
            {
                result = By.CssSelector(locator.Replace("css=", ""));
            }
            else if (locator.StartsWith("#"))
            {
                result = By.Name(locator.Replace("#", ""));
            }
            else if (locator.StartsWith("link="))
            {
                result = By.LinkText(locator.Replace("link=", ""));
            }

            else
            {
                result = By.Id(locator);
            }

            return result;
        }

        public void SelectWindow(string title)
        {
            Thread.Sleep(4000);
            string BaseWindow = GetWebDriver().CurrentWindowHandle;
            ReadOnlyCollection<string> handles = GetWebDriver().WindowHandles;
            foreach (string handle in handles)
            {
                if (handle != BaseWindow)
                {
                    GetWebDriver().SwitchTo().Window(handle).Title.Equals(title);
                    break;
                }
            }
        }

        public Boolean isElementPresent(String locator)
        {
            Boolean result = false;
            try
            {
                driver.FindElement(ByLocator(locator));
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public Boolean isElementVisible(String locator)
        {
            Boolean result = false;
            try
            {
                result = driver.FindElement(ByLocator(locator)).Displayed;

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public void WaitForElementPresent(String locator, int timeout)
        {
            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(locator))
                {
                    break;
                }

                try
                {

                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                }
            }
        }

        public void WaitForElementEnabled(String locator, int timeout)
        {

            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(locator))
                {
                    if (driver.FindElement(ByLocator(locator)).Enabled)
                    {
                        break;
                    }
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    // e.printStackTrace();
                }
            }
        }

        public void WaitForElementNotEnabled(String locator, int timeout)
        {

            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(locator))
                {
                    if (!driver.FindElement(ByLocator(locator)).Enabled)
                    {
                        break;
                    }
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {


                }
            }
        }

        public void WaitForElementVisible(String locator, int timeout)
        {

            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(locator))
                {
                    if (driver.FindElement(ByLocator(locator)).Displayed)
                    {
                        break;
                    }
                }
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                }
            }
        }

        public void WaitForElementNotVisible(String locator, int timeout)
        {
            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(locator))
                {
                    if (!driver.FindElement(ByLocator(locator)).Displayed)
                    {
                        break;
                    }
                }
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                }
            }
        }

        public void SendKeys(String locator, String value)
        {
            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement el = GetWebDriver().FindElement(ByLocator(locator));
            el.Clear();
            el.SendKeys(value);
        }

        public void TypeText(String locator, String value)
        {
            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement el = GetWebDriver().FindElement(ByLocator(locator));
            el.SendKeys(value);
        }

        public void Click(String locator)
        {
            this.WaitForElementPresent(locator, 20);
            Boolean x = isElementPresent(locator);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement el = driver.FindElement(ByLocator(locator));
            el.Click();
        }

        public void SelectDropDown(String locator, String targetValue)
        {

            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));

            IWebElement dropDownListBox = driver.FindElement(ByLocator(locator));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByValue(targetValue);
        }

        public String GetText(String locator)
        {
            String value = "";
            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement textval = driver.FindElement(ByLocator(locator));
            value = textval.Text;
            return value;
        }

        public String GetTextFromNonVisibleElement(String locator)
        {
            String value = "";
            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement textval = driver.FindElement(ByLocator(locator));
            value = ((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].innerHTML", textval).ToString();
            return value;
        }

        public String GetValue(String locator)
        {
            String value = "";
            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement textval = driver.FindElement(ByLocator(locator));
            value = textval.GetAttribute("value");
            return value;
        }

        public String GetAtrributeByXpath(String xpath, String attribute)
        {
            String aa = driver.FindElement(By.XPath(xpath)).GetAttribute(attribute);
            //Console.WriteLine("Value of Style" +aa);
            return aa;
        }

        public String GetAtrributeByLocator(String locator, String attribute)
        {
            String aa = driver.FindElement(ByLocator(locator)).GetAttribute(attribute);
            // Console.WriteLine("Value of Style" + aa);
            return aa;
        }

        public bool IsFieldDisabled(string locator, string attribute)
        {
            bool bRetVal = false;

            try
            {
                bRetVal = Convert.ToBoolean(driver.FindElement(ByLocator(locator)).GetAttribute(attribute));
            }
            catch (Exception ex)
            {
            }

            return bRetVal;
        }

        public void SelectDropDownByText(String locator, String targetText)
        {
            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement dropDownListBox = driver.FindElement(ByLocator(locator));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText(targetText);

        }

        //Clear Text box Value
        public void ClearTextBoxValue(String locator)
        {
            this.WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement el = GetWebDriver().FindElement(ByLocator(locator));
            el.Clear();
        }

        //Count number Of Rows
        public int XpathCount(String xPath)
        {
            int count = driver.FindElements(By.XPath(xPath)).Count;
            return count;
        }

        public void ClickViaJavaScript(string locator)
        {
            this.WaitForElementPresent(locator, 20);
            Boolean x = isElementPresent(locator);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement el = driver.FindElement(ByLocator(locator));

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
        }

        public string GetIDByAtttribute(string locator)
        {
            string sRetVal = "";

            IWebElement we = driver.FindElement(By.XPath(locator));
            sRetVal = we.GetAttribute("id");

            return sRetVal;
        }

        //Method to verify text in page source
        public void VerifyPageText(String text)
        {
            Boolean result = GetWebDriver().PageSource.Contains(text);
            Assert.IsTrue(result, "Text String: " + text + "Not Found.");
        }

        //Method to verify text in page source
        public void VerifyPageTextNotAvailable(String text)
        {
            Boolean result = GetWebDriver().PageSource.Contains(text);
            Assert.IsFalse(result, "Text String: " + text + " Found.");
        }

        //Get path
        public string getPath()
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "Upload\\";
            return fPath;
        }

        //Path to store screenshot
        public string getPathToScreenshot()
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "Screenshot\\";
            return fPath;
        }

        //Taking Screenshot
        public void TakeScreenshot(string name)
        {
            string Location = getPathToScreenshot() + name + ".png";
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(Location, ImageFormat.Png);
        }
        
        //Perform Enter event
        public void Enter()
        {
            Actions builder = new Actions(driver);
            builder.SendKeys(Keys.Enter);
        }

        //Double click on the element
        public void DoubleClick(String locator)
        {
            this.WaitForElementPresent(locator, 20);
            Boolean x = isElementPresent(locator);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement el = driver.FindElement(ByLocator(locator));

            Actions builder = new Actions(driver);
            builder.DoubleClick(el).Perform();
        }

        //Mouse Hover
        public void MouseHover(String locator)
        {
            this.WaitForElementPresent(locator, 20);
            Boolean x = isElementPresent(locator);
            Assert.IsTrue(isElementPresent(locator));
            IWebElement el = driver.FindElement(ByLocator(locator));

            Actions builder = new Actions(driver);
            builder.MoveToElement(el).Perform();
        }

        //Scroll down till element
        public void ScrollDown(String locator)
        {
            IWebElement element = driver.FindElement(ByLocator(locator));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(500);
        }

        //Clear Browser Cache
        public void ClearBrowserCache()
        {
            GetWebDriver().Manage().Cookies.DeleteAllCookies(); //delete all cookies
            Thread.Sleep(8000); //wait 5 seconds to clear cookies.
        }

        //Verify the page title
        public void verifyTitle(string title)
        {
            Thread.Sleep(5000);
            string text = GetWebDriver().Title;
            Assert.IsTrue(text.Contains(title));
        }

        //Verify current URL
        public void VerifyCurrentUrl(String url)
        {
            Assert.IsTrue(GetWebDriver().Url.Contains(url));
        }

        //Drag and Drop
        public void DragAndDrop(String Object, String target)
        {
            IWebElement objectElement = driver.FindElement(ByLocator(Object));
            IWebElement targetElement = driver.FindElement(ByLocator(target));

            Actions builder = new Actions(driver);

            IAction dragAndDrop = builder.ClickAndHold(objectElement)
               .MoveToElement(targetElement)
               .Release(targetElement)
               .Build();

            dragAndDrop.Perform();
        }

        //Wait for text present
        public void WaitForTextVisible(String text, int timeout)
        {
            var wait = new WebDriverWait(GetWebDriver(), TimeSpan.FromSeconds(timeout));
            String locator = "//*[contains(text()," + "\"" + text + "\")]";
            wait.Until(driver => GetWebDriver().FindElement(ByLocator(locator)).Displayed);
        }

        //Wait for text present
        public void WaitForElementDisplayed(String locator, int timeout)
        {
            var wait = new WebDriverWait(GetWebDriver(), TimeSpan.FromSeconds(timeout));
            wait.Until(driver => GetWebDriver().FindElement(ByLocator(locator)).Displayed);
        }

        //Click hidden element
        public void ClickHiddenElement(String locator)
        {
            IWebElement element = GetWebDriver().FindElement(ByLocator(locator));
            ((IJavaScriptExecutor)GetWebDriver()).ExecuteScript("arguments[0].click();", element);
        }

        //Wait till loading completed
        public void WaitforLoading(int timeout)
        {
            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        //Wait for text hidden
        public void WaitForTextHide(String text, int timeout)
        {
            Thread.Sleep(2000);
            var wait = new WebDriverWait(GetWebDriver(), TimeSpan.FromSeconds(timeout));
            String locator = "//*[contains(text()," + "\"" + text + "\")]";
            wait.Until(driver => GetWebDriver().FindElement(ByLocator(locator)).Displayed == false);
            Thread.Sleep(2000);
        }

        //Wait for text on page source
        public void WaitForTextInPage(String text, int timeout)
        {
            var wait = new WebDriverWait(GetWebDriver(), TimeSpan.FromSeconds(timeout));
            wait.Until(driver => GetWebDriver().PageSource.Contains(text));
            Assert.IsTrue(GetWebDriver().PageSource.Contains(text));
            Thread.Sleep(1000);
        }

        public void acceptAlert()
        {
            Thread.Sleep(4000);
            try
            {
                GetWebDriver().SwitchTo().Alert().Accept();
            }
            catch (NoAlertPresentException ex)
            {

            }
        }

        public void WaitForAlert()
        {
            Thread.Sleep(3000);
            for (; ; )
            {
                try
                {
                    GetWebDriver().SwitchTo().Alert();
                    break;
                }
                catch(NoAlertPresentException ex)
                {
                    WaitForWorkArround(500);
                }
            }
        }

        public void SwitchFrameToWrite(int iframeindex, String Text)
        {
            GetWebDriver().SwitchTo().Frame(iframeindex);
            GetWebDriver().FindElement(ByLocator("//body")).Clear();
            GetWebDriver().FindElement(ByLocator("//body")).SendKeys(Text);
            GetWebDriver().SwitchTo().DefaultContent();
        }

        public void CheckContent(String locator, String text)
        {
            String value = GetAtrributeByLocator(locator, "value");
            Assert.IsTrue(value.Contains(text));
        }

        public void Upload(String locator, String imagePath)
        {
            if (isElementPresent(locator))
            {
                GetWebDriver().FindElement(ByLocator(locator)).SendKeys(imagePath);
            }
            else
            {
                Console.WriteLine("Element : " + locator + " Not found");
            }
        }

        public void TypeInLastElement(String locator, String text)
        {
            WaitForElementEnabled(locator, 20);
            List<IWebElement> el = new List<IWebElement>(GetWebDriver().FindElements(ByLocator(locator)));
            el[el.Count - 1].Clear();
            el[el.Count - 1].SendKeys(text);
        }

        public void ClickLastElement(string locator)
        {
            WaitForElementEnabled(locator, 20);
            List<IWebElement> el = new List<IWebElement>(GetWebDriver().FindElements(ByLocator(locator)));
            int i = 0;
            foreach (IWebElement element in el)
            {
                if (el[i].Displayed)
                {
                    el[i].Click();
                    break;
                }
                i++;
            }
        }

        public void SelectLastDropdownValue(String locator, String text)
        {
            WaitForElementEnabled(locator, 20);
            List<IWebElement> el = new List<IWebElement>(GetWebDriver().FindElements(ByLocator(locator)));
            int i = 0;
            foreach (IWebElement element in el)
            {
                if (el[i].Displayed)
                {
                    el[i].Click();
                    WaitForElementEnabled("//*[@id='select2-drop']/div/input", 20);
                    GetWebDriver().FindElement(ByLocator("//*[@id='select2-drop']/div/input")).SendKeys(text);
                    String findtext = "//span[contains(text(),'" + text + "')]";
                    GetWebDriver().FindElement(ByLocator(findtext)).Click();
                    break;
                }
                i++;
            }
        }

        public void SetInFile(string file, string data)
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "Upload\\";
            string fileName = fPath + file + ".txt";

            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file
                    Byte[] title = new UTF8Encoding(true).GetBytes(data);
                    fs.Write(title, 0, title.Length);
                    fs.Close();
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }

        public String GetFromFile(string file)
        {
            string Currentpath = Directory.GetCurrentDirectory();
            String[] ab = Currentpath.Split(new string[] { "bin" }, StringSplitOptions.None);
            String a = ab[0];
            String fPath = a + "Upload\\";
            string fileName = fPath + file + ".txt";
            string text = "";

            try
            {
                // Check if file already exists. If yes, delete it. 
                if (File.Exists(fileName))
                {
                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            text = s;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
            return text;
        }
        public void WaitForWorkArround(int timeout)
        {
            Thread.Sleep(timeout);
        }
    }
}
