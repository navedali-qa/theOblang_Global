using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theOblang_Global.PageHelper;
using theOblang_Global.PageHelper.Comm;

namespace theOblang_Global.TestScripts
{
    [TestFixture]
    public class _01Login : DriverTestCase
    {
        [Test]
        public void login()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSetting.xml");
            LoginHelper loginHelper = new LoginHelper(GetWebDriver());

            username = oXMLData.getData("settings/credentials", "Username");
            password = oXMLData.getData("settings/credentials", "Password");

            //Verify Page title
            verifyTitle("MAPCITE");

            //Click on Map button
            loginHelper.ClickElement("MapButton");

            //Wait for text in page
            loginHelper.WaitForTextVisible("Log in",30);

            //Enter User name
            loginHelper.type("username", username[0]);

            //Enter Password
            loginHelper.type("password", password[0]);

            //Click on Login Button
            loginHelper.ClickElement("Login");

            //Wait for Home Page
            loginHelper.WaitForWorkArround(3000);

            //Verify page title
            verifyTitle("MAPCITE");

            //Verify User login successfully
            loginHelper.VerifyPageText("Log off");

            //Click on Logout from the application
            loginHelper.ClickElement("Logout");

            //Verify User logout successfully
            loginHelper.WaitForTextVisible("Map", 30);

        }
    }
}
