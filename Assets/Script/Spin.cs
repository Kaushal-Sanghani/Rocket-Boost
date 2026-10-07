using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float xAngle = 0f;
    [SerializeField] float yAngle = 0f;
    [SerializeField] float zAngle = 0f;

    void Update()
    {
        transform.Rotate(xAngle , yAngle, zAngle);
    }
}
