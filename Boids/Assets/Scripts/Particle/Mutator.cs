using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutator : MonoBehaviour
{
    public bool active;
    /*
    private void Awake()
    {
        StartCoroutine(MutateCheck());
    }


    private IEnumerator MutateCheck()
    {
        while (active)
        {
            yield return new WaitForSeconds(.1f);
            int randNum = Random.Range(0, 100);

            if (randNum < GameManager._instance.mutationChance)
            {
                float geneMutateNum = Mathf.Abs(gene1 - gene2) * GameManager._instance.mutationFactor;
                if (randNum % 2 == 0)
                    geneAmnt -= geneAmnt * GameManager._instance.mutationFactor;
                else
                    geneAmnt += geneAmnt * GameManager._instance.mutationFactor;
            }
        }
    }
    */
}
