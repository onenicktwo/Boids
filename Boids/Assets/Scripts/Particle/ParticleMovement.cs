using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    private ParticleController pc;

    private void Awake()
    {
        pc = this.GetComponent<ParticleController>();
    }
    public Vector3 alignment()
    {
        Vector3 v = Vector3.zero;
        if (pc.neighbors.Count != 0)
        {
            foreach (ParticleController n in pc.neighbors)
            {
                v.x += n.rb2d.velocity.x;
                v.y += n.rb2d.velocity.y;
            }
            v /= pc.neighbors.Count;
            v.Normalize();
        }
        return v;
    }

    public Vector3 cohesion()
    {
        Vector3 v = Vector3.zero;
        if (pc.neighbors.Count != 0)
        {
            foreach (ParticleController n in pc.neighbors)
            {
                v += n.gameObject.transform.position;
            }
            v /= pc.neighbors.Count;
            v -= pc.gameObject.transform.position;
            v.Normalize();
        }
        return v;
    }

    public Vector3 seperation()
    {
        Vector3 v = Vector3.zero;
        if (pc.neighbors.Count != 0)
        {
            foreach (ParticleController n in pc.neighbors)
            {
                v += n.gameObject.transform.position - pc.gameObject.transform.position;
            }
            v *= -1;
            v.Normalize();
        }
        return v;
    }
}
