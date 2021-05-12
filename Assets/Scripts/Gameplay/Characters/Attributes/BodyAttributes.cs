using System.ComponentModel;

namespace Assets.Scripts.Gameplay.Characters.Attributes
{
    public struct BodyAttributes
    {
        [DefaultValue(1)]
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Agility { get; set; }
        public int Reflexes { get; set; }
    }
}