using UnityEngine;
using Cinemachine;
namespace RPG.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera currentCamera;

        [SerializeField] float zoomSpeed = 5f;
        [SerializeField] float minZoom = 8f;
        [SerializeField] float maxZoom = 15f;
        [SerializeField] float yawSpeed = 100f;
        [SerializeField] float currentZoom;
        [SerializeField] float currentYaw = 0f;

		private void Start()
		{
            currentCamera = GetComponent<CinemachineVirtualCamera>();
            
        }

		public void MoveCamera()
		{
            
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
			if (Input.GetMouseButton(2))
			{
				currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
				if (currentYaw >= 359.9) { currentYaw = 0; }
                if (currentYaw >= -359.9) { currentYaw = 0; }


            }
            currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = currentZoom;
        }

		private void LateUpdate()
        {
            MoveCamera();
        }


    }
}

