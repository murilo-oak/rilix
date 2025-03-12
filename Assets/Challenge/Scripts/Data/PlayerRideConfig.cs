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
        [Tooltip("The minimum speed the player can reach.")]
        public float minSpeed = 1f;

        [Min(0.01f)]
        [Tooltip("The maximum speed the player can reach.")]
        public float maxSpeed = 10f;
        
        
        [Header("Movement Influence")]
        [Tooltip("The influence of gravity on the player's speed. The higher the value, the more gravity accelerates the movement downwards.")]
        [Range(0, 1)] public float gravityInfluence = 0.5f;

        [Tooltip("A damping factor for the player's speed, simulating air resistance or other limiting forces. The higher the value, the more the movement is softened.")]
        [Range(0, 1)] public float speedDamping = 0.9f;


    }
}
