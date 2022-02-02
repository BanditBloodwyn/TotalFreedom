using UnityEngine;

namespace Assets.Scripts.Framework.GameManagement.Game
{
    public class GameManager : MonoBehaviour
    {
        public GameState gameState { get; private set; }

        private void Awake()
        {

        }

        // Start is called before the first frame update
        private void Start()
        {
            gameState = GameState.LoadingScreen;
        }

        // Update is called once per frame
        private void Update()
        {
            //if (Input.GetKey(KeyCode.Escape))
            //    Application.Quit();
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
