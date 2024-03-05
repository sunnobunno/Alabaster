using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.EnvironmentSystem
{
    public class ClickController : MonoBehaviour
    {
        private Ray cameraToScreenRay;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            HandleMouseClick();
        }


        private void HandleMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                UpdateCameraToScreenRay();

                RaycastHit2D hit2D = GetHit();
                if (hit2D.collider != null)
                {
                    DebugCameraToScreenRay();
                    GameObject gameObject = hit2D.collider.gameObject;
                    _ = gameObject.TryGetComponent(out IClickableObject objectController);

                    if (objectController != null) objectController.Clicked();
                    else Debug.Log($"{gameObject.name}: No IClickableObject interface implemented");
                }
            }
        }





        private void UpdateCameraToScreenRay()
        {
            cameraToScreenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void DebugCameraToScreenRay()
        {
            var direction = cameraToScreenRay.direction * 20f;
            Debug.DrawRay(cameraToScreenRay.origin, direction, Color.yellow, 1f);
        }

        private RaycastHit2D GetHit()
        {
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(cameraToScreenRay);
            return hit2D;
        }
    }
}


