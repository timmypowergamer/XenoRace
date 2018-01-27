using UnityEngine;
using System.Collections;

namespace RyanDStandard
{
    namespace Events
    {
        public class KillAfter : MonoBehaviour
        {
            public float Delay = 1f;
            private float spawnTime = 0f;

            // Use this for initialization
            void Start()
            {
                spawnTime = Time.time;
            }

            // Update is called once per frame
            void Update()
            {
                if (Time.time > spawnTime + Delay)
                {
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}
