using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab; // Prefab del planeta
    public int numberOfPlanets = 8; // Número de planetas a instanciar
    public int max_rad;

    private void OnDestroy()
    {
        NavigationManager.instance.onZoom = null;
    }
    private void Start()
    {
        // Instanciar los planetas en posiciones circulares
        for (int i = 0; i < numberOfPlanets; i++)
        {

            float radius = Random.Range(0, max_rad);
            float angle = i * Mathf.PI * 2 / numberOfPlanets;
            
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            GameObject _fab = Instantiate(planetPrefab, position, Quaternion.identity, transform); // Añadir transform para anidar
            NavigationManager.instance.onZoom += _fab.GetComponent<ExoplanetInteact>().OnZoom;
        }
    }

}
