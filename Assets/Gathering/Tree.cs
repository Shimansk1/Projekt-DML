using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int treeHealth = 3;
    public GameObject woodPrefab;
    public float respawnTime = 10f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ChopTree()
    {
        treeHealth--;

        if (treeHealth <= 0)
        {
            for (int i = 0; i < Random.Range(3, 6); i++)
            {
                Vector3 spawnPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f));
                GameObject wood = Instantiate(woodPrefab, spawnPos, Quaternion.identity);
                StartCoroutine(AdjustHeight(wood));
            }

            StartCoroutine(RespawnTree());
            transform.position -= new Vector3(0, 100f, 0); // Schováme strom pod zem
        }
    }

    private IEnumerator RespawnTree()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        treeHealth = 3;
    }

    private IEnumerator AdjustHeight(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        if (obj != null && obj.GetComponent<Collider>() != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(obj.transform.position, Vector3.down, out hit, 1f))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GROUND"))
                {
                    obj.transform.position += new Vector3(0, 0.8f, 0);
                }
            }
        }
    }
}
