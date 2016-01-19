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
    public class _03LoadDataSet2 : DriverTestCase
    {
        [Test]
        public void loadDataSet2()
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

            loadHelper.DeleteData("DataDrop", "Country Populations");

            //Select import from menu
            loadHelper.ClickElement("Import");

            loadHelper.WaitForWorkArround(4000);

            //Click on next button
            loadHelper.ClickElement("Next");

            String path = loadHelper.getPath() + "Choropleth World Countries - False Populations.csv";

            //Select browse button and browse to file
            loadHelper.UploadFile("Upload", path);

            loadHelper.WaitForWorkArround(5000);

            //Select Import button against Heatmap Test UK Dataset
            loadHelper.ClickElement("ImportPopulationFile");

            loadHelper.WaitForWorkArround(5000);

            //Select Lat/Long radio button
            loadHelper.ClickElement("LonLat");
            loadHelper.WaitForWorkArround(2000);

            //Select Latitude field in Latitude Field drop down and Longitude field in Longitude Field drop down
            loadHelper.SelectByText("Lattitude", "latitude");
            loadHelper.SelectByText("Longitude", "longitude");
            
            //Select Next button
            loadHelper.ClickElement("LongNext");

            //Select checkbox on all fields, including longitude – just off screen.  Then select next
            loadHelper.SelectAllCheck("CheckBox1");

            //Select next
            loadHelper.ClickElement("CheckNext");

            //Select next
            loadHelper.ClickElement("AuthNext");
            
            //Select the population field in the dropdown selector for Numeric Field.  Leave the other as “No datefield”.
            loadHelper.SelectByText("Numeric", "Flse Population Pos");
            
            //Select next 
            loadHelper.ClickElement("NumeNext");

            //Enter the name “Country Populations” in the Dataset Name box
            loadHelper.type("Dataset", "Country Populations");
            
            //press the “begin import” button
            loadHelper.ClickElement("Beginimport");

            //Wait until the import is finished
            loadHelper.WaitForTextHide("Data load in progress...", 60000);

            //Verify file imported successfully
            loadHelper.WaitForTextVisible("Import Data from a flat file currently(.csv or Excel (.xls 97-2003 and .xlsx workbook", 6000);

            //Validate dataset loaded by going to Dataset menu and finding dataset listed
            loadHelper.ClickElement("DataSetMenu");
            loadHelper.WaitForTextVisible("Country Populations", 5000);

            //Logout from the application
            logout();

        }
    }
}
