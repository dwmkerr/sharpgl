using System;
using System.ComponentModel;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Lighting;

namespace SharpGL.SceneGraph.Core
{
    /// <summary>
    /// The ArcBall camera supports arcball projection, making it ideal for use with a mouse.
    /// </summary>
    [Serializable()]
    public class ArcBall
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerspectiveCamera"/> class.
        /// </summary>
        public ArcBall()
        {
            //  Set the identity matrices.
            transformMatrix.SetIdentity();
            lastRotationMatrix.SetIdentity();
            thisRotationMatrix.SetIdentity();
        }

        /// <summary>
        /// This is the class' main function, to override this function and perform a 
        /// perspective transformation.
        /// </summary>
        public void TransformMatrix(OpenGL gl)
        {
            gl.MultMatrix(transformMatrix.AsColumnMajorArray);
        }

        public void MouseDown(int x, int y)
        {
            //  Map the start vertex.
            MapToSphere((float)x, (float)y, out startVector);
        }

        public void MouseMove(int x, int y)
        {
            //  todo need solid tuple types.
            //  Calculate the quaternion.
            float[] quaternion = CalculateQuaternion(x, y);

            thisRotationMatrix = Matrix3fSetRotationFromQuat4f(quaternion);
            thisRotationMatrix = thisRotationMatrix * lastRotationMatrix;
            Matrix4fSetRotationFromMatrix3f(ref transformMatrix, thisRotationMatrix);          // Set Our Final Transform's Rotation From This One
        }

        public void MouseUp(int x, int y)
        {
            lastRotationMatrix.FromOtherMatrix(thisRotationMatrix, 3, 3);
            thisRotationMatrix.SetIdentity();
            //Matrix4fSetRotationFromMatrix3f(ref transformMatrix, thisRotationMatrix);          // Set Our Final Transform's Rotation From This One
        }

        private Matrix Matrix3fSetRotationFromQuat4f(float[] q1)
        {
            float n, s;
            float xs, ys, zs;
            float wx, wy, wz;
            float xx, xy, xz;
            float yy, yz, zz;
            n = (q1[0] * q1[0]) + (q1[1] * q1[1]) + (q1[2] * q1[2]) + (q1[3] * q1[3]);
            s = (n > 0.0f) ? (2.0f / n) : 0.0f;

            xs = q1[0] * s; ys = q1[1] * s; zs = q1[2] * s;
            wx = q1[3] * xs; wy = q1[3] * ys; wz = q1[3] * zs;
            xx = q1[0] * xs; xy = q1[0] * ys; xz = q1[0] * zs;
            yy = q1[1] * ys; yz = q1[1] * zs; zz = q1[2] * zs;

            Matrix matrix = new Matrix(3, 3);

            matrix[0, 0] = 1.0f - (yy + zz); matrix[1, 0] = xy - wz; matrix[2, 0] = xz + wy;
            matrix[0, 1] = xy + wz; matrix[1, 1] = 1.0f - (xx + zz); matrix[2, 1] = yz - wx;
            matrix[0, 2] = xz - wy; matrix[1, 2] = yz + wx; matrix[2, 2] = 1.0f - (xx + yy);

            return matrix;
        }

        private void Matrix4fSetRotationFromMatrix3f(ref Matrix transform, Matrix matrix)
        {
            float scale = transform.TempSVD();
            transform.FromOtherMatrix(matrix, 3, 3);
            transform.Multiply(scale, 3, 3);
        }

        private float[] CalculateQuaternion(int x, int y)
        {
            //  Map the current vector.
            MapToSphere((float)x, (float)y, out currentVector);

            //  Compute the cross product of the begin and end vectors.
            Vertex cross = startVector.VectorProduct(currentVector);

            //  Is the perpendicular length essentially non-zero?
            if (cross.Magnitude() > 1.0e-5)
            {
                //  The quaternion is the transform.
                return new float[] { cross.X, cross.Y, cross.Z, startVector.ScalarProduct(currentVector) };
            }
            else
            {
                //  Begin and end coincide, return identity.
                return new float[] { 0, 0, 0, 0 };
            }
        }

        private void MapToSphere(float x, float y, out Vertex newVector)
        {
            float scaledX = x * adjustWidth - 1.0f;
            float scaledY = 1.0f - y * adjustHeight;

            //  Get square of length of vector to point from centre.
            float length = scaledX * scaledX + scaledY * scaledY;

            //  Are we outside the sphere?
            if (length > 1.0f)
            {
                //  Get normalising factor.
                float norm = 1.0f / (float)Math.Sqrt(length);

                //  Return normalised vector, a point on the sphere.
                newVector = new Vertex(-scaledX * norm, 0, scaledY * norm);
            }
            else
            {
                //  Return a vector to a point mapped inside the sphere.
                newVector = new Vertex(-scaledX, (float)Math.Sqrt(1.0f - length), scaledY);
            }
        }

        public void SetBounds(float width, float height)
        {
            //  Set the adjust width and height.
            adjustWidth = 1.0f / ((width - 1.0f) * 0.5f);
            adjustHeight = 1.0f / ((height - 1.0f) * 0.5f);
        }

        private float adjustWidth = 1.0f;
        private float adjustHeight = 1.0f;
        private Vertex startVector = new Vertex(0, 0, 0);
        private Vertex currentVector = new Vertex(0, 0, 0);

        Matrix transformMatrix = new Matrix(4, 4);

        Matrix lastRotationMatrix = new Matrix(3, 3);

        Matrix thisRotationMatrix = new Matrix(3, 3);
    }
}
