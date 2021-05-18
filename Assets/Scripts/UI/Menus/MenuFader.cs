using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.Menus
{
    [Serializable]
    public class MenuFader
    {
        [SerializeField] private MenuFadeDirectionStruct[] Objects;
        [SerializeField] private int FadingSpeed = 10;

        private int fadeInCounter;
        private int fadeOutCounter;
        [HideInInspector] public bool visible = true;

        public MenuFader()
        {

        }

        public IEnumerator FadeInCoroutine(GameObject menu)
        {
            visible = true;
            List<GameObject> allMenus = new List<GameObject>();
            for (int i = 0; i < menu.transform.childCount; i++)
                allMenus.Add(menu.transform.GetChild(i).gameObject);

            fadeInCounter = 0;
            while (fadeInCounter < 101)
            {
                foreach (GameObject gameObject in allMenus)
                {
                    gameObject.transform.localPosition -= GetFadingDirection(Objects.First(o => o.Menu.name == gameObject.name).Direction) * FadingSpeed;
                    gameObject.GetComponent<CanvasGroup>().alpha = fadeInCounter / 100f;
                }
                fadeInCounter += FadingSpeed;
                
                if (fadeOutCounter >= 100)
                    foreach (GameObject gameObject in allMenus)
                        gameObject.GetComponent<CanvasGroup>().alpha = 1;
                yield return null;
            }

        }

        public IEnumerator FadeOutCoroutine(GameObject menu)
        {
            visible = false;
            List<GameObject> allMenus = new List<GameObject>();
            for (int i = 0; i < menu.transform.childCount; i++)
                allMenus.Add(menu.transform.GetChild(i).gameObject);

            fadeOutCounter = 100;
            while (fadeOutCounter > -1)
            {
                foreach (GameObject gameObject in allMenus)
                {
                    gameObject.transform.localPosition += GetFadingDirection(Objects.First(o => o.Menu.name == gameObject.name).Direction) * FadingSpeed;
                    gameObject.GetComponent<CanvasGroup>().alpha = fadeOutCounter / 100f;
                }
                fadeOutCounter -= FadingSpeed;

                if(fadeOutCounter <= 0)
                    foreach (GameObject gameObject in allMenus)
                        gameObject.GetComponent<CanvasGroup>().alpha = 0;
                yield return null;
            }

            menu.SetActive(false);
        }

        private Vector3 GetFadingDirection(FadeDirection fadeDirection)
        {
            return fadeDirection switch
            {
                FadeDirection.right => new Vector3(1, 0, 0),
                FadeDirection.left => new Vector3(-1, 0, 0),
                FadeDirection.up => new Vector3(0, 1, 0),
                FadeDirection.down => new Vector3(0, -1, 0),
                FadeDirection.none => Vector3.zero,
                _ => Vector3.zero
            };
        }
    }
}
