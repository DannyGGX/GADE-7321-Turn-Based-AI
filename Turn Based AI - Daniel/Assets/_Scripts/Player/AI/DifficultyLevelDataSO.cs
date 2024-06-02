using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DannyG
{
    
    [CreateAssetMenu(fileName = "Difficulty Level Data", menuName = "Scriptable Object/Difficulty Level Data")]
    public class DifficultyLevelsDataSO : ScriptableObject
    {
        public DifficultyLevel[] DifficultyLevels = new DifficultyLevel[2];

        public DifficultyLevel GetDifficultyLevel(DifficultyNames difficultyName)
        {
            return Array.Find(DifficultyLevels, difficultyLevel => difficultyLevel.difficultyName == difficultyName);
        }
    }

    [System.Serializable]
    public struct DifficultyLevel
    {
        public DifficultyNames difficultyName;
        
        [FormerlySerializedAs("searchDepth")] [Min(1)] public int maxDepth;
        
        [Header("Utility Function Variables")]
        public bool hasCountNumberOf3Connected;
        public bool hasCountNumberOfConnect4Possibilities;
    }
    
    public enum DifficultyNames
    {
        Easy = 0,
        Hard = 1,
    }
}