﻿using System;
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
    public class LoadHelper : DriverHelper
    {
        public LocatorReader locatorReader;

        public LoadHelper(IWebDriver idriver)
            : base(idriver)
        {
            locatorReader = new LocatorReader("LoadFile.xml");
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

        public void SelectAllCheck(string field)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementEnabled(locator, 20);
            int count = GetWebDriver().FindElements(By.XPath(locator)).Count;
            for (int i = 1; i <= count; i++)
            {
                IWebElement el = GetWebDriver().FindElement(ByLocator(locator + "[" + i + "]/input"));
                el.Click();
            }
        }

        public void UploadFile(String field, String path)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementPresent(locator, 30);
            Upload(locator, path);
        }

        public void SelectByText(String dropdown, String text)
        {
            String locator = locatorReader.readLocator(dropdown);
            WaitForElementPresent(locator, 30);
            SelectDropDownByText(locator, text);
        }

        public int GetXPathCount(String field)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementPresent(locator, 30);
            return XpathCount(locator);
        }

        internal void DeleteData(string field, string text)
        {
            String locator = locatorReader.readLocator(field);
            WaitForElementPresent(locator, 30);
            String locator1 = locator + "/option[text()='" + text + "']";
            WaitForWorkArround(5000);
            if (isElementPresent(locator1))
            {
                SelectByText(field, text);
                Click("//button[@id='Delete']");
                Click("//button[text()='Yes']");
            }
        }

        public void DeleteAll()
        {
            List<IWebElement> el = new List<IWebElement>(GetWebDriver().FindElements(ByLocator("//input[contains(@onclick,'deletefile')]")));
            int i = 0;
            foreach (IWebElement element in el)
            {
                if (el[i].Displayed)
                {
                    el[i].Click();
                }
                else
                {
                    break;
                }
                i++;
            }

        }
    }
}
