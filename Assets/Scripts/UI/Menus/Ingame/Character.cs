using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menus.Ingame
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Human Player;
        [SerializeField] private GameObject SkillPanel;
        [SerializeField] private GameObject SkillEntryPrefab;

        private int lastSkillEntryPositionY;

        // Start is called before the first frame update
        private void Start()
        {
            Assert.IsNotNull(Player);
            Assert.IsNotNull(SkillPanel);
            Assert.IsNotNull(SkillEntryPrefab);
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void OnCategoryButtonClick(Button button)
        {
            lastSkillEntryPositionY = 150;

            for (int i = 0; i < SkillPanel.transform.childCount; i++)
                Destroy(SkillPanel.transform.GetChild(i).gameObject);

            Tuple<string, int>[] skills = GetSkills(button.name);

            foreach (Tuple<string, int> skillPropertyInfo in skills)
                CreateSkillEntry(skillPropertyInfo);
        }

        private Tuple<string, int>[] GetSkills(string buttonName)
        {
            return buttonName switch
            {
                "BodyButton" => Player.Attributes.GetBody(),
                "MindButton" => Player.Attributes.GetMind(),
                "CharacterButton" => Player.Attributes.GetCharacter(),
                "SkillsButton" => Player.Attributes.GetSkills(),
                _ => new Tuple<string, int>[0]
            };
        }

        private void CreateSkillEntry(Tuple<string, int> skillPropertyInfo)
        {
            GameObject skillEntry = Instantiate(SkillEntryPrefab, SkillPanel.transform);
            skillEntry.transform.localPosition = new Vector3(-90, lastSkillEntryPositionY, 0);
            skillEntry.GetComponent<Text>().text = skillPropertyInfo.Item1;

            GameObject skillValue = Instantiate(SkillEntryPrefab, SkillPanel.transform);
            skillValue.transform.localPosition = new Vector3(90, lastSkillEntryPositionY, 0);
            skillValue.GetComponent<Text>().text = skillPropertyInfo.Item2.ToString();

            lastSkillEntryPositionY -= 12;
        }
    }
}
