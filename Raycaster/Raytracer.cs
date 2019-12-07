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
        public static int reflections = 1;
		// List of spheres in the scene
		public static List<Sphere> spheres = new List<Sphere>();
		// List of light sources in the scene
		public static List<LightSource> lights = new List<LightSource>();

		// Ambient color (Background color)
		public static Color ambientColor = new Color(0.9f, 0.9f, 1f);

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
			/*for(int x = 0; x < 20; x++)
			{
				Sphere s = new Sphere(new Vector3((float)r.NextDouble() * 20 - 10f, (float)r.NextDouble() * 15 - 7.5f, (float)r.NextDouble() * 10 + 7f), (float)r.NextDouble() * 3);
				s.color = new Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
			}
			*/
			Sphere s1 = new Sphere(new Vector3(-2, -5f, 10), 1.2f);
			Sphere s2 = new Sphere(new Vector3(2, -4, 18), 1);
            Sphere s3 = new Sphere(new Vector3(8, 2, 25), 0.4f);
            Sphere s4 = new Sphere(new Vector3(1, 5, 27), 2);
            Sphere s5 = new Sphere(new Vector3(-7, 1, 11), 1);
            Sphere s6 = new Sphere(new Vector3(7, -2, 15), 2);
            Sphere s7 = new Sphere(new Vector3(6, 7, 13), 2.5f);
            Sphere s8 = new Sphere(new Vector3(5, 5, 18), 1.3f);
            Sphere s9 = new Sphere(new Vector3(4, -4, 10), 0.5f);
            Sphere s10 = new Sphere(new Vector3(2, 3, 12), 0.4f);
            s1.color = new Color(0f, 1f, 1f);
			s2.color = new Color(1f, 0f, 1f);
            s3.color = new Color(1f, 0.4f, 0.3f);
            s4.color = new Color(0.7f, 0.4f, 1f);
            s4.color = new Color(0.4f, 0.1f, 1f);
            s6.color = new Color(0f, 0.2f, 1f);
            s7.color = new Color(0.7f, 0.4f, 0.5f);
            s8.color = new Color(0.7f, 0.9f, 1f);
            s9.color = new Color(0.6f, 0.4f, 0.1f);

            // Start rendering
            
            Render();

            //Render();
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
		public static void Reflection (Sphere hsphere, Vector3 hp, Vector3 dir, int x, int y, int bounces)
		{
			Vector3 pos = new Vector3(); 
			float rad = 0.1f;
			if(hsphere != null)
			{
				pos = hsphere.position;
				rad = hsphere.radius;
			}

			// Reflection vector
			Vector3 rdir = (dir - 2 * Vector3.Dot(dir, (hp - pos).normalized) * (hp - pos).normalized).normalized;
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
					ambientColor.r * c * (hsphere2.color.r / (bounces + 1)) + (ambientColor.r * ambientIntensity),
					ambientColor.g * c * (hsphere2.color.g / (bounces + 1)) + (ambientColor.g * ambientIntensity),
					ambientColor.b * c * (hsphere2.color.b / (bounces + 1)) + (ambientColor.b * ambientIntensity))
				);

				if(bounces < 1)
				{
					// Reflection
                    Reflection(hsphere2, hp2, rdir, x, y, bounces + 1);

				}
				
			}
			

		}

        static void createThread()
        {

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
            List<Task> tasks = new List<Task>() ;
            int i = 0;
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
                    var t = new Task(() =>
                    {
                        Reflection(null, new Vector3(0, 0, 0.1f), -new Vector3(xv, yv, 1f).normalized, x, y, reflections);
                    });
                    tasks.Add(t);
                    t.Start();
                    i++;
                }

            }
            while (tasks.Any(t=> !t.IsCompleted)) { }
            Thread.Sleep(100);


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
