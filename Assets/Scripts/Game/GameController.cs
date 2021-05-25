using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameWorldInitializer gameWorldInitializer; // the script
        private AudioSource _audio; // the attached music in the game world
        private void Start()
        {
            _audio = gameObject.GetComponent<AudioSource>();

            var settings = gameWorldInitializer.settings;
            _audio.volume = (settings._musicLevel / 100);
            if (!settings._enableMusic)
            {
                _audio.Stop();
            }
            else
            {
                _audio.Play();
            }
        }
    }
}
