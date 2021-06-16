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
            var wave = new Wave(30, 30);
            var mesh = wave.GetMesh();

            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
            meshCollider.convex = true;
            MakeBackfaceVisible();

            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        private void Update()
        {
            // time += 50f * Time.deltaTime;
            // transform.rotation = Quaternion.Euler(0f, 0f, time);
        }

        /// <summary>
        /// Solution: http://answers.unity.com/answers/723483/view.html
        /// Makes the backface of this mesh visible!
        /// </summary>
        private void MakeBackfaceVisible()
        {
            var mesh = meshFilter.mesh;
            var vertices = mesh.vertices;
            var verticesLength = vertices.Length;
            var newVerts = new Vector3[verticesLength * 2];
            for (var j = 0; j < verticesLength; j++){
                // duplicate vertices and uvs:
                newVerts[j] = newVerts[j+verticesLength] = vertices[j];
            }
            var triangles = mesh.triangles;
            var trianglesLength = triangles.Length;
            var newTris = new int[trianglesLength * 2]; // double the triangles
            for (var i=0; i < trianglesLength; i+=3){
                // copy the original triangle
                newTris[i] = triangles[i];
                newTris[i+1] = triangles[i+1];
                newTris[i+2] = triangles[i+2];
                // save the new reversed triangle
                var j = i+trianglesLength; 
                newTris[j] = triangles[i]+verticesLength;
                newTris[j+2] = triangles[i+1]+verticesLength;
                newTris[j+1] = triangles[i+2]+verticesLength;
            }
            mesh.vertices = newVerts;
            mesh.triangles = newTris; // assign triangles last!
        }
    }

    /// <summary>
    /// Implemented by myself without any help
    /// </summary>
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
            mesh = new Mesh();
            
            GenerateVertices();
            GenerateTriangles();
            GenerateMeshData();
        }

        #region Initializer

        /// <summary>
        /// Generates vertices for this mesh
        /// </summary>
        private void GenerateVertices()
        {
            vertices = new List<Vector3>();
            segmentsList = new List<Vector3[]>();

            for (var x = 0; x < xDir; x++)
            {
                var verticesOfSegment = new Vector3[zDir];
                for (var z = 0; z < zDir; z++)
                {
                    var fraction = (z / (float) zDir);
                    var _x = x;
                    var _y = (float) Math.Sin((2 * Math.PI) * fraction) * 3f;
                    var _z = z;

                    var vertex = new Vector3(_x, _y, _z);

                    verticesOfSegment[z] = vertex;
                    vertices.Add(vertex);
                }
                segmentsList.Add(verticesOfSegment);
            }
        }

        /// <summary>
        /// Generates triangle indices for this mesh
        /// </summary>
        private void GenerateTriangles()
        {
            triangles = new List<int>();
            var tempArray = vertices.ToArray();
            for (var x = 0; x < xDir-1; x++)
            {
                var next = (x + 1) % xDir;
                var currArea = segmentsList[x];
                var nextArea = segmentsList[next];
                for (var z = 0; z < zDir-1; z++)
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

        /// <summary>
        /// Generates mesh data by attaching the vertices and triangles to mesh
        /// </summary>
        private void GenerateMeshData()
        {
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            mesh.Optimize();
        }

        #endregion

        #region Getters

        /// <summary>
        /// Returns mesh
        /// </summary>
        /// <returns></returns>
        public Mesh GetMesh()
        {
            return mesh;
        }

        #endregion
    }
}