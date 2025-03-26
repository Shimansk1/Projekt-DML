using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherer : MonoBehaviour
{
    public float interactionRange = 2f; 
    public int baseDamage = 1; 
    public int axeDamage = 2;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    hit.collider.GetComponent<Tree>()?.ChopTree();
                }
                else if (hit.collider.CompareTag("Rock"))
                {
                    hit.collider.GetComponent<Rock>()?.MineRock();
                }
            }
        }
    }



    private bool HasAxe()
    {
        // TODO: Tady pøidej logiku pro kontrolu inventáøe
        return false; // Momentálnì vždy false, ale mùžeš propojit s inventáøem
    }
}
