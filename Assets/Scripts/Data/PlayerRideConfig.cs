using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RilixHalloweenChallenge
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player Config")]
    public class PlayerRideConfig : ScriptableObject
    {
        [Header("Speed Limits")]
        [Min(0.01f)]
        public float minSpeed = 1f;

        [Min(0.01f)]
        public float maxSpeed = 10f;
        
        
        [Header("Movement Influence")]
        [Range(0, 1)] public float gravityInfluence = 0.5f;
        [Range(0, 1)] public float speedDamping = 0.9f;


    }
}
