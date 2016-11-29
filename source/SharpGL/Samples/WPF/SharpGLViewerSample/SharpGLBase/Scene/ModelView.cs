using GlmNet;
using SharpGLBase.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SharpGLBase.Scene
{
    public enum CameraRotationHandling { SRT, TRS}
    /// <summary>
    /// This class contains all functionality related to the ModelView matrix.
    /// </summary>
    public class ModelView : TransformableBase,INotifyPropertyChanged
    {
        #region events

        #region propertychanged event
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion propertychanged event

        #endregion events

        #region fields
        CameraRotationHandling _cameraHandling = CameraRotationHandling.SRT;
        #endregion fields

        #region properties
        /// <summary>
        /// The ModelView Matrix.
        /// </summary>
        public mat4 ModelviewMatrix
        {
            get { return ResultMatrix; }
        }

        /// <summary>
        /// Get or set the sequence of the modelview rotation transformation.
        /// </summary>
        public CameraRotationHandling CameraHandling
        {
            get { return _cameraHandling; }
            set { _cameraHandling = value; }
        }
        #endregion properties

        /// <summary>
        /// Converts ModelviewMatrix to a float[].
        /// </summary>
        /// <returns>A float[] containing all the values from the initial matrix.</returns>
        public float[] ToArray()
        {
            return ModelviewMatrix.to_array();
        }
       
        /// <summary>
        /// Converst the this.ToArray() from a float[] to a double[]
        /// </summary>
        /// <returns></returns>
        public double[] ToDoubleArray()
        {
            float[] arr = ToArray();
            var newArr = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                newArr[i] = (double)arr[i];
            }

            return newArr;
        }

        /// <summary>
        /// Modelview needs different sequences of transformations.
        /// </summary>
        public override void RecalculateResultMatrix()
        {
            if (CameraHandling == Scene.CameraRotationHandling.TRS)
                ResultMatrix = TranslationMatrix * RotationMatrix * ScalingMatrix; // rotate around center
            else if (CameraHandling == Scene.CameraRotationHandling.SRT)
                ResultMatrix = ScalingMatrix * RotationMatrix * TranslationMatrix; // rotate around camera center

            //ResultMatrix = ScalingMatrix * RotationMatrix * TranslationMatrix; // rotate around camera center
            //ResultMatrix = TranslationMatrix * ScalingMatrix * RotationMatrix; // rotate around camera center (swap axis)
            //ResultMatrix = TranslationMatrix * RotationMatrix * ScalingMatrix; // rotate around own center
            //ResultMatrix = RotationMatrix * ScalingMatrix * TranslationMatrix; // rotate around own center (swap axis)
            //ResultMatrix = RotationMatrix * TranslationMatrix * ScalingMatrix; // stretches shape (swap axis)
            //ResultMatrix = ScalingMatrix * TranslationMatrix * RotationMatrix; // stretches shape (swap axis)
            //ResultMatrix = RotationMatrix * TranslationMatrix.Transpose() * ScalingMatrix; // stretches shape (swap axis)
            //ResultMatrix = ScalingMatrix * TranslationMatrix.Transpose() * RotationMatrix; // stretches shape (swap axis)

            OnPropertyChanged("ModelviewMatrix");
        }

        /// <summary>
        /// Allow direct array access to the ModelviewMatrix.
        /// </summary>
        /// <param name="col">Collumn index.</param>
        /// <returns>The vec4 at this position.</returns>
        public vec4 this[int col]{
            get
            {
                return ModelviewMatrix[col];
            }
        }
    }
}
