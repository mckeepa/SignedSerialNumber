using System.Collections.Generic;
using VerificationService.Model;

namespace VerificationService.Controllers
{
    public class VerificationResponse
    {
        public Installer Installer { get; set; }
        public IList<VerifiedProduct> Products { get; set; }

    }
}