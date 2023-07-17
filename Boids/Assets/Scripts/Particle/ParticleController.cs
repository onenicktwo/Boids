using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    /*
     * The current behaviour of the boids (going in one direction with little change)
     * is due to the lack of goal setting, since they have no reason to change once
     * they find a flock.
     * 
     * Goal Examples:
     * http://www.kfish.org/boids/pseudocode.html
     */
    public Transform gfx;

    public float maxX = 11.4f;
    public float maxY = 5f;
    public float turnFactor = 1f;

    public float initEnergy = 1f;
    public float currEnergy;

    public List<ParticleController> particleNeighbors = new List<ParticleController>();
    public List<GameObject> foodNeighbors = new List<GameObject>();
    private ParticleMovement movement;

    [HideInInspector]
    public Rigidbody2D rb2d;

    public float speed = 1f;
    public float maxSpeed = 3f;
    private Vector2 ali;
    public float aliWeight;
    private Vector2 coh;
    public float cohWeight;
    private Vector2 sep;
    public float sepWeight;

    /*
     * States should be on a fixed percentage. Ex: 0<=Hungry<40<=Content<70<=Reproduce<100
     * This needs to be put on a variable so it can be randomized later. Note: These variables
     * can heavily change the behaviour of the particles, so very high/low values might lead to
     * some issues
     * 
     * Tried to make the states more realistics by adding a max weight for hungry and reproduce.
     * It works by finding percentage of the percentage energy within the specific state (Ex: 
     * initEnergy = 1f, currEnergyPercent = .15f, hungerPercent = .3f, hungerWeightPercent = 
     * .5f, and if the max weight is 10f then the weight for the hunger vector is 5f)
     */
    public enum States { Hungry, Content, Reproduce}
    public States currState;
    public float hungryPercent = .3f;
    private Vector2 food;
    public float maxHungryWeight = 3f;
    public float currHungryWeight = 0f;
    public float reproducePercent = .7f;
    private Vector2 mate;
    public float maxReproduceWeight = 3f;
    public float currReproduceWeight = 0f;

    private void Awake()
    {
        movement = this.GetComponent<ParticleMovement>();
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb2d.velocity = rb2d.velocity.normalized * speed * Time.deltaTime;

        aliWeight = Random.Range(0f, 2f);
        cohWeight = Random.Range(0f, 2f);
        sepWeight = Random.Range(0f, 2f);

        currEnergy = initEnergy;
    }

    private void Update()
    {
        StateControl();
        Border();
        UpdateEnergy();
        Look();
    }

    private void FixedUpdate()
    {
        ali = movement.alignment() * aliWeight;
        coh = movement.cohesion() * cohWeight;
        sep = movement.seperation() * sepWeight;
        food = movement.nearestFood() * 100;
        Vector2 newVelocity = rb2d.velocity + ali + coh + sep + food;
        newVelocity = newVelocity.normalized * speed * Time.deltaTime;
        rb2d.velocity = Vector2.Lerp(rb2d.velocity, newVelocity, .05f);

        if (rb2d.velocity.magnitude > maxSpeed)
        {
            rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
        }
    }

    private void StateControl()
    {
        float currEnergyPercent = currEnergy / initEnergy;
        // currHungryWeight = (1 - currEnergyPercent) * maxHungryWeight;
        // currReproduceWeight = currEnergyPercent * maxReproduceWeight;
        if (currEnergyPercent < hungryPercent)
        {
            currState = States.Hungry;
            currHungryWeight = (1 - currEnergyPercent / hungryPercent) * maxHungryWeight;
        }
        else if (currEnergyPercent > reproducePercent)
        {
            currState = States.Reproduce;
            currReproduceWeight = (1 - (1 - currEnergyPercent) / (1 - reproducePercent)) * maxReproduceWeight;
        }
        else
        {
            currState = States.Content;
            currHungryWeight = 0f;
            currReproduceWeight = 0f;
        }
    }

    private void Border()
    {
        float posX = gameObject.transform.position.x;
        float posY = gameObject.transform.position.y;
        /*
        Vector2 normVel = rb2d.velocity.normalized;
        if (posX > maxX || posX < -maxX || posY > maxY || posY < -maxY)
        {
            gameObject.transform.position *= -1;
            gameObject.transform.position += new Vector3(normVel.x / 10, normVel.y / 10, 0);
        }
        */
        if (posX < -maxX)
            rb2d.velocity += new Vector2(turnFactor, 0);
        if (posX > maxX)
            rb2d.velocity -= new Vector2(turnFactor, 0);
        if (posY > -maxY)
            rb2d.velocity -= new Vector2(0, turnFactor);
        if (posY < maxY)
            rb2d.velocity += new Vector2(0, turnFactor);
    }

    private void UpdateEnergy()
    {
        if (currEnergy <= 0)
        {
            //Kill effect
            Debug.Log(this.gameObject.name + " Died at " + System.DateTime.Now);
            Destroy(this.gameObject);
        }
        if (currEnergy >= initEnergy)
        {
            currEnergy = initEnergy;
        }
        currEnergy -= Time.deltaTime;
    }

    private void Look()
    {
        Vector3 normTarget = rb2d.velocity.normalized;
        float angle = Mathf.Atan2(normTarget.y, normTarget.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        gfx.rotation = rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "particle")
        {
            particleNeighbors.Add(collision.gameObject.GetComponent<ParticleController>());
        }
        if (collision.gameObject.tag == "food")
        {
            foodNeighbors.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "particle")
        {
            int indexOfController = particleNeighbors.IndexOf(collision.gameObject.GetComponent<ParticleController>());
            if (indexOfController >= 0)
                particleNeighbors.RemoveAt(indexOfController);
        }
        if(collision.gameObject.tag == "food")
        {
            int indexOfFood = foodNeighbors.IndexOf(collision.gameObject);
            if (indexOfFood >= 0)
                foodNeighbors.RemoveAt(indexOfFood);
        }
    }
}
