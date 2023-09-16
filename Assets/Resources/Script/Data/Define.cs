using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneType
{ 
    public class Define
    {
        public enum SceneType
        {
            First,
            Lobby,
            Game
        }
        public SceneType sceneType;

        public enum Pattern
        {
            EnemySpawn,
            MeteorRain,
            FireBreath,
            Boss
        }
        public Pattern pattern;
    }
}
