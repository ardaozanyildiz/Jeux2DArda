using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Cible � suivre")]
    public Transform target;      // drag & drop le Player ici dans l�Inspector

    [Header("D�calage")]
    public Vector3 offset = new Vector3(0, 0, -10); // en 2D, garder Z = -10

    [Header("Lissage")]
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Position d�sir�e
        Vector3 desiredPosition = target.position + offset;

        // Interpolation liss�e
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothed;
    }
}