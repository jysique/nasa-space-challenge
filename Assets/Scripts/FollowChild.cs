using UnityEngine;

public class FollowChild : MonoBehaviour
{
    // Referencia al GameObject hijo
    public Transform child;

    void Update()
    {
        // Asegurarse de que el padre sigue la posici�n del hijo
        if (child != null)
        {
            transform.position = child.position;
        }
    }
}
