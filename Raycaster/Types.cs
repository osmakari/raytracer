using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycaster
{
	/*
		Types
	*/

	/// <summary>
	/// 2 Dimensional Vector
	/// </summary>
	public class Vector2
	{
		public float x = 0;
		public float y = 0;

		public Vector2()
		{
			x = 0;
			y = 0;
		}

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public static Vector2 zero
		{
			get
			{
				return new Vector2();
			}
		}

		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}

		public static Vector2 operator /(Vector2 a, float b)
		{
			return new Vector2(a.x / b, a.y / b);
		}

		public static Vector2 operator *(Vector2 a, float b)
		{
			return new Vector2(a.x * b, a.y * b);
		}

		public static Vector2 operator *(Vector2 a, int b)
		{
			return new Vector2(a.x * b, a.y * b);
		}

		public static Vector2 operator *(int a, Vector2 b)
		{
			return new Vector2(a * b.x, a * b.y);
		}

		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public Vector2 normalized
		{
			get
			{
				return new Vector2(x / magnitude, y / magnitude);
			}
		}

		public float magnitude
		{
			get
			{
				return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
			}
		}


	}

	/// <summary>
	/// 3 Dimensional Vector
	/// </summary>
	public class Vector3
	{
		public float x = 0;
		public float y = 0;
		public float z = 0;

		public Vector3()
		{
			x = 0;
			y = 0;
			z = 0;
		}

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3 zero
		{
			get
			{
				return new Vector3();
			}
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		public static Vector3 operator *(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static Vector3 operator /(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static Vector3 operator /(Vector3 a, float b)
		{
			return new Vector3(a.x / b, a.y / b, a.z / b);
		}

		public static Vector3 operator *(Vector3 a, float b)
		{
			return new Vector3(a.x * b, a.y * b, a.z * b);
		}

		public static Vector3 operator *(float b, Vector3 a)
		{
			return new Vector3(a.x * b, a.y * b, a.z * b);
		}

		public static Vector3 operator *(Vector3 a, int b)
		{
			return new Vector3(a.x * b, a.y * b, a.z * b);
		}

		public static Vector3 operator *(int a, Vector3 b)
		{
			return new Vector3(a * b.x, a * b.y, a * b.z);
		}
		/// <summary>
		/// Calculates the dot product of 2 vectors
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public static float Distance(Vector3 a, Vector3 b)
		{
			return (float)Math.Sqrt((a * a).magnitude + (b * b).magnitude);
		}

		public Vector3 normalized
		{
			get
			{
				return new Vector3(x / magnitude, y / magnitude, z / magnitude);
			}
		}

		public float magnitude
		{
			get
			{
				return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
			}
		}

		public override string ToString()
		{
			return "Vector3(" + this.x + ", " + this.y + ", " + this.z + ")";
		}

	}

	/// <summary>
	/// Color class (float, 0f-1f)
	/// </summary>
	public class Color
	{

		float _r = 0f;
		float _g = 0f;
		float _b = 0f;

		public float r
		{
			get
			{
				return Math.Min(Math.Max(_r, 0f), 1f);
			}
			set
			{
				_r = value;
			}
		}
		public float g
		{
			get
			{
				return Math.Min(Math.Max(_g, 0f), 1f);
			}
			set
			{
				_g = value;
			}
		}
		public float b
		{
			get
			{
				return Math.Min(Math.Max(_b, 0f), 1f);
			}
			set
			{
				_b = value;
			}
		}

		public Color()
		{

		}

		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}
	}
}
