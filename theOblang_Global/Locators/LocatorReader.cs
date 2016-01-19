using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace theOblang_Global.Locators
{
    public class LocatorReader
    {
        public String fileName;
        public LocatorReader(String filename)
        {
            fileName = filename;
        }
        public string readLocator(string key)
        {
            string value = "";
            XmlDocument xmlDocument = new XmlDocument();

            string env = Environment.CurrentDirectory;
            xmlDocument.Load("../../Locators/" + fileName);
            XmlElement root = xmlDocument.DocumentElement;
            XmlNode node = xmlDocument.DocumentElement.SelectSingleNode(@key);
            XmlNode childNode = node.ChildNodes[0];
            if (childNode is XmlCDataSection)
            {
                XmlCDataSection cdataSection = childNode as XmlCDataSection;
                value = cdataSection.Value;
            }
            return value;
        }
    }
}
