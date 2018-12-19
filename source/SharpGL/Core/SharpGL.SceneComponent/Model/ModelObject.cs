using SharpGL.SceneComponent.SimpleUI.ColorIndicator;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent.Model
{

    

    public class ModelObject:SceneElement
    {

    }


    public abstract class ColorIndicatorMassElement
    {
        public abstract void UpdateColor(UIColorIndicator indicator);
    }


    public class ParticlesMassElement: ColorIndicatorMassElement
    {

        private float[] positions;

        private float[] quantities;

        private GLColor[] colors;


        public ParticlesMassElement(float[] positions, float[] quantities)
        {
            if (positions.Length != quantities.Length)
                throw new ArgumentException("particle must have ");

            this.positions = positions;
            this.quantities = quantities;
        }


        public float[] Positions
        {
            get
            {
                return this.positions;
            }
        }

        public float[] Quantities
        {
            get
            {
                return this.quantities;
            }
            set
            {
                this.quantities = value;
            }
        }

        public override void UpdateColor(UIColorIndicator indicator)
        {
            GLColor[] colors = this.colors;
            if (colors == null)
            {
                colors = new GLColor[this.quantities.Length];
            }
            for (int i = 0; i < this.quantities.Length; i++)
            {
               colors[i]= indicator.MapToColor(this.quantities[i]);
            }
            this.colors = colors;
        }


    }
}
