using UnityEngine;

namespace RPG.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform targetTransform;
        [SerializeField] Vector3 offset;

        [SerializeField] float pitch = 2f;
        [SerializeField] float zoomSpeed = 4f;
        [SerializeField] float minZoom = 5f;
        [SerializeField] float maxZoom = 20f;
        [SerializeField] float yawSpeed = 100f;
        [SerializeField] float currentZoom = 10f;
        [SerializeField] float currentYaw = 0f;
        void Update()
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            if (Input.GetMouseButton(2))
            {
                currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
            }
        }
        private void LateUpdate()
        {

            transform.position = targetTransform.position - offset * currentZoom;
            transform.LookAt(targetTransform.position + Vector3.up * pitch);
            transform.RotateAround(targetTransform.position, Vector3.up, currentYaw);
        }


    }
}

