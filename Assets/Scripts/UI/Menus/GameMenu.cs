using UnityEngine;

namespace Assets.Scripts.UI.Menus
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private MenuFader menuFader;
        [HideInInspector] public bool visible => menuFader.visible;

        // Start is called before the first frame update
        private void Start()
        {
            menuFader ??= new MenuFader();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void FadeIn(GameObject menu)
        {
            menu.SetActive(true);
            StartCoroutine(menuFader.FadeInCoroutine(menu));
        }

        public void FadeOut(GameObject menu)
        {
            if (!menu.activeSelf) 
                return;
            StartCoroutine(menuFader.FadeOutCoroutine(menu));
        }
    }
}
