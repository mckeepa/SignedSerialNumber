using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;

namespace XmlSignMessageTest
{
    [TestClass]
    public class SignMessageTest
    {
        [TestMethod]
        public void CanLoadCertificateIntoStore()
        {
            //return new X509Certificate2(@".\TestData\public_privatekey.pfx", password);

        }

        public void CanCreateCertificate()
        {
            //return new X509Certificate2(@".\TestData\public_privatekey.pfx", password);

        }


        [TestMethod]
        public void CanSignXmlString()
        {

            string xmlDoc = TestMessage;
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlDoc);
            X509Certificate2 myCert = new X509Certificate2(@".\TestData\public_privatekey.pfx", "");


            //var response = XmlSignMessage.SignXML.SignMessage(xmlDoc);
            var response = XmlSignMessage.SignXML.SignMessage(xmlDoc, myCert);

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