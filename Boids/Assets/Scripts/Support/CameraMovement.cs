using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private TMP_Text particleInfoText;
    [SerializeField] private TMP_Text instructionText;

    private Camera cam;
    private Vector3 dragOrigin;
    [SerializeField]
    private Transform targetParticle;
    private int currentParticleIndex = 0;
    public List<GameObject> particles;

    [SerializeField]
    private float zoomStep;
    [SerializeField]
    private float followSpeed = 2f;

    private void Awake()
    {
        cam = Camera.main;

        // Set default texts or hide them
        if (particleInfoText != null && instructionText != null)
        {
            particleInfoText.text = "Not Spectating";
            instructionText.text = "Right-click to spectate a particle";
        }
    }

    private void Update()
    {
        Pan();
        Zoom();
        Spectate();
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

    private void Spectate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.GetComponent<ParticleController>())
            {
                targetParticle = hit.transform;
                // Update the GUI
                UpdateParticleInfo(hit.collider.GetComponent<ParticleController>());
                UpdateInstructionText("Press Space to focus and unfocus");
            }
            else
            {
                ResetGUI();
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

    // GUI Functions
    private void UpdateParticleInfo(ParticleController particleController)
    {
        if (particleInfoText == null) return;

        string info = "Particle ID: " + particleController.particleID + "\nSpeed: " + particleController.speed;
        particleInfoText.text = info;
    }

    private void UpdateInstructionText(string instruction)
    {
        if (instructionText == null) return;

        instructionText.text = instruction;
    }

    private void ResetGUI()
    {
        if (particleInfoText == null || instructionText == null) return;

        particleInfoText.text = "Not Spectating";
        instructionText.text = "Right-click to spectate a particle";
    }
}