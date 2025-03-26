using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int rockHealth = 5;
    public GameObject stonePrefab;
    public float respawnTime = 15f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private MeshRenderer meshRenderer;
    private Collider rockCollider;

    private bool hasPickaxe = true;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        meshRenderer = GetComponent<MeshRenderer>();
        rockCollider = GetComponent<Collider>();
    }

    public void MineRock()
    {
        if (!hasPickaxe) return;

        rockHealth--;

        if (rockHealth <= 0)
        {
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                Vector3 spawnPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f));
                GameObject stone = Instantiate(stonePrefab, spawnPos, Quaternion.identity);
                StartCoroutine(AdjustHeight(stone));
            }

            StartCoroutine(RespawnRock());
            meshRenderer.enabled = false;
            rockCollider.enabled = false;
        }
    }

    private IEnumerator RespawnRock()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rockHealth = 5;

        meshRenderer.enabled = true; // Znovu zobrazíme kámen
        rockCollider.enabled = true; // Aktivujeme kolizi
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
