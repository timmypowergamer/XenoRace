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
        part.GetCollisionEvents(other, events);

        foreach (ParticleCollisionEvent thing in events)
        {
            GameObject obj = Instantiate(splatPrefab, thing.intersection, Quaternion.Euler(thing.normal));
            obj.transform.parent = other.transform;//parent it to the thing we hit
            obj.transform.Rotate(0f, Random.Range(0f, 240f), 0f);
        }
    }
}