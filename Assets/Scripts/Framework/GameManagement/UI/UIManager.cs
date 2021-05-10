using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.UI.Menus;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Framework.GameManagement.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [SerializeField] private GameObject CharacterMenu;
        [SerializeField] private GameObject Inventory;
        [SerializeField] private GameObject Options;

        private readonly List<GameObject> menus = new List<GameObject>();

        public bool IsMenuOpen =>
            CharacterMenu.activeSelf ||
            Inventory.activeSelf ||
            Options.activeSelf;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);      // this object stays even if scenes are changing

            Assert.IsNotNull(CharacterMenu);
            Assert.IsNotNull(Inventory);
            Assert.IsNotNull(Options);

            menus.AddRange(new []{CharacterMenu, Inventory, Options});

            foreach (GameObject m in menus)
            {
                m.SetActive(true);
                m.GetComponent<GameMenu>().FadeOut(m);
            }

        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
                HandleMenu(CharacterMenu);
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
                HandleMenu(Inventory);
            if (Input.GetKeyDown(KeyCode.Escape))
                HandleMenu(Options);
        }

        private void HandleMenu(GameObject menu)
        {
            GameMenu gameMenu = menu.GetComponent<GameMenu>();

            if (gameMenu.visible)
                gameMenu.FadeOut(menu);
            else
            {
                foreach (GameObject m in menus.Where(m => m != menu))
                {
                    GameMenu gm = m.GetComponent<GameMenu>();
                    if (gm.visible == true)
                        gm.FadeOut(m);
                }                
                gameMenu.FadeIn(menu);
            }
        }
    }
}