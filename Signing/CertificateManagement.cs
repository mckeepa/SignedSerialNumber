using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Signing
{
    public class CertificateManagement
    {
        public CertificateManagement()
        {
        }

        public static X509Certificate2 LoadCert(StoreLocation storeLocation, string storeName, string serialNumber)
        {

            var store = new X509Store(storeName, StoreLocation.CurrentUser); //StoreLocation.LocalMachine fails too
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


        // Sign an XML file. 
        // This document cannot be verified unless the verifying 
        // code has the key with which it was signed.
        public static System.Xml.XmlDocument SignXml(XmlDocument xmlDoc, System.Security.Cryptography.RSA Key)
        {

            // Check arguments.
            if (xmlDoc == null)  throw new ArgumentException("xmlDoc");
            if (Key == null)  throw new ArgumentException("Key");

            // Create a SignedXml object.

            System.Security.Cryptography.Xml.SignedXml = new SignedXml(xmlDoc);

            //Load Certificate - .pfx file holds private and public keys
            // commandline: openssl pkcs12 -export -out public_privatekey.pfx - inkey private.key -in publickey.cer
            X509Certificate2 privateCert = new X509Certificate2(@".\TestData\public_privatekey.pfx", "");


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
