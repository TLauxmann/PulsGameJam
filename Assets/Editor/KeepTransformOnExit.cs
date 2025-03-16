using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public class KeepTransformOnExit
{
    private static Dictionary<Transform, TransformData> savedTransforms = new();

    static KeepTransformOnExit()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            SaveTransforms();
        }
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            RestoreTransforms();
        }
    }

    private static void SaveTransforms()
    {
        savedTransforms.Clear();
        foreach (KeepTransform keep in GameObject.FindObjectsOfType<KeepTransform>())
        {
            if (keep.keepTransform) // Only save if the checkbox is enabled
            {
                savedTransforms[keep.transform] = new TransformData(keep.transform);
            }
        }
    }

    private static void RestoreTransforms()
    {
        foreach (var kvp in savedTransforms)
        {
            if (kvp.Key != null) // Ensure object still exists
            {
                kvp.Value.ApplyTo(kvp.Key);
            }
        }
        savedTransforms.Clear();
    }

    private class TransformData
    {
        private Vector3 position;
        private Quaternion rotation;
        private Vector3 scale;

        public TransformData(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }

        public void ApplyTo(Transform transform)
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }
    }
}

