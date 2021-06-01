using System;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Deletes the children in the score ladder,
    /// if the player returns back to the main menu
    /// </summary>
    public class DeleteHighScoreOnDisable: MonoBehaviour
    {
        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}