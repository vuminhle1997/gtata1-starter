using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class SphereMeshGenerator : MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private MeshCollider meshCollider;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
        }

        void Start()
        {
            var sphere = new Sphere(64, 64, 1f);
            var mesh = sphere.GetMesh();

            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
            meshCollider.convex = true;
        }
    }

    public class Sphere
    {
        private int latitude;
        private int longitude;
        private float radius;
        private List<Vector3> vertices;
        private List<Vector3[]> segmentsList;
        private List<Vector2> uvs;
        private List<int> triangles;
        private Mesh mesh;

        /// <summary>
        /// Source: https://stackoverflow.com/a/4082020
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="radius"></param>
        public Sphere(int latitude, int longitude, float radius)
        {
            mesh = new Mesh();
            this.latitude = latitude;
            this.longitude = longitude;
            this.radius = radius;

            GenerateVerticesAndUvs();
            GenerateTriangles();
            GenerateMeshData();
        }

        #region Initializer

        /// <summary>
        /// Attaches the generated vertices and triangles to the mesh.
        /// Recalculates the bounds and optimizes the mesh.
        /// </summary>
        private void GenerateMeshData()
        {
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();
        }

        /// <summary>
        /// Generates the vertices for the mesh data
        /// </summary>
        private void GenerateVerticesAndUvs()
        {
            vertices = new List<Vector3>();
            segmentsList = new List<Vector3[]>();

            uvs = new List<Vector2>();
            for (var m = 0; m <= latitude; m++)
            {
                var verticesOfSegment = new Vector3[longitude];
                for (var n = 0; n < longitude; n++)
                {
                    var x = Mathf.Sin(Mathf.PI * m/latitude) * Mathf.Cos(2 * Mathf.PI * n/longitude);
                    var y = Mathf.Sin(Mathf.PI * m/latitude) * Mathf.Sin(2 * Mathf.PI * n/longitude);
                    var z = Mathf.Cos(Mathf.PI * m /latitude);

                    var uv = new Vector2(x, y);
                    var vertex = new Vector3(x, y, z) * radius;
                    verticesOfSegment[n] = vertex;
                    uvs.Add(uv);
                    vertices.Add(vertex);
                }
                segmentsList.Add(verticesOfSegment);
            }
        }

        /// <summary>
        /// Generates the triangles for the Mesh data
        /// </summary>
        private void GenerateTriangles()
        {
            triangles = new List<int>();
            Vector3[] tempArray = vertices.ToArray();
            for(var m = 0; m < latitude; m++)
            {
                var next = (m + 1) % latitude;
                if (m == latitude-1)
                {
                    next = latitude;
                }
                var currentArea = segmentsList[m];
                var nextArea = segmentsList[next];
                for (var n = 0; n < currentArea.Length; n++)
                {
                    var _next = (n + 1) % currentArea.Length;
            
                    var v1 = currentArea[n];
                    var v2 = currentArea[_next];
                    var v3 = nextArea[n];
                    var v4 = nextArea[_next];
            
                    var i1 = Array.IndexOf(tempArray, v1);
                    var i2 = Array.IndexOf(tempArray, v2);
                    var i3 = Array.IndexOf(tempArray, v3);
                    var i4 = Array.IndexOf(tempArray, v4);
                    
                    // draws first triangle (v1-v2-v3)
                    triangles.Add(i1);
                    triangles.Add(i2);
                    triangles.Add(i3);
                    // draws second triangle (v3-v4-v1)
                    // triangles.Add(i3);
                    // triangles.Add(i4);
                    // triangles.Add(i1);
                    
                    // I don't know what I did here
                    // but it works for generating sphere faces
                    triangles.Add(i2);
                    triangles.Add(i4);
                    triangles.Add(i3);
                }
            }
        }

        #endregion

        /// <summary>
        /// Returns Mesh
        /// </summary>
        /// <returns>Mesh - the mash data</returns>
        public Mesh GetMesh()
        {
            return mesh;
        }
    }
}
