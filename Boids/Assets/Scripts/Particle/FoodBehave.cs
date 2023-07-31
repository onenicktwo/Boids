using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehave : MonoBehaviour
{
    public float energyValue = 5f;

    private void OnDestroy()
    {
        GameManager._instance.RemoveFood();
    }
}

