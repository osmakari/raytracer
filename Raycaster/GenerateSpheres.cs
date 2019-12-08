using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycaster
{
    public class GenerateSpheres
    {

        public List<Sphere> sphereList = new List<Sphere>();
        public GenerateSpheres()
        {
            sphereList.Add(new Sphere(new Vector3(8f, 4f, 15f), 0.9f));
            sphereList.Add(new Sphere(new Vector3(6f, 3f, 29f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(4f, 2f, 11f), 0.4f));
            sphereList.Add(new Sphere(new Vector3(5f, 1f, 27f), 0.6f));
            sphereList.Add(new Sphere(new Vector3(5f, 7f, 09f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(5f, 3f, 23f), 0.6f));
            sphereList.Add(new Sphere(new Vector3(9f, 4f, 28f), 1.1f));
            sphereList.Add(new Sphere(new Vector3(5f, 0f, 04f), 1.5f));
            sphereList.Add(new Sphere(new Vector3(6f, 9f, 24f), 1.6f));
            sphereList.Add(new Sphere(new Vector3(1f, 6f, 11f), 0.7f)); // 10
            sphereList.Add(new Sphere(new Vector3(7f, 6f, 09f), 0.4f));
            sphereList.Add(new Sphere(new Vector3(3f, 0f, 16f), 0.9f));
            sphereList.Add(new Sphere(new Vector3(5f, 3f, 17f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(2f, 0f, 17f), 1.5f));
            sphereList.Add(new Sphere(new Vector3(0f, 0f, 64f), 1.3f));
            sphereList.Add(new Sphere(new Vector3(8f, 4f, 12f), 0.1f));
            sphereList.Add(new Sphere(new Vector3(5f, 4f, 15f), 0.7f));
            sphereList.Add(new Sphere(new Vector3(5f, 4f, 14f), 0.9f));
            sphereList.Add(new Sphere(new Vector3(8f, 9f, 28f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(5f, 4f, 05f), 1.8f));
            sphereList.Add(new Sphere(new Vector3(9f, 2f, 01f), 0.5f));//20
            sphereList.Add(new Sphere(new Vector3(7f, 2f, 06f), 1.3f));
            sphereList.Add(new Sphere(new Vector3(5f, 3f, 05f), 0.7f));
            sphereList.Add(new Sphere(new Vector3(7f, 1f, 23f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(0f, 0f, 15f), 1.4f));
            sphereList.Add(new Sphere(new Vector3(1f, 7f, 14f), 1.7f));
            sphereList.Add(new Sphere(new Vector3(8f, 0f, 06f), 1.9f));
            sphereList.Add(new Sphere(new Vector3(4f, 5f, 16f), 1.8f));
            sphereList.Add(new Sphere(new Vector3(5f, 7f, 23f), 0.7f));
            sphereList.Add(new Sphere(new Vector3(5f, 0f, 22f), 0.3f));
            sphereList.Add(new Sphere(new Vector3(1f, 9f, 23f), 1.9f));//30
            sphereList.Add(new Sphere(new Vector3(7f, 7f, 05f), 1.1f));
            sphereList.Add(new Sphere(new Vector3(5f, 6f, 21f), 0.7f));
            sphereList.Add(new Sphere(new Vector3(3f, 3f, 17f), 1.9f));
            sphereList.Add(new Sphere(new Vector3(3f, 4f, 01f), 1.1f));
            sphereList.Add(new Sphere(new Vector3(3f, 7f, 15f), 1.1f));
            sphereList.Add(new Sphere(new Vector3(1f, 4f, 19f), 1.7f));
            sphereList.Add(new Sphere(new Vector3(3f, 4f, 25f), 0.4f));
            sphereList.Add(new Sphere(new Vector3(5f, 9f, 23f), 1.1f));
            sphereList.Add(new Sphere(new Vector3(3f, 0f, 07f), 1.4f));
            sphereList.Add(new Sphere(new Vector3(8f, 4f, 15f), 1.5f));//40
            sphereList.Add(new Sphere(new Vector3(2f, 6f, 13f), 0.6f));
            sphereList.Add(new Sphere(new Vector3(0f, 5f, 25f), 1.4f));
            sphereList.Add(new Sphere(new Vector3(1f, 1f, 21f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(5f, 5f, 28f), 1.5f));
            sphereList.Add(new Sphere(new Vector3(4f, 6f, 02f), 0.8f));
            sphereList.Add(new Sphere(new Vector3(1f, 4f, 21f), 1.2f));
            sphereList.Add(new Sphere(new Vector3(6f, 3f, 16f), 0.3f));
            sphereList.Add(new Sphere(new Vector3(8f, 9f, 08f), 0.9f));
            sphereList.Add(new Sphere(new Vector3(0f, 8f, 17f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(1f, 4f, 04f), 0.6f));//50
             sphereList.Add(new Sphere(new Vector3(4f, 4f, 06f), 1.8f));
             sphereList.Add(new Sphere(new Vector3(2f, 7f, 27f), 1.4f));
             sphereList.Add(new Sphere(new Vector3(1f, 2f, 15f), 1.4f));
             sphereList.Add(new Sphere(new Vector3(4f, 2f, 06f), 0.6f));
             sphereList.Add(new Sphere(new Vector3(7f, 9f, 02f), 0.6f));
             sphereList.Add(new Sphere(new Vector3(0f, 0f, 16f), 1.4f));
             sphereList.Add(new Sphere(new Vector3(6f, 3f, 11f), 0.6f));
             sphereList.Add(new Sphere(new Vector3(2f, 9f, 17f), 1.2f));
             sphereList.Add(new Sphere(new Vector3(7f, 2f, 22f), 0.5f));
             sphereList.Add(new Sphere(new Vector3(6f, 5f, 27f), 0.6f));
             sphereList.Add(new Sphere(new Vector3(1f, 1f, 03f), 0.6f));
             sphereList.Add(new Sphere(new Vector3(3f, 5f, 15f), 1.1f));
             sphereList.Add(new Sphere(new Vector3(6f, 2f, 15f), 0.3f));
             sphereList.Add(new Sphere(new Vector3(4f, 2f, 05f), 1.5f));
             sphereList.Add(new Sphere(new Vector3(7f, 0f, 28f), 0.2f));
             sphereList.Add(new Sphere(new Vector3(6f, 9f, 14f), 0.2f));
             sphereList.Add(new Sphere(new Vector3(4f, 2f, 08f), 1.3f));
             sphereList.Add(new Sphere(new Vector3(5f, 4f, 25f), 0.5f));
             sphereList.Add(new Sphere(new Vector3(2f, 7f, 13f), 1.9f));
             sphereList.Add(new Sphere(new Vector3(2f, 8f, 20f), 0.2f));
             sphereList.Add(new Sphere(new Vector3(3f, 7f, 16f), 0.5f));
             sphereList.Add(new Sphere(new Vector3(7f, 0f, 09f), 1.3f));
             sphereList.Add(new Sphere(new Vector3(0f, 8f, 17f), 0.8f));
             sphereList.Add(new Sphere(new Vector3(1f, 8f, 18f), 1.8f));
             sphereList.Add(new Sphere(new Vector3(7f, 6f, 09f), 0.1f));
             sphereList.Add(new Sphere(new Vector3(1f, 1f, 02f), 0.1f));
             sphereList.Add(new Sphere(new Vector3(4f, 3f, 21f), 1.7f));
             sphereList.Add(new Sphere(new Vector3(5f, 9f, 08f), 0.4f));
             sphereList.Add(new Sphere(new Vector3(9f, 5f, 12f), 0.7f));
             sphereList.Add(new Sphere(new Vector3(7f, 5f, 14f), 0.9f));
             sphereList.Add(new Sphere(new Vector3(7f, 2f, 25f), 0.7f));
             sphereList.Add(new Sphere(new Vector3(6f, 6f, 25f), 0.8f));
             sphereList.Add(new Sphere(new Vector3(9f, 2f, 28f), 0.3f));
             sphereList.Add(new Sphere(new Vector3(3f, 3f, -5f), 0.3f));
             sphereList.Add(new Sphere(new Vector3(0f, 8f, 03f), 1.9f));
             sphereList.Add(new Sphere(new Vector3(3f, 8f, 26f), 1.5f));
             sphereList.Add(new Sphere(new Vector3(1f, 0f, 05f), 1.2f));
             sphereList.Add(new Sphere(new Vector3(9f, 8f, 23f), 0.8f));
             sphereList.Add(new Sphere(new Vector3(9f, 8f, 29f), 0.9f));
             sphereList.Add(new Sphere(new Vector3(2f, 0f, 21f), 0.5f));
             sphereList.Add(new Sphere(new Vector3(5f, 7f, 21f), 1.5f));
             sphereList.Add(new Sphere(new Vector3(4f, 5f, 21f), 0.5f));
             sphereList.Add(new Sphere(new Vector3(0f, 6f, 11f), 1.2f));
             sphereList.Add(new Sphere(new Vector3(4f, 6f, 03f), 1.5f));
             sphereList.Add(new Sphere(new Vector3(0f, 5f, 14f), 0.6f));
             sphereList.Add(new Sphere(new Vector3(3f, 1f, 25f), 1.1f));
             sphereList.Add(new Sphere(new Vector3(0f, 6f, 02f), 0.8f));
             sphereList.Add(new Sphere(new Vector3(3f, 9f, 13f), 1.9f));
             sphereList.Add(new Sphere(new Vector3(3f, 8f, 22f), 0.2f));
            sphereList.Add(new Sphere(new Vector3(3f, 8f, 22f), 0.2f));

            Random r = new Random();
            foreach (Sphere s in sphereList)
            {
                s.color = new Color((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
            }
        }
    }
}
