using System;
using UnityEngine;

namespace Assets.Scripts.World.WorldGeneration.Settings
{
    //Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
    //Modified by: Sebastian Lague

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public class ConditionalHideAttribute : PropertyAttribute
    {
        public string conditionalSourceField;
        public int enumIndex;

        public ConditionalHideAttribute(string boolVariableName)
        {
            conditionalSourceField = boolVariableName;
        }

        public ConditionalHideAttribute(string enumVariableName, int enumIndex)
        {
            conditionalSourceField = enumVariableName;
            this.enumIndex = enumIndex;
        }

    }
}