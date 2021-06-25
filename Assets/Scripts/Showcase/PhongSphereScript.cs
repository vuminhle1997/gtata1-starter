using System;
using UnityEngine;

namespace Showcase
{
    public class PhongSphereScript : MonoBehaviour
    {
        [SerializeField] private float xDir, yDir, zDir;
        [SerializeField] private float ambientCoefficient, diffuseCoefficient, specularCoefficient;
        [SerializeField] private Vector4 ambientColor, diffuseColor, specularColor;
        [SerializeField] private float shininess;
        
        private MeshRenderer meshRenderer;
        private float R = 100f;
        private int Iteration = 1;
        
        void Start()
        {
            InitFields();

            // source: https://answers.unity.com/questions/122349/how-to-run-update-every-second.html
            InvokeRepeating("RotateLightPoint", 0, 1.0f);
        }

        private void Update()
        {
            ChangeShaderProperties();
        }

        #region Behaviours

        /// <summary>
        /// Changes the shader's properties every frame
        /// </summary>
        private void ChangeShaderProperties()
        {
            meshRenderer.material.SetVector("_LightPoint", new Vector4(xDir, yDir, zDir, 1f));

            meshRenderer.material.SetFloat("_AmbientCoefficient", ambientCoefficient);
            meshRenderer.material.SetFloat("_DiffuseCoefficient", diffuseCoefficient);
            meshRenderer.material.SetFloat("_SpecularCoefficient", specularCoefficient);
            meshRenderer.material.SetFloat("_Shininess", shininess);

            meshRenderer.material.SetVector("_AmbientColor", ambientColor);
            meshRenderer.material.SetVector("_DiffuseColor", diffuseColor);
            meshRenderer.material.SetVector("_SpecularColor", specularColor);
        }

        /// <summary>
        /// Rotates the light point around the sphere every second
        /// </summary>
        private void RotateLightPoint()
        {
            if (Iteration > 10) Iteration = 1;
            var t = (((float) 1 / 10) * Iteration) * (2 * (float) Math.PI);

            xDir = (float) Math.Sin(t) * R;
            zDir = (float) Math.Cos(t) * R;

            Iteration++;
        }
        
        /// <summary>
        /// Initializes the fields of this class.
        /// </summary>
        private void InitFields()
        {
            xDir = 0;
            yDir = 88f;
            zDir = 0;
            meshRenderer = GetComponent<MeshRenderer>();
            ambientCoefficient = 1f;
            diffuseCoefficient = 1f;
            specularCoefficient = -1.9f;
            shininess = 10f;

            ambientColor = new Vector4(1f, 1f, 1f, 1f);
            diffuseColor = new Vector4(1f, 1f, 1f, 1f);
            specularColor = new Vector4(1f, 1f, 1f, 1f);
        }

        #endregion
    }
}
