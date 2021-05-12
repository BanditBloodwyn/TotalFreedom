using System;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Scripts.Gameplay.Characters.Attributes
{
    public class AttributeContainer
    {
        public BodyAttributes Body { get; set; }
        public MindAttributes Mind { get; set; } 
        public CharacterAttributes Character { get; set; }
        public SkillAttributes Skills { get; set; }

        public Tuple<string, int>[] GetBody()
        {
            PropertyInfo[] props = Body.GetType().GetProperties();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();

            foreach (PropertyInfo prop in props)
                list.Add(new Tuple<string, int>(prop.Name, (int)prop.GetValue(Body)));

            return list.ToArray();
        }

        public Tuple<string, int>[] GetMind()
        {
            PropertyInfo[] props = Mind.GetType().GetProperties();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();

            foreach (PropertyInfo prop in props)
                list.Add(new Tuple<string, int>(prop.Name, (int)prop.GetValue(Mind)));

            return list.ToArray();
        }
        public Tuple<string, int>[] GetCharacter()
        {
            PropertyInfo[] props = Character.GetType().GetProperties();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();

            foreach (PropertyInfo prop in props)
                list.Add(new Tuple<string, int>(prop.Name, (int)prop.GetValue(Character)));

            return list.ToArray();
        }
        public Tuple<string, int>[] GetSkills()
        {
            PropertyInfo[] props = Skills.GetType().GetProperties();
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();

            foreach (PropertyInfo prop in props)
                list.Add(new Tuple<string, int>(prop.Name, (int)prop.GetValue(Skills)));

            return list.ToArray();
        }
    }
}