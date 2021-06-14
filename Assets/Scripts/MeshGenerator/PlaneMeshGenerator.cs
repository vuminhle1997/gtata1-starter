using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class PlaneMeshGenerator: MonoBehaviour
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRender;
        private float time;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRender = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            var plane = new Plane(150, 150);
            var mesh = plane.GetMesh();

            meshFilter.mesh = mesh;
            MakeBackfaceVisible();
            transform.localScale = new Vector3(.3f, 1, .3f);
        }
        
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
    public class Plane
    {
        private List<int> triangles;
        private List<Vector3> vertices;
        private List<Vector3[]> segmentsList;
        private List<Vector2> uvs;

        private int xDir;
        private int zDir;

        private Mesh mesh;

        public Plane(int xDir, int zDir)
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

            uvs = new List<Vector2>();
            
            for (var x = 0; x < xDir; x++)
            {
                var verticesOfSegment = new Vector3[zDir];
                for (var z = 0; z < zDir; z++)
                {
                    var fraction = (z / (float) zDir);
                    var _x = x;
                    var _y = 0;
                    var _z = z;

                    var vertex = new Vector3(_x, _y, _z);
                    var uv = new Vector2(_x, _z);

                    uvs.Add(uv);
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
                    triangles.Add(i4);
                    triangles.Add(i3);
                    triangles.Add(i1);
                    
                    triangles.Add(i2);
                    triangles.Add(i4);
                    triangles.Add(i1);
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
            mesh.uv = uvs.ToArray();
            
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

    public class AnotherPlane
    {
        private Mesh mesh;
        private int x;
        private int y;
        public AnotherPlane(int x, int y)
        {
            this.x = x;
            this.y = y;
            mesh = new Mesh();
            GenerateMeshData();
        }
        
        public AnotherPlane()
        {
            this.x = 100;
            this.y = 100;
            mesh = new Mesh();
            
            GenerateMeshData();
        }

        private void GenerateMeshData()
        {
            var subdivisions = new Vector2Int(x, y);
            var vertexSize = subdivisions + new Vector2Int(1, 1);

            var vertices = new Vector3[vertexSize.x * vertexSize.y];
            var uvs = new Vector2[vertices.Length];

            for (var _y = 0; _y < vertexSize.y; _y++)
            {
                var v = (1f / subdivisions.y) * _y;
                for (var _x = 0; _x < vertexSize.x; _x++)
                {
                    var u = (1f / subdivisions.x) * _x;

                    var vertex = new Vector3(u, v, 0);

                    var uv = new Vector2(u, v);

                    var index = _x + (_y * vertexSize.x);

                    vertices[index] = vertex;
                    uvs[index] = uv;
                }
            }

            var triangles = new int[subdivisions.x * subdivisions.y * 6];

            for (var i = 0; i < subdivisions.x * subdivisions.y; i++)
            {
                var triangleIndex = (i % subdivisions.x) + (i / subdivisions.x) + vertexSize.x;
                var indexer = i * 6;

                triangles[indexer + 0] = triangleIndex;
                triangles[indexer + 1] = triangleIndex + subdivisions.x + 1;
                triangles[indexer + 2] = triangleIndex + 1;

                triangles[indexer + 3] = triangleIndex + 1;
                triangles[indexer + 4] = triangleIndex + subdivisions.x + 1;
                triangles[indexer + 5] = triangleIndex + subdivisions.x + 2;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
        }

        public Mesh GetMesh()
        {
            return mesh;
        }
    }
}