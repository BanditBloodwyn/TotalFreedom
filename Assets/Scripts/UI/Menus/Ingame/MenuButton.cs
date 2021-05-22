using Assets.Scripts.Framework.GameManagement.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.UI.Menus.Ingame
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject MenuToOpen;
        
        // Start is called before the first frame update
        private void Start()
        {
            Assert.IsNotNull(MenuToOpen);
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void OnButtonClicked()
        {
            UIManager.instance.HandleMenu(MenuToOpen);
        }
    }
}
