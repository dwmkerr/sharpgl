using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// contains minimum value and maximum value.
    /// </summary>
    public class QuantityRange
    {
        public float minValue;
        public float maxValue;

        public QuantityRange(float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public override string ToString()
        {
            return string.Format("{0} ~ {1}", minValue, maxValue);
            //return base.ToString();
        }
    }
}
