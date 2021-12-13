using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comp2001Referral.Models
{
    public class deliveryAddresses
    {
        public int deliveryID
        {
            get; set;
        }
        public string houseName
        {
            get; set;
        }
        public string streetName
        {
            get; set;
        }
        public string town
        {
            get; set;
        }
        public string county
        {
            get; set;
        }
        public string country
        {
            get; set;
        }
        public string postcode
        {
            get; set;
        }
    }
}
