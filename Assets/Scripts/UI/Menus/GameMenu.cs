using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Menus
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private float FadeSpeed = 10;

        private CanvasGroup canvasGroup;

        // Start is called before the first frame update
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            StartCoroutine("FadeInCoroutine");
        }

        public void FadeOut()
        {
            if (!gameObject.activeSelf)
                return;

            StartCoroutine("FadeOutCoroutine");
        }

        private IEnumerator FadeInCoroutine()
        {
            transform.localScale = new Vector3(0, 1, 1);
            canvasGroup.alpha = 0;

            while (transform.localScale.x < 1.0f)
            {
                transform.localScale += new Vector3(FadeSpeed * Time.deltaTime, 0, 0);
                canvasGroup.alpha = transform.localScale.x;
                yield return null;
            }
        }

        private IEnumerator FadeOutCoroutine()
        {

            transform.localScale = Vector3.one;
            canvasGroup.alpha = 1;

            while (transform.localScale.x > 0)
            {
                transform.localScale -= new Vector3(FadeSpeed * Time.deltaTime, 0, 0);
                canvasGroup.alpha = transform.localScale.x;
                yield return null;
            }

            gameObject.SetActive(false);
            yield return null;
        }
    }
}
