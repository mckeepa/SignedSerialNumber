using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace XmlSignMessage

{

    public class SignXML
    {

        //public static string xxx_SignMessage(string xmlMessage)
        //{
        //    try
        //    {
        //        // Create a new CspParameters object to specify
        //        // a key container.
        //        CspParameters cspParams = new CspParameters();
        //        cspParams.KeyContainerName = "XML_DSIG_RSA_KEY";

        //        // Create a new RSA signing key and save it in the container. 
        //        RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

        //        // Create a new XML document.
        //        XmlDocument xmlDoc = new XmlDocument();

        //        // Load an XML file into the XmlDocument object.
        //        xmlDoc.PreserveWhitespace = true;
        //        xmlDoc.LoadXml(xmlMessage);

        //        // Sign the XML document. 
        //        var signatureElement = GetSignatureXml(xmlDoc, rsaKey);

        //        Console.WriteLine("XML file signed.");

        //        // Save the document.

        //        xmlDoc.Save(Console.Out);
        //        return xmlDoc.InnerXml;


        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw e;
        //    }
        //}


        // Sign an XML file. 
        // This document cannot be verified unless the verifying 
        // code has the key with which it was signed.
        public static XmlElement GetSignatureXml(XmlDocument xmlDoc, RSA Key)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (Key == null)
                throw new ArgumentException("Key");

            // Create a SignedXml object.
            var signedXml = new SignedXml(xmlDoc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = Key;



            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);



            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            return xmlDigitalSignature;

            // Append the element to the XML document.
            //xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

        }


        public static X509Certificate2 LoadCert(StoreLocation storeLocation, string serialNumber)
        {

            var store = new X509Store(StoreLocation.CurrentUser); //StoreLocation.LocalMachine fails too
            store.Open(OpenFlags.ReadOnly);
            var certificates = store.Certificates;
            foreach (var certificate in certificates)
            {
                //if (certificate..Subject.Contains("xxx"))
                if (certificate.SerialNumber.Contains(serialNumber))
                    {
                        return certificate;
                }
            }
            throw new Exception("Certificate not found.");
        }


        public static XmlDocument SignMessage(string xmlMessage, X509Certificate2 cert)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xmlMessage);


           //     X509Certificate2 myCert = new X509Certificate2(@".\TestData\public_privatekey.pfx", "");

                if (cert != null)
                {
                    RSA rsaKey = ((RSA)cert.PrivateKey);

                    // Sign the XML document. 
                    SignXml(xmlDoc, rsaKey);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return xmlDoc;
        }


        // Sign an XML file. 
        // This document cannot be verified unless the verifying 
        // code has the key with which it was signed.
        public static XmlDocument SignXml(XmlDocument xmlDoc, RSA Key)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (Key == null)
                throw new ArgumentException("Key");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            //Load Certificate - .pfx file holds private and public keys
            // commandline: openssl pkcs12 -export -out public_privatekey.pfx - inkey private.key -in publickey.cer
            X509Certificate2 privateCert = new X509Certificate2(@".\Data\public_privatekey.pfx", "password");


             // Add the key to the SignedXml document.
             signedXml.SigningKey = Key;
            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(privateCert);
            X509IssuerSerial xserial;
            

            xserial.IssuerName = privateCert.IssuerName.Name;
            xserial.SerialNumber = privateCert.SerialNumber;
            
            keyInfoData.AddIssuerSerial(xserial.IssuerName, xserial.SerialNumber);
            keyInfo.AddClause(keyInfoData);

            signedXml.KeyInfo = keyInfo;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            return xmlDoc;
        }
    }
}
