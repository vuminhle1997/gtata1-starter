using System;
using UnityEngine;

namespace Utils
{
    public class DeleteHighScoreOnDisable: MonoBehaviour
    {
        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}