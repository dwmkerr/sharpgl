using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class FloatHelper
    {
        public static string ToShortString(this float value)
        {
            string result = null;
            if (value <= -10 || 10 <= value)
            {
                result = string.Format("{0:0.0}", value);
            }
            else
            {
                result = string.Format("{0:0.00}", value);
            }

            return result;
        }
    }
}
