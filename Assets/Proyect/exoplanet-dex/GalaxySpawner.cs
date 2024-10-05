using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxySpawner : MonoBehaviour
{
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int numberOfSystems = 5; 
    [SerializeField] private int planetsPerSystem = 5; 
    private float minOrbitRadius = 0.5f; 
     private float maxOrbitRadius = 3f; 
    [SerializeField] private Vector2 galaxySize = new Vector2(20f, 20f);
    private void Start()
    {
        Spawn();
    }
    void Spawn()
    {
        for (int i = 0; i < numberOfSystems; i++)
        {

            Vector3 systemPosition = new Vector3(
                Random.Range(-galaxySize.x / 2, galaxySize.x / 2),
                Random.Range(-galaxySize.y / 2, galaxySize.y / 2),
                0);


            GameObject star = Instantiate(starPrefab, systemPosition, Quaternion.identity);

            for (int j = 0; j < planetsPerSystem; j++)
            {
                float orbitRadius = Random.Range(minOrbitRadius, maxOrbitRadius); 
                float orbitAngle = Random.Range(0f, 360f); 

                Vector3 planetPosition = new Vector3(
                    systemPosition.x + Mathf.Cos(orbitAngle * Mathf.Deg2Rad) * orbitRadius,
                    systemPosition.y + Mathf.Sin(orbitAngle * Mathf.Deg2Rad) * orbitRadius,
                    0);

                GameObject planet = Instantiate(planetPrefab, planetPosition, Quaternion.identity);

                planet.GetComponent<PlanetOrbit>().InitializeOrbit(star.transform, orbitRadius, orbitAngle);
            }
        }
    }
}

