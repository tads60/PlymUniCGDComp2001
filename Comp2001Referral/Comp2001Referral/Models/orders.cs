using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comp2001Referral.Models
{
    public class orders
    {
        public int orderNumber
        {
            get; set;
        }
        public int customerID
        {
            get; set;
        }

        public int totalAmount
        {
            get; set;
        }

        public DateTime date
        {
            get; set;
        }

        public DateTime dispatched
        {
            get; set;
        }
    }
}
