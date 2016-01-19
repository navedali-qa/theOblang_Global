using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace theOblang_Global.PageHelper.Comm
{
    public class XMLParse
    {
        public XmlDocument oXMLSettings;

        public void Dispose()
        {
            oXMLSettings = null;
        }

        //* Method for loading the XML 
        public bool LoadXML(string sXMLPath)
        {
            bool bRetVal = false;

            oXMLSettings = null;
            oXMLSettings = new XmlDocument();
            oXMLSettings.Load(sXMLPath);

            if (oXMLSettings != null)
            {
                bRetVal = true;
            }

            return bRetVal;
        }

        //* Method for getting a single node value from the XML doc using the passed in xPath
        public string getNodeValue(string xPath)
        {
            string sRetVal = "";

            XmlNode oNode = null;
            oNode = oXMLSettings.SelectSingleNode(xPath);

            if (oNode != null)
            {
                sRetVal = oNode.InnerText;
            }

            return sRetVal;
        }

        //* Method for getting a multiple node values from the XML doc using the passed in xPath, and the Element to get
        public string[] getData(string xPath, string sElement)
        {
            string[] sRetVal;
            var listOfData = new List<string>();

            XmlNodeList oNodes = oXMLSettings.SelectNodes(xPath);

            foreach (XmlNode oNode in oNodes)
            {
                XmlNode oValue = oNode.SelectSingleNode(sElement);
                listOfData.Add(oValue.InnerText);
            }

            sRetVal = listOfData.ToArray();
            return sRetVal;
        }

        //* Method for getting the text to search for for the Search activity from the XML doc using the passed in xPath
        //* returns an array of the search text 
        public string[] getSearchData(string xPath)
        {
            string[] sRetVal;
            var listOfData = new List<string>();

            XmlNodeList oNodes = oXMLSettings.SelectNodes(xPath);

            foreach (XmlNode oNode in oNodes)
            {
                listOfData.Add(oNode.InnerText);
            }

            sRetVal = listOfData.ToArray();
            return sRetVal;
        }
    }
}
