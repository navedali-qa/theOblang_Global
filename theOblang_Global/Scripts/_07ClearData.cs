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
    public class _07ClearData : DriverTestCase
    {
        [Test]
        public void clearData()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSetting.xml");
            LoadHelper loadHelper = new LoadHelper(GetWebDriver());

            username = oXMLData.getData("settings/credentials", "Username");
            password = oXMLData.getData("settings/credentials", "Password");

            //Verify Page title
            verifyTitle("MAPCITE");

            //Click on Map button
            loadHelper.ClickElement("MapButton");

            //Wait for text in page
            loadHelper.WaitForTextVisible("Log in", 30);

            login(username[0], password[0]);

            //Wait for Home Page
            loadHelper.WaitForWorkArround(3000);

            //Verify page title
            verifyTitle("MAPCITE");

            //Verify User login successfully
            loadHelper.VerifyPageText("Log off");

            //Remove Dataset
            loadHelper.ClickElement("Admin");

            loadHelper.DeleteData("DataDrop", "Heatmap UK Test");
            loadHelper.DeleteData("DataDrop", "Country Populations");
            loadHelper.DeleteData("DataDrop", "AnalyticsResults1");

            //Select import from menu
            loadHelper.ClickElement("Import");

            //Click on next button
            loadHelper.ClickElement("Next");

            loadHelper.WaitForWorkArround(4000);

            loadHelper.DeleteAll();

            //Logout from the application
            logout();
     
        }
    }
}
