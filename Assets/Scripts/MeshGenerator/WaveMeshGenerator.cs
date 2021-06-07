using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class WaveMeshGenerator: MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRender;
        private MeshCollider meshCollider;
        private float time;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
        }

        private void Start()
        {
            Wave wave = new Wave(10, 10);
            Mesh mesh = wave.GetMesh();

            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
            meshCollider.convex = true;
        }

        private void Update()
        {
            time += 50f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, time);
        }
    }

    public class Wave
    {
        private List<int> triangles;
        private List<Vector3> vertices;
        private List<Vector3[]> segmentsList;

        private int xDir;
        private int zDir;

        private Mesh mesh;

        public Wave(int xDir, int zDir)
        {
            this.xDir = xDir;
            this.zDir = zDir;
            
            GenerateVertices();
            GenerateTriangles();
            GenerateMeshData();
        }

        #region Initializer

        private void GenerateVertices()
        {
            vertices = new List<Vector3>();
            segmentsList = new List<Vector3[]>();
            mesh = new Mesh();
            
            for (int x = 0; x < this.xDir; x++)
            {
                Vector3[] verticesOfSegment = new Vector3[zDir];
                for (int z = 0; z < this.zDir; z++)
                {
                    float fraction = (z / (float) zDir);
                    float _x = x;
                    float _y = (float) Math.Sin((2 * Math.PI) * fraction);
                    float _z = z;

                    Vector3 vertex = new Vector3(_x, _y, _z);

                    verticesOfSegment[z] = vertex;
                    vertices.Add(vertex);
                }
                segmentsList.Add(verticesOfSegment);
            }
        }

        private void GenerateTriangles()
        {
            triangles = new List<int>();
            var tempArray = vertices.ToArray();
            for (int x = 0; x < xDir-1; x++)
            {
                var next = (x + 1) % xDir;
                var currArea = segmentsList[x];
                var nextArea = segmentsList[next];
                for (int z = 0; z < zDir-1; z++)
                {
                    var offset = (z + 1) % currArea.Length;
                    var v1 = currArea[z];
                    var v2 = currArea[offset];
                    var v3 = nextArea[z];
                    var v4 = nextArea[offset];

                    var i1 = Array.IndexOf(tempArray, v1);
                    var i2 = Array.IndexOf(tempArray, v2);
                    var i3 = Array.IndexOf(tempArray, v3);
                    var i4 = Array.IndexOf(tempArray, v4);
                    
                    // draws first triangle (v1-v2-v3)
                    triangles.Add(i1);
                    triangles.Add(i2);
                    triangles.Add(i3);
                    // draws second triangle (v3-v4-v1)
                    triangles.Add(i3);
                    triangles.Add(i4);
                    triangles.Add(i1);
                    
                    // I don't know what I did here
                    // but it works for generating sphere faces
                    triangles.Add(i2);
                    triangles.Add(i4);
                    triangles.Add(i3);
                }
            }
        }

        private void GenerateMeshData()
        {
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            
            mesh.RecalculateBounds();
            mesh.Optimize();
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