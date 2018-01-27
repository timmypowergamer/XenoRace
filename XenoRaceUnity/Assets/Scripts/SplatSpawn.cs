using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatSpawn : MonoBehaviour
{
    public GameObject splatPrefab;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>();

    // Use this for initialization
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        int totalCollisions = part.GetCollisionEvents(other, events);

        foreach (ParticleCollisionEvent thing in events)
        {
            GameObject obj = Instantiate(splatPrefab, thing.intersection, Quaternion.Euler(thing.normal));
            //Debug.Log("col= " + thing.intersection);
            //Debug.Break();
            obj.transform.Rotate(0f, Random.Range(0f, 240f), 0f);
        }
    }
}