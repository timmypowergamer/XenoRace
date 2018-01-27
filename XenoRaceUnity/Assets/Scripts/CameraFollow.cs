using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyanDStandard
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject Target;       //reference to the player game object
        public Vector3 Offset;         //offset distance between the player and camera

        // Use this for initialization
        void Start()
        {

        }

        // LateUpdate is called after Update each frame
        void LateUpdate()
        {
            if (Target != null)
            {
                // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
                transform.position = Target.transform.position + Offset;
            }
        }
    }
}