using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Raycaster
{
	public class Raytracer
	{
        public static int maxReflections = 1;
		// List of spheres in the scene
		public static List<Sphere> spheres = new List<Sphere>();
		// List of light sources in the scene
		public static List<LightSource> lights = new List<LightSource>();

		public static LightSource lightSource = new LightSource(new Vector3(-1, 1, -0.5f));

		// Ambient color (Background color)
		public static Color ambientColor = new Color(0f, 0f, 0f);

		// Ambient intensity
		public static float ambientIntensity = 0.4f;

		public static byte[] pixels;

		private static readonly int sWidth = 3840;
		private static readonly int sHeight = 2160;

        private static TimeSpan time = new TimeSpan();
		// Main raytrace function
		public static void RayTrace ()
		{
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // Generate some spheres to the scene
            Random r = new Random();
            /*for(int x = 0; x < 20; x++)
			{
				Sphere s = new Sphere(new Vector3((float)r.NextDouble() * 20 - 10f, (float)r.NextDouble() * 15 - 7.5f, (float)r.NextDouble() * 10 + 7f), (float)r.NextDouble() * 3);
				s.color = new Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
			}
			*/
            //testing purposes
            
            GenerateSpheres gs = new GenerateSpheres();

            for(int rounds = 0; rounds < 10; rounds++)
            {
                spheres.Clear();
                if (rounds == 0)
                {
                    spheres.Add(gs.sphereList[0]);
                }
                else
                {
                    for (int o = 0; o < rounds * 10; o++) spheres.Add(gs.sphereList[o]);
                }
                for (int i = 0; i < 1; i++)
                {
                    maxReflections = 1;
                    Console.Write("Calculating round " + rounds + " with " + maxReflections + " reflections ");
                    stopWatch.Start();
                    Render();
                    TimeSpan ts = stopWatch.Elapsed;
                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00},{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    Console.Write("| RunTime: " + elapsedTime + "\n");
                    stopWatch.Reset();
                }
            }
            for (int rounds = 0; rounds < 10; rounds++)
            {
                spheres.Clear();
                if (rounds == 0)
                {
                    spheres.Add(gs.sphereList[0]);
                }
                else
                {
                    for (int o = 0; o < rounds * 10; o++) spheres.Add(gs.sphereList[o]);
                }
                for (int i = 0; i < 1; i++)
                {
                    maxReflections = 5;
                    Console.Write("Calculating round " + rounds + " with " + maxReflections + " reflections ");
                    stopWatch.Start();
                    Render();
                    TimeSpan ts = stopWatch.Elapsed;
                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00},{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    Console.Write("| RunTime: " + elapsedTime + "\n");
                    stopWatch.Reset();
                }
            }
            for (int rounds = 0; rounds < 10; rounds++)
            {
                spheres.Clear();
                if (rounds == 0)
                {
                    spheres.Add(gs.sphereList[0]);
                }
                else
                {
                    for (int o = 0; o < rounds * 10; o++) spheres.Add(gs.sphereList[o]);
                }
                for (int i = 0; i < 1; i++)
                {
                    maxReflections = 20;
                    Console.Write("Calculating round " + rounds + " with " + maxReflections + " reflections ");
                    stopWatch.Start();
                    Render();
                    TimeSpan ts = stopWatch.Elapsed;
                    // Format and display the TimeSpan value.
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00},{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    Console.Write("| RunTime: " + elapsedTime + "\n");
                    stopWatch.Reset();
                }
            }
            // Start rendering
            //Render();
        }

		static void SetPixel (int x, int y, Color c)
		{
			lock (pixels)
			{
				int p = ((y * (int)sWidth) + x) * 3;
				pixels[p] = (byte)(c.r * 255);
				pixels[p + 1] = (byte)(c.g * 255);
				pixels[p + 2] = (byte)(c.b * 255);
				
			}
		}

		static Color GetPixel (int x, int y)
		{
			int p = ((y * (int)sWidth) + x) * 3;
			return new Color((float)(pixels[p]/255f), (float)(pixels[p + 1] / 255f), (float)(pixels[p + 2] / 255f));
		}

		static bool IsIntersecting (Vector3 origin, Vector3 dir, Sphere s)
		{

			Vector3 m = origin - s.position;
			// Radius of the sphere
			float r = s.radius;

			// Dot product of m and reflection direction
			float b = Vector3.Dot(m, dir);


			float c = Vector3.Dot(m, m) - r * r;

			// Check if ray is pointing away from the sphere
			if (c > 0.0f && b > 0.0f)
				return false;


			float discr = b * b - c;
			if (discr < 0.0f)
				return false;

			return true;

		}

		// Calculate surface reflections
		public static void Reflection (Sphere hsphere, Vector3 hp, Vector3 dir, int x, int y, int bounces)
		{
			Vector3 pos = new Vector3(); 
			float rad = 0.1f;
			// Reflection vector
			Vector3 rdir = dir.normalized;
			if (hsphere != null)
			{
				pos = hsphere.position;
				rad = hsphere.radius;
				rdir = (dir - 2 * Vector3.Dot(dir, (hp - pos).normalized) * (hp - pos).normalized).normalized;
			}
			
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

				float dv = Vector3.Dot(lightSource.direction, hp2 - hsphere2.position);
				float angle = dv / (lightSource.direction.magnitude * (hp2 - hsphere2.position).magnitude);

				float c = Math.Abs(angle / (float)Math.PI);
				/*
				Display.DrawPixel(x, y, new Color(
					ambientColor.r * c * (hsphere2.color.r / (bounces + 2)) + (ambientColor.r * ambientIntensity),
					ambientColor.g * c * (hsphere2.color.g / (bounces + 2)) + (ambientColor.g * ambientIntensity),
					ambientColor.b * c * (hsphere2.color.b / (bounces + 2)) + (ambientColor.b * ambientIntensity))
				);
				*/
				bool intersects = false;
				for(int i = 0; i < spheres.Count; i++)
				{
					if(IsIntersecting(hp2 + (hp2 - hsphere2.position).normalized * 0.001f, lightSource.direction, spheres[i]))
					{
						intersects = true;
						break;
					}
				}
				
				if(!intersects)
				{
					SetPixel(x, y, new Color(
						c * lightSource.intensity * (hsphere2.color.r / (bounces + 1)) + (ambientColor.r * ambientIntensity),
						c * lightSource.intensity * (hsphere2.color.g / (bounces + 1)) + (ambientColor.g * ambientIntensity),
						c * lightSource.intensity * (hsphere2.color.b / (bounces + 1)) + (ambientColor.b * ambientIntensity))
					);
				}
				else
				{
					SetPixel(x, y, new Color(
						0.01f * (hsphere2.color.r / (bounces + 1)) + (ambientColor.r * ambientIntensity),
						0.01f * (hsphere2.color.g / (bounces + 1)) + (ambientColor.g * ambientIntensity),
						0.01f * (hsphere2.color.b / (bounces + 1)) + (ambientColor.b * ambientIntensity)
					));
				}
				
				

				if(bounces < maxReflections)
				{
					// Reflection
					Reflection(hsphere2, hp2, rdir, x, y, bounces + 1);

				}
				
			}
			else
			{
				
				Vector3 planeNormal = new Vector3(0, -5, 0);
				Vector3 planePos = new Vector3(0, -100, 0);
				if(rdir.y < 0)
				{
					float t = (hp.y - planePos.y)/rdir.y;

					SetPixel(x, y, new Color(
						0.7f/(bounces + 1), 0.7f/ (bounces + 1), 0.99f)
					);
					if (bounces < maxReflections)
					{ 
						Reflection(null, hp + rdir * t, rdir - 2 * (Vector3.Dot(rdir, planeNormal)) * planeNormal, x, y, bounces + 1);
					}
				}
				
				
			}
			

		}
		
		public static void Render ()
		{
			//sWidth = (int)Display.image.Width;
			//sHeight = (int)Display.image.Height;
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
			List<Task> tasks = new List<Task>();

			// Get the center of the screen
			Vector2 center = new Vector2((int)Display.image.Width / 2, (int)Display.image.Height / 2);
			int iw = (int)Display.image.Width;
			int ih = (int)Display.image.Height;
			for(int y = 0; y < Display.image.Height - 1; y++)
			{
				int realY = y;
				var t = new Task(() => {
					RenderRow(center, realY, ih, iw);
				});
				tasks.Add(t);
				t.Start();
			}
			//while (tasks.Any(t=> !t.IsCompleted)) { }
			//Thread.Sleep(100);
			Task.WaitAll(tasks.ToArray());


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

		public static void RenderRow(Vector2 center, int yp, int h, int w)
		{
			for (int x = 0; x < w; x++)
			{

				// Note: Camera is always at (0, 0, 0) and is pointing (0, 0, 1)
				// Get ray X and Y directions for the corresponding pixel
				float xv = (x - center.x) / (float)h;
				float yv = -(yp - center.y) / (float)h;
				
				Reflection(null, new Vector3(0, 0, 0.1f), new Vector3(xv, yv, 1f).normalized, x, yp, 0);
				
				
			}
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
		public float intensity = 3f;

		public LightSource ()
		{
			direction = new Vector3();
		}

		public LightSource (Vector3 dir)
		{
			direction = dir.normalized;
		}
	}
	
}
