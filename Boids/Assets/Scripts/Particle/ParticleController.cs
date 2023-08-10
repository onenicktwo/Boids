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

    public float maxX;
    public float maxY;
    public float turnFactor = 1f;

    public float initEnergy = 1f;
    public float currEnergy;

    public List<ParticleController> particleNeighbors = new List<ParticleController>();
    public List<GameObject> foodNeighbors = new List<GameObject>();

    // Reducing method call lag
    [HideInInspector]
    private Transform particleTransform;
    [HideInInspector]
    public Vector2 globalPosition;

    [HideInInspector]
    private ParticleMovement movement;
    [HideInInspector]
    public ParticleReproduction reproduction;
    [HideInInspector]
    public Rigidbody2D rb2d;

    public float speed = 1f;
    public float maxSpeed = 3f;
    private Vector2 ali;
    public float aliWeight = 1f;
    private Vector2 coh;
    public float cohWeight = 1f;
    private Vector2 sep;
    public float sepWeight = 1f;

    private bool active = true;
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

    public bool isBusy = false;
    private Vector2 mate;
    public float reproducePercent = .7f;
    public float maxReproduceWeight = 3f;
    public float currReproduceWeight = 0f;
    public float reproductionEnergyUse = 1f;
    // Determines who makes the child call
    public bool selected = false;
    // This will be set when the particle is instantiated
    public string flockID;


    /*
    // Sight
    public float sightRadius = 1f;
    private BoxCollider2D boxCollider;
    private int particleLayer = 1 << 6;
    private int foodLayer = 1 << 7;
    */

    private void Awake()
    {
        particleTransform = this.transform;
        globalPosition = particleTransform.position;
        maxX = GameManager._instance.maxX;
        maxY = GameManager._instance.maxY;

        // boxCollider = this.GetComponent<BoxCollider2D>();
        movement = this.GetComponent<ParticleMovement>();
        rb2d = this.GetComponent<Rigidbody2D>();
        reproduction = this.GetComponent<ParticleReproduction>();
        rb2d.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb2d.velocity = rb2d.velocity.normalized * speed;
        
        // Set the properties based on the current flock
        GameManager.Flock assignedFlock = GameManager._instance.GetFlockByID(flockID);
        if (assignedFlock != null)
        {
            aliWeight = assignedFlock.alignmentWeight;
            cohWeight = assignedFlock.cohesionWeight;
            sepWeight = assignedFlock.separationWeight;
            initEnergy = assignedFlock.initEnergy;
        }
        else
        {
            aliWeight = Random.Range(.5f, 1.5f);
            cohWeight = Random.Range(.5f, 1.5f);
            sepWeight = Random.Range(.5f, 1.5f);
        }
        currEnergy = initEnergy;
        // StartCoroutine(GetNewNeighbors());
        StartCoroutine(GetNewVelocity());
    }

    private void Update()
    {
        if (!isBusy)
        {
            globalPosition = particleTransform.position;
            StateControl();
            Border();
            UpdateEnergy();
            Look();

            if (currState == States.Reproduce)
                reproduction.Check();
        }
    }
   
    /*
    private IEnumerator GetNewNeighbors()
    {
        while (active)
        {
            List<Collider2D> colliders = new List<Collider2D>(Physics2D.OverlapCircleAll(globalPosition, sightRadius, particleLayer));
            colliders.Remove(boxCollider);
            particleNeighbors.Clear();
            foreach (Collider2D collider in colliders)
            {
                particleNeighbors.Add(collider.GetComponent<ParticleController>());
            }

            colliders = new List<Collider2D>(Physics2D.OverlapCircleAll(globalPosition, sightRadius, foodLayer));
            foodNeighbors.Clear();
            foreach (Collider2D collider in colliders)
            {
                foodNeighbors.Add(collider.gameObject);
            }

            yield return new WaitForSeconds(.1f);
        }
    }
    */

    private IEnumerator GetNewVelocity()
    {
        while (active)
        {
            yield return new WaitForSeconds(.05f);
            if (!isBusy)
            {
                ali = movement.alignment(particleNeighbors) * aliWeight;
                coh = movement.cohesion(particleNeighbors, globalPosition) * cohWeight;
                sep = movement.seperation(particleNeighbors, globalPosition) * sepWeight;
                food = movement.nearestFood(foodNeighbors, globalPosition) * currHungryWeight;
                
                if (!reproduction.onCooldown)
                    mate = movement.nearestAvailableMate(particleNeighbors, globalPosition) * currReproduceWeight;
                else
                    mate = Vector2.zero;
                
                Vector2 newVelocity = rb2d.velocity + ali + coh + sep + food + mate;
                newVelocity = newVelocity.normalized * speed;

                rb2d.velocity = Vector2.Lerp(rb2d.velocity, newVelocity, .05f);

                if (rb2d.velocity.magnitude > maxSpeed)
                {
                    rb2d.velocity = rb2d.velocity.normalized * maxSpeed;
                }
            }
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
        float posX = globalPosition.x;
        float posY = globalPosition.y;

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
            // Kill effect
            GameManager._instance.RemoveParticle(this.gameObject);
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
        if (collision.GetType() == typeof(CapsuleCollider2D))
        {
            Transform parent = collision.gameObject.transform.parent;
            if (parent.TryGetComponent<ParticleController>(out ParticleController pc))
            {
                if (particleNeighbors.IndexOf(pc) < 0)
                    particleNeighbors.Add(pc);
            }
        }
        if (collision.TryGetComponent<FoodBehave>(out FoodBehave fb))
        {
            foodNeighbors.Add(fb.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetType() == typeof(CapsuleCollider2D))
        {
            Transform parent = collision.gameObject.transform.parent;
            if (parent.TryGetComponent<ParticleController>(out ParticleController pc))
            {
                int indexOfController = particleNeighbors.IndexOf(pc);
                if (indexOfController >= 0)
                    particleNeighbors.RemoveAt(particleNeighbors.IndexOf(pc));
            }
        }
        if(collision.TryGetComponent<FoodBehave>(out FoodBehave fb))
        {
            int indexOfFood = foodNeighbors.IndexOf(fb.gameObject);
            if (indexOfFood >= 0)
                foodNeighbors.RemoveAt(foodNeighbors.IndexOf(fb.gameObject));
        }
    }
    
}
