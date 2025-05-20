using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Data.Helper
{
   public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpirationInDays { get; set; }

        //public bool ValidateIssuer { get; set; }
        //public bool ValidateAudience { get; set; }
        //public bool ValidateLifetime { get; set; }
        //public bool ValidateIssuerSigningKey { get; set; }
        //public int AccessTokenExpireDate { get; set; }
        //public int RefreshTokenExpireDate { get; set; }
    }
}
