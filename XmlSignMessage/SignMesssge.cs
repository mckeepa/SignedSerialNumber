//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Cryptography;
//using System.Security.Cryptography.Xml;
//using System.Xml;
//using System.IO;
//using System.Deployment.Internal.CodeSigning;
//using System.Security.Cryptography.X509Certificates;

//namespace XmlSignMessage

//{

//    public class SignXML256
//    {

//        public static string SignMessage(string xmlMessage)
//        {


//            CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");

//            X509Certificate2 cert = new X509Certificate2(@"location of pks file", "password", X509KeyStorageFlags.Exportable);

//            // Export private key from cert.PrivateKey and import into a PROV_RSA_AES provider:
//            var exportedKeyMaterial = cert.PrivateKey.ToXmlString( /* includePrivateParameters = */ true);
//            var key = new RSACryptoServiceProvider(new CspParameters(24 /* PROV_RSA_AES */));
//            key.PersistKeyInCsp = false;
//            key.FromXmlString(exportedKeyMaterial);

//            XmlDocument doc = new XmlDocument();
//            doc.PreserveWhitespace = true;
//            doc.Load(@"input.xml");

//            SignedXml signedXml = new SignedXml(doc);
//            signedXml.SigningKey = key;
//            signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

//            // 
//            // Add a signing reference, the uri is empty and so the whole document 
//            // is signed. 
//            Reference reference = new Reference();
//            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
//            reference.AddTransform(new XmlDsigExcC14NTransform());
//            reference.Uri = "";
//            signedXml.AddReference(reference);

//            // 
//            // Add the certificate as key info, because of this the certificate 
//            // with the public key will be added in the signature part. 
//            KeyInfo keyInfo = new KeyInfo();
//            keyInfo.AddClause(new KeyInfoX509Data(cert));
//            signedXml.KeyInfo = keyInfo;
//            // Generate the signature. 
//            signedXml.ComputeSignature();

//            return signedXml.ToString();
//        }
//    }
//}
