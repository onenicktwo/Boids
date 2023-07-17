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
    public Vector2 alignment()
    {
        Vector2 v = Vector2.zero;
        if (pc.particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in pc.particleNeighbors)
            {
                v.x += n.rb2d.velocity.x;
                v.y += n.rb2d.velocity.y;
            }
            v /= pc.particleNeighbors.Count;
            v.Normalize();
        }
        return v;
    }

    public Vector2 cohesion()
    {
        Vector2 v = Vector2.zero;
        if (pc.particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in pc.particleNeighbors)
            {
                v.x += n.gameObject.transform.position.x;
                v.y += n.gameObject.transform.position.y;
            }
            v /= pc.particleNeighbors.Count;
            v.x -= pc.gameObject.transform.position.x;
            v.y -= pc.gameObject.transform.position.y;
            v.Normalize();
        }
        return v;
    }

    public Vector2 seperation()
    {
        Vector2 v = Vector2.zero;
        if (pc.particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in pc.particleNeighbors)
            {
                v.x += n.gameObject.transform.position.x - pc.gameObject.transform.position.x;
                v.y += n.gameObject.transform.position.y - pc.gameObject.transform.position.y;
            }
            v *= -1;
            v.Normalize();
        }
        return v;
    }

    public Vector2 nearestFood()
    {
        Vector2 v = Vector2.zero;
        if (pc.foodNeighbors.Count == 0)
            return Vector2.zero;
        for(int i = 0; i < pc.foodNeighbors.Count; i++)
        {
            if (pc.foodNeighbors[i] != null)
            {       
                if (v == Vector2.zero)
                    v = pc.foodNeighbors[i].transform.position - pc.gameObject.transform.position;
                else if ((pc.foodNeighbors[i].transform.position - pc.gameObject.transform.position).magnitude < v.magnitude)
                {
                    v = pc.foodNeighbors[i].transform.position - pc.gameObject.transform.position;
                }
            }
            else
            {
                pc.foodNeighbors.Remove(pc.foodNeighbors[i]);
                i--;
            }
        }
        v.Normalize();
        return v;
    }
}
