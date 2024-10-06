using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public float zoomSpeed = 10.0f;
    public float panSpeed = 10.0f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    public System.Action<float> onZoom;

    public static NavigationManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Awakee();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Awakee()
    {
        Camera.main.orthographicSize = maxZoom;
    }
    float scroll;
    void Update()
    {
        // Zoom con la rueda del ratón
        float _scroll = Input.GetAxis("Mouse ScrollWheel");
        if(_scroll != scroll)
        {
            
            scroll = _scroll;
            float scrolled = Camera.main.orthographicSize - scroll * zoomSpeed;
            onZoom?.Invoke(scrolled);
            Camera.main.orthographicSize = Mathf.Clamp(scrolled, minZoom, maxZoom);
        }
        
        // Movimiento de la cámara con el clic derecho
        if (Input.GetMouseButton(1))
        {
            float h = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float v = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
            Camera.main.transform.Translate(-h, -v, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                // Verifica si el objeto es un planeta
                Debug.Log(" hit de planet");

            }
        }
    }
}
