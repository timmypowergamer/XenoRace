using UnityEngine;
using System.Collections;

namespace RyanDStandard
{
    namespace Physics
    {
        public class BouncePlate : MonoBehaviour
        {

            public float bounce = 10.0f;
            public Vector3 Direction = new Vector3(0f, 1f, 0f);

            void Start()
            {
                if (GetComponent<Collider>())
                {
                    GetComponent<Collider>().isTrigger = true;
                }
            }

            void OnTriggerEnter(Collider other)
            {
                if (other.GetComponent<Rigidbody>())
                {
                    other.GetComponent<Rigidbody>().AddForce(bounce * Direction, ForceMode.Impulse);
                }
            }

            void OnTriggerStay(Collider other)
            {
                if (other.GetComponent<Rigidbody>())
                {
                    other.GetComponent<Rigidbody>().AddForce(bounce * Direction, ForceMode.Impulse);
                }
            }

            void OnDrawGizmos()
            {
                Gizmos.color = Color.cyan;
                if (GetComponent<BoxCollider>())
                {
                    BoxCollider c = GetComponent<BoxCollider>();
                    Gizmos.DrawWireCube(transform.position, c.bounds.size);
                }
            }
        }
    }
}