using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.PresentationLayer.Behaviours
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    public class WeightedNormals : MonoBehaviour 
    {
        [SerializeField] private int _texcoordChannel = 3;
        [SerializeField] private float _cospatialVertexDistance = 0.01f;
        [SerializeField] private bool _useSharedMesh;

        private Mesh _mesh;
        
        private Mesh Mesh 
        {
            get
            {
                if (!_mesh)
                {
                    var mf = GetComponent<MeshFilter>();
                    _mesh = !Application.isPlaying || _useSharedMesh ? mf.sharedMesh : mf.mesh;
                }

                return _mesh;
            }
        }
        
        private class CospatialVertex 
        {
            public Vector3 position;
            public Vector3 accumulatedNormal;
        }

        [ContextMenu("Execute")]
        private void ApplyWeightedNormals()
        {
            var vertices = Mesh.vertices;
            var triangles = Mesh.triangles;
            var outlineNormals = new Vector3[vertices.Length];
            
            var cospatialVerticesData = new List<CospatialVertex>();
            var cospacialVertexIndices = new int[vertices.Length];
            FindCospatialVertices(vertices, cospacialVertexIndices, cospatialVerticesData);
            
            var numTriangles = triangles.Length / 3;
            
            for (var t = 0; t < numTriangles; t++) 
            {
                var vertexStart = t * 3;
                var index1 = triangles[vertexStart];
                var index2 = triangles[vertexStart + 1];
                var index3 = triangles[vertexStart + 2];
                ComputeNormalAndWeights(vertices[index1], vertices[index2], vertices[index3], out var normal, out var weights);
                AddWeightedNormal(normal * weights.x, index1, cospacialVertexIndices, cospatialVerticesData);
                AddWeightedNormal(normal * weights.y, index2, cospacialVertexIndices, cospatialVerticesData);
                AddWeightedNormal(normal * weights.z, index3, cospacialVertexIndices, cospatialVerticesData);
            }
            
            for (var i = 0; i < outlineNormals.Length; i++) 
            {
                var cvIndex = cospacialVertexIndices[i];
                var cospatial = cospatialVerticesData[cvIndex];
                outlineNormals[i] = cospatial.accumulatedNormal.normalized;
            }
            
            Mesh.SetUVs(_texcoordChannel, outlineNormals);
        }

        private void FindCospatialVertices(Vector3[] vertices, int[] indices, List<CospatialVertex> cospatialsList) 
        {
            for (var i = 0; i < vertices.Length; i++) 
            {
                if (IsPreviouslyFound(vertices[i], cospatialsList, out int index)) 
                {
                    indices[i] = index;
                } 
                else 
                {
                    var cospatialEntry = new CospatialVertex() 
                    {
                        position = vertices[i],
                        accumulatedNormal = Vector3.zero,
                    };
                    indices[i] = cospatialsList.Count;
                    cospatialsList.Add(cospatialEntry);
                }
            }
        }

        private bool IsPreviouslyFound(Vector3 position, List<CospatialVertex> registry, out int index) 
        {
            for (var i = 0; i < registry.Count; i++)
            {
                if (!(Vector3.Distance(registry[i].position, position) <= _cospatialVertexDistance)) continue;
                index = i;
                return true;
            }
            index = -1;
            return false;
        }

        private void ComputeNormalAndWeights(Vector3 a, Vector3 b, Vector3 c, out Vector3 normal, out Vector3 weights) 
        {
            normal = Vector3.Cross(b - a, c - a).normalized;
            weights = new Vector3(Vector3.Angle(b - a, c - a), Vector3.Angle(c - b, a - b), Vector3.Angle(a - c, b - c));
        }

        private void AddWeightedNormal(Vector3 weightedNormal, int vertexIndex, int[] cvIndices, List<CospatialVertex> cospatials) 
        {
            var cvIndex = cvIndices[vertexIndex];
            cospatials[cvIndex].accumulatedNormal += weightedNormal;
        }

        private void Awake()
        {
            ApplyWeightedNormals();
        }
    }
}