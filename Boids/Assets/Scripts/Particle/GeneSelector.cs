using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneSelector : MonoBehaviour
{
    public static bool GetGeneBool()
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 0)
            return true;
        else
            return false;
    }
    public static float GetGeneFloat(float gene1, float gene2)
    {
        float geneAmnt = gene1 < gene2 ? Random.Range(gene1, gene2) : Random.Range(gene2, gene2);
        return geneAmnt;
    }
}
