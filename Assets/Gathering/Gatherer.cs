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
        // TODO: Tady p�idej logiku pro kontrolu invent��e
        return false; // Moment�ln� v�dy false, ale m��e� propojit s invent��em
    }
}
