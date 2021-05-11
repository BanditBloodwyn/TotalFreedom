namespace Assets.Scripts.Gameplay.Characters.Attributes
{
    public class AttributeContainer
    {
        public BodyAttributes Body { get; set; }
        public MindAttributes Mind { get; set; } 
        public CharacterAttributes Character { get; set; }
        public CombatSkillAttributes Combat { get; set; }
        public CraftingSkillAttributes Crafting { get; set; }
        public CriminalSkillAttributes Criminal { get; set; }
        public JobSkillAttributes Job { get; set; }
        public MiscSkillAttributes Misc { get; set; }
        public MovementPerks MovementPerks { get; set; }
        public SpecialPerks SpecialPerks { get; set; }
    }
}