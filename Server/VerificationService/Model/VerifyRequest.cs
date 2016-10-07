using System.Collections.Generic;

namespace VerificationService.Model
{
    public class VerifyRequest
    {
        public Installer Installer { get; set; }
        public List<Product> Products { get; set; }
        public Address InstallationAddress { get; set; }
        public Organisation Retailer { get; set; }
}
    

    public class Organisation

    {
        public string Name { get; set; }
        public string ABN { get; set; }

    }

    public class Address
    {
        public string AddressLine1 { get; set; } 
        public string AddressLine2 { get; set; }
        public string Suburb       { get; set; }
        public string Postcode     { get; set; }
        public string State        { get; set; }
        public Location Location { get; set; }


    }

    public class Location
    {
        public string Latitude   { get; set; }
        public string Longitude  { get; set; }
        public string Altitude   { get; set; }
        public int Accuracy { get; set; }
        public bool ManuallyEntered { get; set; }

    }


    public class VerifiedProduct: Product {
        public string ModelNumber { get; set; }
        public string FlashTest { get; set; }

    }

    public class Product
        {
            public string SerialNumber { get; set; }
            public string Manufacturer { get; set; }
            public Organisation Retailer { get; set; }
            public string Importer{ get; set; }
    
        }

    public class Installer
    {

        public int ID{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName  { get; set; }
        public string CompanyABN { get; set; }
 
    }
}