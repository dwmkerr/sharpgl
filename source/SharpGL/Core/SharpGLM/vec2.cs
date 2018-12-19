using System;

namespace GlmNet 
{
    /// <summary>
    /// Represents a two dimensional vector.
    /// </summary>
	public struct vec2
	{
		public float x;
		public float y;

		public float this[int index]
		{
			get 
			{
				if(index == 0) return x;
				else if(index == 1) return y;
                else throw new Exception("Out of range.");
			}
			set 
			{
				if(index == 0) x = value;
                else if (index == 1) y = value;
                else throw new Exception("Out of range.");
			}
		}

		public vec2(float s)
		{
			x = y = s;
		}

		public vec2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

        public vec2(vec2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        public vec2(vec3 v)
        {
            this.x = v.x;
            this.y = v.y;
        }
		
		public static vec2 operator + (vec2 lhs, vec2 rhs)
		{
			return new vec2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

        public static vec2 operator +(vec2 lhs, float rhs)
        {
            return new vec2(lhs.x + rhs, lhs.y + rhs);
        }

        public static vec2 operator -(vec2 lhs, vec2 rhs)
        {
            return new vec2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static vec2 operator - (vec2 lhs, float rhs)
        {
            return new vec2(lhs.x - rhs, lhs.y - rhs);
        }

        public static vec2 operator *(vec2 self, float s)
		{
			return new vec2(self.x * s, self.y * s);
		}

        public static vec2 operator *(float lhs, vec2 rhs)
        {
            return new vec2(rhs.x * lhs, rhs.y * lhs);
        }

        public static vec2 operator * (vec2 lhs, vec2 rhs)
        {
            return new vec2(rhs.x * lhs.x, rhs.y * lhs.y);
        }

        public static vec2 operator /(vec2 lhs, float rhs)
        {
            return new vec2(lhs.x / rhs, lhs.y / rhs);
        }

        public float[] to_array()
        {
            return new[] { x, y };
        }
	}
}