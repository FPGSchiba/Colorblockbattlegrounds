using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnDeath : MonoBehaviour
{
    [SerializeField]
    GameObject Block;
    [GreyOut]
    [SerializeField]
    float counter;
    
    public void BlockSpawn(float Blocks)
    {

        GameObject spawn = GameObject.Find("DeathBlockSpawn");

        do
        {
            GameObject block = Instantiate(Block, spawn.transform);
            Rigidbody rb =  block.GetComponent<Rigidbody>();
            rb.AddForce(transform.up * 100);
            counter++;

        } while (counter < Blocks);

    }
}
