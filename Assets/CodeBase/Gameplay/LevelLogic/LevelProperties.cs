using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [CreateAssetMenu]
    public class LevelProperties : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private string _sceneName;
        [SerializeField] private Sprite _previewImage;
        [SerializeField] private LevelProperties _nextLevel;

        public string Title => _title;
        public string SceneName => _sceneName;
        public Sprite PreviewImage => _previewImage;
        public LevelProperties NextLevel => _nextLevel;


    }
}

