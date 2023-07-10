using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float maxX = 11.4f;
    public float maxY = 5f;

    private float initEnergy = 1f;
    private float currEnergy;

    public List<ParticleController> neighbors = new List<ParticleController>();
    private ParticleMovement movement;
    [SerializeField]
    public Rigidbody2D rb2d;
    public float speed = 1f;
    private Vector3 ali;
    public float aliWeight;
    private Vector3 coh;
    public float cohWeight;
    private Vector3 sep;
    public float sepWeight;

    private void Awake()
    {
        movement = this.GetComponent<ParticleMovement>();
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb2d.velocity = rb2d.velocity.normalized * speed;

        aliWeight = Random.Range(0f, 1f);
        cohWeight = Random.Range(0f, 1f);
        sepWeight = Random.Range(0f, 1f);

        currEnergy = initEnergy;
    }

    private void Update()
    {
        ParticleWrap();
        UpdateEnergy();
    }

    private void FixedUpdate()
    {
        ali = movement.alignment() * aliWeight;
        coh = movement.cohesion() * cohWeight;
        sep = movement.seperation() * sepWeight;
        rb2d.velocity += new Vector2(ali.x + coh.x + sep.x, ali.y + coh.y + sep.y);
        rb2d.velocity = rb2d.velocity.normalized * speed;
    }

    private void ParticleWrap()
    {
        float posX = gameObject.transform.position.x;
        float posY = gameObject.transform.position.y;
        Vector2 normVel = rb2d.velocity.normalized;
        if (posX > maxX || posX < -maxX || posY > maxY || posY < -maxY)
        {
            gameObject.transform.position *= -1;
            gameObject.transform.position += new Vector3(normVel.x, normVel.y, 0);
        }
    }

    private void UpdateEnergy()
    {
        if (currEnergy <= 0)
        {
            //Kill effect
        }
        currEnergy -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "particle")
        {
            neighbors.Add(collision.gameObject.GetComponent<ParticleController>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "particle")
        {
            int indexOfController = neighbors.IndexOf(collision.gameObject.GetComponent<ParticleController>());
            if (indexOfController >= 0)
                neighbors.RemoveAt(indexOfController);
        }
    }
}
