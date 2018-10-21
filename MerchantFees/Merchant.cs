using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantFees
{
    public class Merchant
    {
        public string Name { get; set; }
        public MerchantType Type {
            get {
                if (Name == "TELIA" || Name == "CIRCLE_K")
                    return MerchantType.BIG;
                else return MerchantType.REGULAR;
            }
        }

        public Merchant()
        {

        }
    }

    public enum MerchantType
    {
        REGULAR,
        BIG
    }
}