using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helper
{
    public static  class CheckNull
    {
        public static bool HasValue(this string value) {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
