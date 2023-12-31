using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    private ParticleController pc;
    public float radius = 1f;
    private void Awake()
    {
        pc = this.GetComponent<ParticleController>();
    }
    public Vector2 alignment(List<ParticleController> particleNeighbors)
    {
        Vector2 v = Vector2.zero;
        if (particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in particleNeighbors)
            {
                if(n != null && !n.isBusy)
                    v += (Vector2) (n.rb2d.velocity);
            }
            v /= particleNeighbors.Count;
        }
        return v;
    }

    public Vector2 cohesion(List<ParticleController> particleNeighbors, Vector2 globalPosition)
    {
        Vector2 v = Vector2.zero;
        if (particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in particleNeighbors)
            {
                if(n != null && !n.isBusy)
                    v += n.globalPosition;
            }
            v /= particleNeighbors.Count;
            v -= globalPosition;  
        }
        return v;
    }

    public Vector2 seperation(List<ParticleController> particleNeighbors, Vector2 globalPosition)
    {
        Vector2 v = Vector2.zero;
        int nAvoid = 0;
        if (particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in particleNeighbors)
            {
                if (n != null &&
                    Vector2.Distance(n.globalPosition, globalPosition) < radius && 
                    !n.isBusy)
                {
                    nAvoid++;
                    v += (Vector2)(globalPosition - n.globalPosition);
                }
            }
            if(nAvoid > 0)
                v /= nAvoid;
        }
        return v;
    }

    public Vector2 nearestFood(List<GameObject> foodNeighbors, Vector2 globalPosition)
    {
        Vector2 v = Vector2.zero;
        if (foodNeighbors.Count == 0)
            return Vector2.zero;
        for(int i = 0; i < foodNeighbors.Count; i++)
        {
            if (foodNeighbors[i] != null)
            {       
                if (v == Vector2.zero)
                    v = (Vector2) foodNeighbors[i].transform.position - globalPosition;
                else if (((Vector2) foodNeighbors[i].transform.position - globalPosition).magnitude < v.magnitude)
                {
                    v = (Vector2) foodNeighbors[i].transform.position - globalPosition;
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

    public Vector2 nearestAvailableMate(List<ParticleController> particleNeighbors, Vector2 globalPosition)
    {
        Vector2 v = Vector2.zero;
        if (particleNeighbors.Count == 0)
            return Vector2.zero;
        for (int i = 0; i < particleNeighbors.Count; i++)
        {
            if (particleNeighbors[i] != null)
            {
                if (!particleNeighbors[i].isBusy &&
                    !particleNeighbors[i].reproduction.onCooldown &&
                    (particleNeighbors[i].selected && !GetComponent<ParticleController>().selected) ||
                    (!particleNeighbors[i].selected && GetComponent<ParticleController>().selected))
                {
                    if (v == Vector2.zero)
                    {
                        v = particleNeighbors[i].globalPosition - globalPosition;
                    }
                    else if ((particleNeighbors[i].globalPosition - globalPosition).magnitude < v.magnitude)
                    {
                        v = particleNeighbors[i].globalPosition - globalPosition;
                    }
                }
            }
            else
            {
                pc.particleNeighbors.Remove(pc.particleNeighbors[i]);
                i--;
            }
        }
        v.Normalize();
        return v;
    }
}
