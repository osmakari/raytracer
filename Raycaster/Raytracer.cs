using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Raycaster
{
	public static class Raytracer
	{
		// List of spheres in the scene
		public static List<Sphere> spheres = new List<Sphere>();
		// List of light sources in the scene
		public static List<LightSource> lights = new List<LightSource>();

		// Ambient color (Background color)
		public static Color ambientColor = new Color(1f, 1f, 1f);

		// Ambient intensity
		public static float ambientIntensity = 0.3f;

		public static byte[] pixels;

		private static int sWidth;
		private static int sHeight;

		// Main raytrace function
		public static void RayTrace ()
		{
			// Generate some spheres to the scene
			Random r = new Random();
			for(int x = 0; x < 20; x++)
			{
				Sphere s = new Sphere(new Vector3((float)r.NextDouble() * 20 - 10f, (float)r.NextDouble() * 15 - 7.5f, (float)r.NextDouble() * 10 + 7f), (float)r.NextDouble() * 3);
				s.color = new Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
			}
			/*
			Sphere s1 = new Sphere(new Vector3(-2, 0, 10), 3);
			Sphere s2 = new Sphere(new Vector3(2, 0, 7), 1);

			s1.color = new Color(0f, 1f, 1f);
			s2.color = new Color(1f, 0f, 1f);
			*/

			// Start rendering
			Render();
		}

		static void SetPixel (int x, int y, Color c)
		{
			int p = ((y * (int)sWidth) + x) * 3;
			pixels[p] = (byte)(c.r * 255);
			pixels[p + 1] = (byte)(c.g * 255);
			pixels[p + 2] = (byte)(c.b * 255);
		}

		static Color GetPixel (int x, int y)
		{
			int p = ((y * (int)sWidth) + x) * 3;
			return new Color((float)(pixels[p]/255f), (float)(pixels[p + 1] / 255f), (float)(pixels[p + 2] / 255f));
		}

		// Calculate surface reflections
		static void Reflection (Sphere hsphere, Vector3 hp, Vector3 dir, int x, int y, int bounces)
		{
			// Reflection vector
			Vector3 rdir = (dir - 2 * Vector3.Dot(dir, (hp - hsphere.position).normalized) * (hp - hsphere.position).normalized).normalized;
			//Random rnd = new Random();
			//Vector3 rv = new Vector3((float)(rnd.NextDouble() * 0.01) - 0.005f, (float)(rnd.NextDouble() * 0.01) - 0.005f, (float)(rnd.NextDouble() * 0.01) - 0.005f);

			//rdir += rv;

			//rdir = rdir.normalized;

			// Hit point
			Vector3 hp2 = null;
			// Hit sphere
			Sphere hsphere2 = null;

			// Shortest ray distance
			float shortest = -1;

			// Go through all spheres in the scene
			for (int rs = 0; rs < spheres.Count; rs++)
			{
				// Calculating ray-sphere intersection

				// Can't hit same sphere twice in a row
				if (spheres[rs] == hsphere)
					continue;

				

				// Vector between last hit point and sphere center
				Vector3 m = hp - spheres[rs].position;
				// Radius of the sphere
				float r = spheres[rs].radius;

				if (hsphere2 != null)
				{
					if (Vector3.Distance(hp, hsphere2.position) - hsphere2.radius < Vector3.Distance(hp, spheres[rs].position) - r)
					{
						continue;
					}
				}

				// Dot product of m and reflection direction
				float b = Vector3.Dot(m, rdir.normalized);
				

				float c = Vector3.Dot(m, m) - r * r;

				// Check if ray is pointing away from the sphere
				if (c > 0.0f && b > 0.0f)
					continue;


				float discr = b * b - c;

				// If discriminant is less than 0, ray missed the sphere
				if (discr < 0.0f)
					continue;

				// Calculate intersection
				float t = -b - (float)Math.Sqrt(discr);
				if (t < 0.0f)
					t = 0.0f;


				// Calculate hit point vector
				Vector3 q = hp + rdir.normalized * t;

				// Set this bounce if it's the shortest
				if (shortest < 0)
				{
					shortest = q.magnitude;
					hsphere2 = spheres[rs];
					hp2 = q;

				}
				else
				{
					if (q.magnitude < shortest)
					{
						shortest = q.magnitude;
						hsphere2 = spheres[rs];
						hp2 = q;
					}
				}

			}
			// Intersection found, calculate pixel value
			if (hsphere2 != null)
			{

				float dv = Vector3.Dot(rdir, hp2 - hsphere2.position);
				float angle = dv / (rdir.magnitude * (hp2 - hsphere2.position).magnitude);

				float c = Math.Abs(angle / (float)Math.PI);
				/*
				Display.DrawPixel(x, y, new Color(
					ambientColor.r * c * (hsphere2.color.r / (bounces + 2)) + (ambientColor.r * ambientIntensity),
					ambientColor.g * c * (hsphere2.color.g / (bounces + 2)) + (ambientColor.g * ambientIntensity),
					ambientColor.b * c * (hsphere2.color.b / (bounces + 2)) + (ambientColor.b * ambientIntensity))
				);
				*/
				SetPixel(x, y, new Color(
					ambientColor.r * c * (hsphere2.color.r / (bounces + 2)) + (ambientColor.r * ambientIntensity),
					ambientColor.g * c * (hsphere2.color.g / (bounces + 2)) + (ambientColor.g * ambientIntensity),
					ambientColor.b * c * (hsphere2.color.b / (bounces + 2)) + (ambientColor.b * ambientIntensity))
				);

				if(bounces < 1)
				{
					// Reflection
					Reflection(hsphere2, hp2, rdir, x, y, bounces + 1);
				}
				
			}
			

		}

		public static void Render ()
		{
			sWidth = (int)Display.image.Width;
			sHeight = (int)Display.image.Height;
			pixels = new byte[sWidth * sHeight * 3];
			Debug.WriteLine("PIXELS: " + pixels.Length);

			// Draw background color
			for(int y = 0; y < (int)Display.image.Height; y++)
			{
				for (int x = 0; x < (int)Display.image.Width; x++)
				{
					//Display.DrawPixel(x, y, ambientColor);
					SetPixel(x, y, ambientColor);
				}
			}

			// Get the center of the screen
			Vector2 center = new Vector2((int)Display.image.Width / 2, (int)Display.image.Height / 2);
			for(int y = 0; y < Display.image.Height; y++)
			{
				for(int x = 0; x < Display.image.Width; x++)
				{
					// Note: Camera is always at (0, 0, 0) and is pointing (0, 0, 1)
					// Get ray X and Y directions for the corresponding pixel
					float xv = (x - center.x) / (float)Display.image.Height;
					float yv = -(y - center.y) / (float)Display.image.Height;

					// Create a normalized vector from xv and yv
					Vector3 ray = new Vector3(xv * 1.2f, yv * 1.2f, 1).normalized;

					Vector3 hp = null;
					Sphere hsphere = null;

					float shortest = -1;

					// Go through the spheres
					for(int rs = 0; rs < spheres.Count; rs++)
					{
						Vector3 m = -spheres[rs].position;
						float r = spheres[rs].radius;

						// Optimization: Check if the ray distance could be shorter than the current shortest distance
						if (hsphere != null)
						{
							if (hsphere.position.magnitude - hsphere.radius < spheres[rs].position.magnitude - r)
							{
								continue;
							}
						}

						// Calculate dot product between ray and negated sphere position
						float b = Vector3.Dot(m, ray);
						float c = Vector3.Dot(m, m) - r * r;

						// Check if ray is pointing away from the sphere
						if (c > 0.0f && b > 0.0f)
							continue;

						float discr = b * b - c;

						// If discriminant is less than 0, ray missed the sphere
						if (discr < 0.0f)
							continue;

						// Calculate intersection point
						float t = -b - (float)Math.Sqrt(discr);
						if (t < 0.0f)
							t = 0.0f;

						// Calculate hit point vector
						Vector3 q = ray * t;
						// Set this bounce if it's the shortest
						if (shortest < 0)
						{
							shortest = q.magnitude;
							hsphere = spheres[rs];
							hp = q;
							
						}
						else
						{
							if(q.magnitude < shortest)
							{
								shortest = q.magnitude;
								hsphere = spheres[rs];
								hp = q;
							}
						}

					}
					// Intersection found, set pixel and check reflection
					if(hsphere != null)
					{
						float dv = Vector3.Dot(ray, hp - hsphere.position);
						float angle = dv / (ray.magnitude * (hp - hsphere.position).magnitude);
						
						float c = Math.Abs(angle / (float)Math.PI);
						/*
						Display.DrawPixel(x, y, new Color(
							ambientColor.r * c * hsphere.color.r + (ambientColor.r * ambientIntensity), 
							ambientColor.g * c * hsphere.color.g + (ambientColor.g * ambientIntensity), 
							ambientColor.b * c * hsphere.color.b + (ambientColor.b * ambientIntensity))
						);
						*/
						SetPixel(x, y, new Color(
							ambientColor.r * c * hsphere.color.r + (ambientColor.r * ambientIntensity),
							ambientColor.g * c * hsphere.color.g + (ambientColor.g * ambientIntensity),
							ambientColor.b * c * hsphere.color.b + (ambientColor.b * ambientIntensity))
						);

						// Reflection
						Reflection(hsphere, hp, ray, x, y, 0);
					}

				}
			}

			Debug.WriteLine("Tasks ended, drawing pixels...");
			
			for (int y = 0; y < (int)Display.image.Height; y++)
			{
				for (int x = 0; x < (int)Display.image.Width; x++)
				{
					Display.DrawPixel(x, y, GetPixel(x, y));
				}
			}
			Debug.WriteLine("Pixels drawn");
			pixels = null;
			GC.Collect();
		}
	}
	/// <summary>
	/// Sphere class
	/// </summary>
	public class Sphere
	{
		public Vector3 position = new Vector3();
		public float radius = 1f;

		public Color color = new Color(255, 255, 255);

		public Sphere ()
		{
			Raytracer.spheres.Add(this);
		}

		public Sphere (Vector3 pos, float rad)
		{
			Raytracer.spheres.Add(this);
			this.position = pos;
			this.radius = rad;
		}
	}
	
	/// <summary>
	/// Light source class
	/// </summary>
	public class LightSource
	{
		public Vector3 direction;
		public float intensity = 5f;

		public LightSource ()
		{
			direction = new Vector3();
		}

		public LightSource (Vector3 dir)
		{
			direction = dir;
		}
	}
	
}
