using System.Linq;
using Code.Infrastructure.Logic.CardSlots;
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button("Collect"))
      {
        levelData.CardSlots = FindObjectsOfType<CardSlotMarker>()
          .Select(x =>
            new CardSlotData(x.CardSpawnId, x.transform.position))
          .ToList();
      }

      EditorUtility.SetDirty(target);
    }
  }
}