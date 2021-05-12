using Assets.Scripts.Gameplay.Characters.Attributes;
using UnityEngine;

public class Human : MonoBehaviour
{
    public AttributeContainer Attributes { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        Attributes = new AttributeContainer();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
