using UnityEngine;

namespace Assets.Scripts.Framework.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameState gameState { get; private set; }
        public OpenMenus openMenus { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if(instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);      // this object stays even if scenes are changing
        }

        // Start is called before the first frame update
        private void Start()
        {
            gameState = GameState.LoadingScreen;
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void PlayerDied()
        {
            gameState = GameState.Death;
        }

        public void GameStarted()
        {
            gameState = GameState.Playing;
        }
    }
}
