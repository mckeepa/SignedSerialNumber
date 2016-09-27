using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace XmlSignMessageTest
{
    [TestClass]
    public class SignMessageTest
    {
        [TestMethod]
        public void CanSignXmlString()
        {

            string xmlDoc = TestMessage;
            //var response = XmlSignMessage.SignXML.SignMessage(xmlDoc);
            var response = XmlSignMessage.SignXML.SignMessage(xmlDoc);

        }

        public string TestMessage
        {
            get
            {
                return File.ReadAllText(@".\TestData\request1.xml"); 
               ;
            }
        }
    }
}