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
            Torus torus = new Torus(12, 32, 2f, 0.25f);
            Mesh mesh = torus.GetMesh();

            meshFilter.mesh = mesh;
        }
    }

    /// <summary>
    /// sources:
    /// - https://gamedev.stackexchange.com/a/16850
    /// - DE: https://de.wikipedia.org/wiki/Torus#Parametrisierung
    /// - EN: https://en.wikipedia.org/wiki/Torus#Geometry
    /// Some code snippets where taken from this: https://forum.unity.com/threads/torus-in-unity.8487/
    /// But I tried my best to understand this and implemented my own code.
    /// </summary>
    public class Torus
    {
        // segments of larger circle
        private int torusSegments;
        // segments of circle around point P
        private int tubeSegments;

        private float majorRadius; // R
        private float minorRadius; // r
        private Mesh mesh;

        public Torus(int torusSegments, int tubeSegments, float majorRadius, float minorRadius)
        {
            this.torusSegments = torusSegments;
            this.tubeSegments = tubeSegments;
            this.majorRadius = majorRadius;
            this.minorRadius = minorRadius;
            mesh = new Mesh();
            
            GenerateVerticesAndIndices();
        }

        public Torus()
        {
            torusSegments = 12;
            tubeSegments = 32;
            majorRadius = 2f;
            minorRadius = 0.25f;
            mesh = new Mesh();
            
            GenerateVerticesAndIndices();
        }

        #region Initializer

        private void GenerateVerticesAndIndices()
        {
            Vector3[] vertices = new Vector3[torusSegments * tubeSegments];
            List<Vector3[]> segmentsList = new List<Vector3[]>();
            // create vertices
            for (int i = 0; i < torusSegments; i++)
            {
                Vector3[] vertexesOfSegment = new Vector3[tubeSegments];
                var phi = (2 * Math.PI / torusSegments) * i;
                for (int j = 0; j < tubeSegments; j++)
                {
                    // formula taken from wikipedia source
                    var theta = (2 * Math.PI / tubeSegments) * j;
                    var x = (float) ((majorRadius + minorRadius * Math.Cos(theta)) * Math.Cos(phi));
                    var y = (float) ((majorRadius + minorRadius * Math.Cos(theta)) * Math.Sin(phi));
                    var z = (float) (minorRadius * Math.Sin(theta));
                    Vector3 vertex = new Vector3(x, y, z);

                    var index = i * tubeSegments + j;
                    vertexesOfSegment[j] = vertex;
                    vertices[index] = vertex;
                }
                segmentsList.Add(vertexesOfSegment);
            }
            
            // create triangles and indices
            // total vertices * primitives * indices (3 = vertex for one primitive [GL_TRIANGLE])
            // int totalTriangles = ((torusSegments * tubeSegments) * 2) * 3;
            List<int> triangles = new List<int>();
            for (int i = 0; i < torusSegments; i++)
            {
                var next = (i + 1) % torusSegments;
                var currentTorusTubeRing = segmentsList[i];
                var nextTorusTubeRing = segmentsList[next];

                for (int j = 0; j < currentTorusTubeRing.Length; j++)
                {
                    var _next = (j + 1) % currentTorusTubeRing.Length;
                    
                    // v1 --- v4    
                    // |  \    |
                    // |   \   |
                    // |    \  |   
                    // v2 --- v3
                    var v1 = currentTorusTubeRing[j];
                    var v2 = currentTorusTubeRing[_next];
                    var v3 = nextTorusTubeRing[_next];
                    var v4 = nextTorusTubeRing[j];
                    
                    var i1 = Array.IndexOf(vertices, v1);
                    var i2 = Array.IndexOf(vertices, v2);
                    var i3 = Array.IndexOf(vertices, v3);
                    var i4 = Array.IndexOf(vertices, v4);
                    
                    // draws first triangle (v1-v2-v3)
                    triangles.Add(i1);
                    triangles.Add(i2);
                    triangles.Add(i3);
                    // draws second triangle (v3-v4-v1)
                    triangles.Add(i3);
                    triangles.Add(i4);
                    triangles.Add(i1);
                }
            }
            
            this.mesh.vertices = vertices;
            this.mesh.triangles = triangles.ToArray();
            this.mesh.RecalculateBounds();
            this.mesh.Optimize();
        }

        #endregion

        #region Getters

        public Mesh GetMesh()
        {
            return mesh;
        }

        #endregion
    }
}
