using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theOblang_Global.PageHelper;
using theOblang_Global.PageHelper.Comm;
using NUnit.Framework;

namespace theOblang_Global.TestScripts
{
    [TestFixture]
    public class _05VisualizationChoropleth : DriverTestCase
    {
        [Test]
        public void visualizationChoropleth()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSetting.xml");
            VisualisationHelper visualisationHelper = new VisualisationHelper(GetWebDriver());

            username = oXMLData.getData("settings/credentials", "Username");
            password = oXMLData.getData("settings/credentials", "Password");

            //Verify Page title
            verifyTitle("MAPCITE");

            //Click on Map button
            visualisationHelper.ClickElement("MapButton");

            //Wait for text in page
            visualisationHelper.WaitForTextVisible("Log in", 30);

            login(username[0], password[0]);

            //Wait for Home Page
            visualisationHelper.WaitForWorkArround(3000);

            //Verify page title
            verifyTitle("MAPCITE");

            //Verify User login successfully
            visualisationHelper.VerifyPageText("Log off");

            //Verify heatmap available or not
            bool result = visualisationHelper.verifyText("Country Populations");
            if (!result)
            {
                LoadHelper loadHelper = new LoadHelper(GetWebDriver());
                loadHelper.ClickElement("Import");
                loadHelper.ClickElement("Next");
                String path = loadHelper.getPath() + "Choropleth World Countries - False Populations.csv";
                loadHelper.UploadFile("Upload", path);
                loadHelper.WaitForWorkArround(5000);
                loadHelper.ClickElement("ImportPopulationFile");
                loadHelper.WaitForWorkArround(5000);
                loadHelper.SelectByText("Lattitude", "latitude");
                loadHelper.SelectByText("Longitude", "longitude");
                loadHelper.ClickElement("LongNext");
                loadHelper.SelectAllCheck("CheckBox1");
                loadHelper.ClickElement("CheckNext");
                loadHelper.ClickElement("AuthNext");
                loadHelper.SelectByText("Numeric", "Flse Population Pos");
                loadHelper.ClickElement("NumeNext");
                loadHelper.type("Dataset", "Country Populations");
                loadHelper.ClickElement("Beginimport");
                loadHelper.WaitForTextHide("Data load in progress...", 60000);
                loadHelper.WaitForTextVisible("Import Data from a flat file currently(.csv or Excel (.xls 97-2003 and .xlsx workbook", 6000);
            }


            //Enter Europe to search
            visualisationHelper.type("SearchBox", "Europe");

            //Click on Search button
            visualisationHelper.ClickElement("SearchButton");

            visualisationHelper.WaitForWorkArround(5000);

            //Click on Clear button
            visualisationHelper.ClickElement("ClearButton");

            //Select Choropleth Menu
            visualisationHelper.ClickElement("ChoroplethMenu");

            visualisationHelper.WaitForWorkArround(3000);

            //Select country population from the dropdown
            visualisationHelper.SelectByText("CountryDropdown", "Country Populations");

            //Select Word Borders in Shapeset dropdown
            visualisationHelper.SelectByText("Shapeset", "World Borders");

            //Check the Choropleth checkbox
            visualisationHelper.ClickElement("ChoroplethCheck");

            //Select “OK” to the message and wait
            visualisationHelper.WaitForWorkArround(3000);
            visualisationHelper.acceptAlert();

            //Wait for the process to complete
            visualisationHelper.WaitForTextVisible("Choropleth Loaded", 60000);

            //Select the choropleth menu button to withdraw the menu
            visualisationHelper.ClickElement("ChoroplethMenu");

            visualisationHelper.WaitForWorkArround(5000);

            //Take a screenshot of the active window
            visualisationHelper.TakeScreenshot("Choropleth_" + GetRandomNumber());

            //Select the choropleth menu button to withdraw the menu
            visualisationHelper.ClickElement("ChoroplethMenu");

            //Uncheck the Choropleth checkbox
            visualisationHelper.ClickElement("ChoroplethCheck");

            //Logout from the application
            logout();

        }
    }
}
