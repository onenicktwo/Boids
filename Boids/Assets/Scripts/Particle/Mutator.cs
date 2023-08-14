using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutator : MonoBehaviour
{
    public static void MutateCheck(ParticleController controller)
    {
        int randNum = Random.Range(0, 100);

        if (randNum < GameManager._instance.mutationChance)
        {
            int randGene = Random.Range(1, 10);

            switch (randGene)
            {
                case 1:
                    controller.initEnergy = GetNewMutatedGene(randGene, controller.initEnergy);
                    break;
                case 2:
                    controller.spriteRenderer.color = GetNewMutatedColor(randGene, controller.spriteRenderer.color);
                    break;
                case 3:
                    controller.speed = GetNewMutatedGene(randGene, controller.speed);
                    break;
                case 4:
                    controller.aliWeight = GetNewMutatedGene(randGene, controller.aliWeight);
                    break;
                case 5:
                    controller.cohWeight = GetNewMutatedGene(randGene, controller.cohWeight);
                    break;
                case 6:
                    controller.sepWeight = GetNewMutatedGene(randGene, controller.sepWeight);
                    break;
                case 7:
                    controller.maxHungryWeight = GetNewMutatedGene(randGene, controller.maxHungryWeight);
                    break;
                case 8:
                    controller.maxReproduceWeight = GetNewMutatedGene(randGene, controller.maxReproduceWeight);
                    break;
                case 9:
                    controller.reproductionEnergyUse = GetNewMutatedGene(randGene, controller.reproductionEnergyUse);
                    break;
            }
        }
    }

    private static float GetNewMutatedGene(int randNum, float value)
    {
        if (randNum % 2 == 0)
            value -= value * GameManager._instance.mutationFactor;
        else
            value += value * GameManager._instance.mutationFactor;
        return value;
    }

    private static Color GetNewMutatedColor(int randNum, Color value)
    {
        if (randNum % 2 == 0)
            value -= value * GameManager._instance.mutationFactor;
        else
            value += value * GameManager._instance.mutationFactor;
        return value;
    }
}
