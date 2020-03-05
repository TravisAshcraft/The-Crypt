using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crypt.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;
        
        void Update()
        {
           transform.position = target.position;
        }
    }

}