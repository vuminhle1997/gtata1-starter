using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameWorldInitializer gameWorldInitializer;
        private AudioSource _audio;
        private void Awake()
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
