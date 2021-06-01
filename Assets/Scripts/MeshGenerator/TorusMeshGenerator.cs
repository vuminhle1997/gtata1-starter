using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator
{
    public class TorusMeshGenerator : MonoBehaviour
    {
        private MeshFilter meshFilter;

        private MeshRenderer meshRenderer;


        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            GenerateTorus();
        }

        private void GenerateTorus()
        {
            Torus torus = new Torus();
            Mesh mesh = torus.GetMesh();

            Debug.Log(torus.Vertices);
            Debug.Log(torus.Vertices);

            meshFilter.mesh = mesh;
        }
    }

    public class Torus
    {
        // segments of larger circle
        private int torusSegments = 12;
        // segments of circle around point P
        private int tubeSegments = 32;

        private float majorRadius = 2f; // R
        private float minorRadius = 0.25f; // r
        private Mesh mesh;

        public Vector3[] Vertices
        {
            get;
            set;
        }

        public int[] Triangles
        {
            get;
            set;
        }

        public Torus()
        {
            mesh = new Mesh();
            Vector3[] vertices = new Vector3[torusSegments * tubeSegments];
            List<Vector3[]> segmentsList = new List<Vector3[]>();
            // create vertices
            for (int i = 0; i < torusSegments; i++)
            {
                Vector3[] vertexesOfSegment = new Vector3[tubeSegments];
                var phi = (2 * Math.PI / torusSegments) * i;
                for (int j = 0; j < tubeSegments; j++)
                {
                    var theta = (2 * Math.PI / tubeSegments) * j;
                    var x = (float) ((majorRadius + minorRadius * Math.Cos(theta)) * Math.Cos(phi));
                    var y = (float) ((majorRadius + minorRadius * Math.Cos(theta)) * Math.Sin(phi));
                    var z = (float) (minorRadius * Math.Sin(theta));
                    Vector3 vertex = new Vector3(x, y, z);

                    var index = i * tubeSegments + j;
                    vertexesOfSegment[j] = vertex;
                    vertices[index] = vertex;
                    // vertexesOfSegment = vertexesOfSegment.Append(vertex).ToArray();
                    // vertices = vertices.Append(vertex).ToArray();
                }
                segmentsList.Add(vertexesOfSegment);
            }
            
            // create triangles and indices
            // total vertices * primitives * indices (3 = vertex for one primitive [GL_TRIANGLE])
            int totalTriangles = ((torusSegments * tubeSegments) * 2) * 3;
            List<int> triangles = new List<int>();
            for (int i = 0; i < torusSegments; i++)
            {
                var next = (i + 1) % torusSegments;
                var currentTorusTubeRing = segmentsList[i];
                var nextTorusTubeRing = segmentsList[next];

                for (int j = 0; j < currentTorusTubeRing.Length; j++)
                {
                    var _next = (j + 1) % currentTorusTubeRing.Length;

                    var v1 = currentTorusTubeRing[i];
                    var v2 = currentTorusTubeRing[_next];
                    var v3 = nextTorusTubeRing[_next];
                    var v4 = nextTorusTubeRing[j];

                    // v1 --- v4    
                    // |  \    |
                    // |   \   |
                    // |    \  |   
                    // v2 --- v3
                    var i1 = Array.IndexOf(vertices, v1);
                    var i2 = Array.IndexOf(vertices, v2);
                    var i3 = Array.IndexOf(vertices, v3);
                    var i4 = Array.IndexOf(vertices, v4);
                    
                    triangles.Add(i1);
                    triangles.Add(i2);
                    triangles.Add(i3);

                    triangles.Add(i1);
                    triangles.Add(i3);
                    triangles.Add(i4);
                }
            }

            Vertices = vertices;
            Triangles = triangles.ToArray();
            
            this.mesh.vertices = vertices;
            this.mesh.triangles = triangles.ToArray();
            this.mesh.RecalculateBounds();
            this.mesh.Optimize();
        }

        public Mesh GetMesh()
        {
            

            return mesh;
        }
    }
}
