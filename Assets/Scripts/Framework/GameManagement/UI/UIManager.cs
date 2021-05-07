using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Framework.GameManagement.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [SerializeField] private GameObject CharacterMenu;
        [SerializeField] private GameObject Inventory;

        private readonly List<GameObject> menus = new List<GameObject>();

        public bool IsMenuOpen =>
            CharacterMenu.activeSelf ||
            Inventory.activeSelf;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);      // this object stays even if scenes are changing

            Assert.IsNotNull(CharacterMenu);
            Assert.IsNotNull(Inventory);

            menus.AddRange(new []{CharacterMenu, Inventory});
        }

        // Start is called before the first frame update
        private void Start()
        {
            CharacterMenu.SetActive(false);
            Inventory.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
                HandleMenu(CharacterMenu);
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
                HandleMenu(Inventory);
        }

        private void HandleMenu(GameObject menu)
        {
            if(menu.activeSelf)
                menu.SetActive(false);
            else
            {
                foreach (GameObject m in menus.Where(m => m != menu))
                    m.SetActive(false);
                menu.SetActive(true);
            }
        }
    }
}