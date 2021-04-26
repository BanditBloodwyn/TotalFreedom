using System;
using Assets.Scripts.World.WorldGeneration;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(TerrainGenerator))]
    public class WorldEditor : UnityEditor.Editor
    {
        private TerrainGenerator world;
        private UnityEditor.Editor shapeEditor;
        private UnityEditor.Editor colorEditor;

        public override void OnInspectorGUI()
        {
            using EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope();

            base.OnInspectorGUI();

            DrawSettingsEditor(world.ShapeSettings, null, ref world.shapeSettingsFoldout, ref shapeEditor);
        }

        private void DrawSettingsEditor(Object settings, Action onSettingsUpdated, ref bool foldout, ref UnityEditor.Editor editor)
        {
            if (settings == null)
                return;

            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

            using EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope();
            if (foldout)
            {
                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();

                if (check.changed)
                    onSettingsUpdated?.Invoke();
            }
        }

        private void OnEnable()
        {
            world = (TerrainGenerator)target;
        }
    }
}