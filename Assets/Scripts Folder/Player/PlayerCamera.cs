using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;
    [SerializeField] float camSpeed, rotSpeed;
    float yRotation, xRotation;

    void LateUpdate()
    {
        Vector3 targetPosition = cameraTarget.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, camSpeed * Time.deltaTime);
    }

    void Update()
    {

    }

    public void Rotate(Vector2 rotation)
    {
        yRotation += rotation.x * rotSpeed * Time.deltaTime;
        xRotation += rotation.y * rotSpeed * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 60f);
        transform.localRotation = Quaternion.Euler(-xRotation, yRotation, 0f);
    }
}
