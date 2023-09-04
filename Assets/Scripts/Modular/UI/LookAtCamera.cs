using UnityEngine;
using UnityEngine.EventSystems;

namespace Modular.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LookAtInverse,
            CameraForward,
        }        
        
        [SerializeField] private Mode mode;
        
        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case Mode.LookAtInverse:
                    Vector3 directionFromCameraToObject = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + directionFromCameraToObject);
                    break;
                case Mode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
            }
        }
    }
}