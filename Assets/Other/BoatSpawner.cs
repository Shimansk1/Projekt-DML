using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boatPrefab;
    [SerializeField] private Transform spawnPoint;

    private GameObject spawnedBoat;

    public void SpawnBoat()
    {
        if (spawnedBoat != null) return;

        spawnedBoat = Instantiate(boatPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
