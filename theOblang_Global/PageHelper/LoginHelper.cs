using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using theOblang_Global.PageHelper.Comm;
using OpenQA.Selenium.Support.UI;
using theOblang_Global.Locators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace theOblang_Global.PageHelper
{
    public class LoginHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public LoginHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("Login.xml");
        }

        public void type(String field, String Text)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementEnabled(locator, 20);
            WaitForElementDisplayed(locator, 30);
            SendKeys(locator, Text);
        }

        public void ClickElement(string Field)
        {
            String locator = locatorReader.readLocator(Field);
            WaitForElementVisible(locator, 30);
            WaitForElementEnabled(locator, 20);
            Click(locator);
        }

        public void verifyElementPresent(String Field)
        {
            String locator = locatorReader.readLocator(Field);
            WaitForElementPresent(locator, 20);
            Assert.IsTrue(isElementPresent(locator));
        }

        public void verifyfieldText(string field, string text)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementEnabled(locator, 20);
            CheckContent(locator, text);
        }

        public void scrollToElement(string field)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementEnabled(locator, 20);
            ScrollDown(locator);
        }

        public void performClick(string field)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementEnabled(locator, 20);
            ClickViaJavaScript(locator);
        }

        public void uploadImage(string Field, string imagePath)
        {
            String locator = locatorReader.readLocator(Field);
            WaitForElementPresent(locator, 30);
            Upload(locator, imagePath);
        }
    }
}
