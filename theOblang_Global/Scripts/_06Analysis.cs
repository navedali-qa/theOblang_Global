using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theOblang_Global.PageHelper;
using theOblang_Global.PageHelper.Comm;
using OpenQA.Selenium;
using NUnit.Framework;

namespace theOblang_Global.TestScripts
{
    [TestFixture]
    public class _06Analysis : DriverTestCase
    {
        [Test]
        public void analysis()
        {
            PathToDownloads pathToDownloads = new PathToDownloads();

            
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSetting.xml");
            AnalysisHelper analysisHelper = new AnalysisHelper(GetWebDriver());

            username = oXMLData.getData("settings/credentials", "Username");
            password = oXMLData.getData("settings/credentials", "Password");

            //Verify Page title
            verifyTitle("MAPCITE");

            //Click on Map button
            analysisHelper.ClickElement("MapButton");

            //Wait for text in page
            analysisHelper.WaitForTextVisible("Log in", 30);

            login(username[0], password[0]);

            //Wait for Home Page
            analysisHelper.WaitForWorkArround(3000);

            //Verify page title
            verifyTitle("MAPCITE");

            //Verify User login successfully
            analysisHelper.VerifyPageText("Log off");
            
            //Verify heatmap available or not
            bool result = analysisHelper.verifyText("Heatmap UK Test");
            if (!result)
            {
                LoadHelper loadHelper = new LoadHelper(GetWebDriver());
                loadHelper.ClickElement("Import");
                loadHelper.ClickElement("Next");
                String path = loadHelper.getPath() + "Heatmap Test UK.csv";
                loadHelper.UploadFile("Upload", path);
                loadHelper.WaitForWorkArround(5000);
                loadHelper.ClickElement("ImportHeatmapFile");
                loadHelper.WaitForWorkArround(4000);
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

            //Remove Dataset
            analysisHelper.ClickElement("AdminMenu");

            analysisHelper.DeleteData("DataDrop", "AnalyticsResults1");
            
            //Enter Aberdeen,uk to search
            analysisHelper.type("SearchBox", "Aberdeen,uk");

            //Click on Search button
            analysisHelper.ClickElement("SearchButton");
            
            //Select the dataset menu
            analysisHelper.ClickElement("DatasetMenu");
            
            analysisHelper.WaitForWorkArround(5000);

            //Select the left hand pin from the two that are visible
            analysisHelper.ClickByIndex("ShowDataSet");

            analysisHelper.WaitForWorkArround(5000);
            
            //Click on Left pin
            analysisHelper.ClickByIndex("LeftPin");

            //Selecting the pin will bring up a modal box
            analysisHelper.WaitForTextVisible("Marker Inspector", 6000);

            //Select the Analytics Tab
            analysisHelper.ClickElement("AnalyticsTab");

            //Select the Analysis Tab
            analysisHelper.ClickElement("AnalysisTab");

            //Select JulianTest1 from the Saved Formula dropdown
            analysisHelper.SelectByText("LoadFormula", "JulianTest1");

            //press the Load button
            analysisHelper.ClickElement("Load");

            //Select the Run button from lower down in the modal box
            analysisHelper.ClickElement("Run");
            analysisHelper.WaitForWorkArround(3000);

            //Select “Heatmap UK Test” in the “Dataset to Run…..” drop down
            analysisHelper.SelectByText("FormAgainst", "Heatmap UK Test");

            //Enter name
            analysisHelper.type("AnaticName", "AnalyticsResults1");

            //Select the Run against dataset button
            analysisHelper.ClickElement("RunAgainst");

            //Wait for completion message (could be 3-4 minutes)
            analysisHelper.WaitForAlert();

            //Select ok
            analysisHelper.acceptAlert();

            analysisHelper.WaitForWorkArround(3000);

            //Close marker window
            analysisHelper.ClickElement("CloseMarker");

            //Select Dataset menu
            analysisHelper.ClickElement("DatasetMenu");

            //Check new Dataset present called AnalyticsResults1
            analysisHelper.WaitForTextVisible("AnalyticsResults1", 5000);
            
            //Enter “Europe” in the search box
            analysisHelper.type("SearchBox", "Europe");

            //Click on Search button
            analysisHelper.ClickElement("SearchButton");

            analysisHelper.WaitForWorkArround(5000);

            //Remove large pin
            analysisHelper.ClickElement("ClearButton");

            //Select the “Draw” menu
            analysisHelper.ClickElement("Drawmenu");

            //Select the rectangle option
            analysisHelper.ClickElement("Rect");
            analysisHelper.WaitForWorkArround(3000);

            //Click the map in the top left hand corner outside the UK and drag the slection to the bottom right (as shown below) 
            String exe = analysisHelper.getPath() + "Rect.exe";
            System.Diagnostics.Process.Start(exe);

            analysisHelper.WaitForWorkArround(5000);
            
            //Select AnalyticsResults1 in the dataset dropdown
            analysisHelper.SelectByText("AnaDrop", "AnalyticsResults1");

            //Select CSV in the format dropdown 
            analysisHelper.SelectByText("Format", "CSV");

            //Select export.  This will download the data as CSV
            analysisHelper.ClickElement("Export");

            analysisHelper.WaitForWorkArround(5000);
            exe = analysisHelper.getPath() + "SaveDialog.exe";
            System.Diagnostics.Process.Start(exe);

            analysisHelper.WaitForWorkArround(5000);

            //Save CSV file to test directory
            Assert.IsTrue(pathToDownloads.pathToFile("Export_*.csv"));
             
            //Select the dataset menu
            analysisHelper.ClickElement("DatasetMenu");

            analysisHelper.WaitForWorkArround(5000);

            //Uncheck the data set
            analysisHelper.ClickByIndex("ShowDataSet");

            analysisHelper.WaitForWorkArround(5000);
            //Logout from the application
            logout();
            
        }
    }
}
