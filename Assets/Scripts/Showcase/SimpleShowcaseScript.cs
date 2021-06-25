using UnityEngine;
using UnityEngine.SceneManagement;

namespace Showcase
{
    public class SimpleShowcaseScript : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               // SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
            }
        }
    }
}
