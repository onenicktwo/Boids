using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    private Vector3 dragOrigin;
    private Transform targetParticle;
    private int currentParticleIndex = 0;
    public List<GameObject> particles;
    [SerializeField]
    private float zoomStep;
    [SerializeField]
    private float followSpeed = 2f;


    private void Start()
    {
        ParticleController.OnParticleDeath += HandleParticleDeath;
    }


    private void Update()
    {
        cam = Camera.main;
        Pan();
        Zoom();
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && hit.collider.GetComponent<ParticleController>())
            {  
                targetParticle = hit.transform;
                followSpeed = targetParticle.GetComponent<ParticleController>().followSpeed;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CycleParticles(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CycleParticles(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (targetParticle != null)
            {
                targetParticle = null;
            }
            else
            {
                if (particles.Count > 0)
                {
                    targetParticle = particles[currentParticleIndex].transform;
                }
            }
        }
        if (targetParticle != null)
        {
            Vector3 targetPosition = new Vector3(targetParticle.position.x, targetParticle.position.y, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    private void Pan()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) && targetParticle == null)
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += diff;
        }
    }

    private void Zoom()
    {
        cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomStep;
        if (cam.orthographicSize < 5)
            cam.orthographicSize = 5;
    }

    private void CycleParticles(int direction)
    {
        if (particles.Count == 0) return;
        currentParticleIndex += direction;
        if (currentParticleIndex < 0) currentParticleIndex = particles.Count - 1;
        if (currentParticleIndex >= particles.Count) currentParticleIndex = 0;
        targetParticle = particles[currentParticleIndex].transform;
    }
    private void HandleParticleDeath(GameObject deadParticle)
    {
        if (targetParticle == deadParticle.transform)
        {
            // Move to default position or select the next particle
            targetParticle = null; 
        }
    }
}