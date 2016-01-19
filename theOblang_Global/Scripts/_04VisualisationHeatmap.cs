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
    public class _04VisualisationHeatmap : DriverTestCase
    {
        [Test]
        public void visualisationHeatmap()
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
            bool result = visualisationHelper.verifyText("Heatmap UK Test");
            if (!result)
            {
                LoadHelper loadHelper = new LoadHelper(GetWebDriver());
                loadHelper.ClickElement("Import");
                loadHelper.ClickElement("Next");
                String path = loadHelper.getPath() + "Heatmap Test UK.csv";
                loadHelper.UploadFile("Upload", path);
                loadHelper.WaitForWorkArround(5000);
                loadHelper.ClickElement("ImportHeatmapFile");
                loadHelper.SelectByText("Lattitude", "Latitude");
                loadHelper.SelectByText("Longitude", "Longitude");
                loadHelper.ClickElement("LongNext");
                loadHelper.SelectAllCheck("CheckBox1");
                loadHelper.ClickElement("CheckNext");
                loadHelper.WaitForWorkArround(5000);
                loadHelper.ClickElement("AuthorityCheck");
                loadHelper.acceptAlert();
                loadHelper.ClickElement("AuthNext");
                loadHelper.SelectByText("Numeric", "population");
                loadHelper.ClickElement("NumeNext");
                loadHelper.type("Dataset", "Heatmap UK Test");
                loadHelper.ClickElement("Beginimport");
                loadHelper.WaitForTextHide("Data load in progress...", 60000);
                loadHelper.WaitForTextVisible("Import Data from a flat file currently(.csv or Excel (.xls 97-2003 and .xlsx workbook", 6000);
            }
            

            //Enter Oxfordshire to search
            visualisationHelper.type("SearchBox", "Oxfordshire");
            
            //Click on Search button
            visualisationHelper.ClickElement("SearchButton");

            visualisationHelper.WaitForWorkArround(5000);

            //Click on Clear button
            visualisationHelper.ClickElement("ClearButton");
            
            //Select Heatmap Menu
            visualisationHelper.ClickElement("HeatmapMenu");

            visualisationHelper.WaitForWorkArround(3000);

            //Select Heatmap from dropdown
            visualisationHelper.SelectByText("Heatmapdropdown", "Heatmap UK Test");
            
            //Click on Show heatmap checkbox
            visualisationHelper.ClickElement("HeatCheck");

            visualisationHelper.WaitForWorkArround(5000);

            //Withdrawn menu
            visualisationHelper.ClickElement("HeatmapMenu");
            visualisationHelper.WaitForWorkArround(5000);

            //Take screen shot
            visualisationHelper.TakeScreenshot("HeatmapVisualisation_" + GetRandomNumber());

            //Select Heatmap from dropdown
            visualisationHelper.ClickElement("HeatmapMenu");

            //Uncheck Show heatmap checkbox
            visualisationHelper.ClickElement("HeatCheck");

            visualisationHelper.WaitForWorkArround(5000);
            //Logout from the application
            logout();

        }
    }
}
