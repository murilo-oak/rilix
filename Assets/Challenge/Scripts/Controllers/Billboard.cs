using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RilixHalloweenChallenge
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] Transform _camTf;


        private void Update()
        {
            transform.LookAt(Camera.main.transform);    
        }
    }
}
