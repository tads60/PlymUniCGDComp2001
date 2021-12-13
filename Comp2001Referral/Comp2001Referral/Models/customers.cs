using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comp2001Referral.Models
{
    public partial class customers
    {
        public int IDNumber
        {
            get; set;
        }
        public string title
        {
            get; set;
        }
        public string firstName
        {
            get; set;
        }

        public string lastName
        {
            get; set;
        }

         public int phoneNumber
        {
            get; set;
        }

        public int deliveryAddressID 
        { 
            get; set; 
        }
        public int billingAddressID
        {
            get; set;
        }
    }
}
