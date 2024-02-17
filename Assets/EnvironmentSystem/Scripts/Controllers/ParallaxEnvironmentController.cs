using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace EnvironmentSystem
{
    public class ParallaxEnvironmentController : MonoBehaviour
    {
        // In order for parallax to work
        // Need position of mouse
        // Need center position of screen
        // Alter position of environment assets by a multiplier based on mouse's distance away from center of screen
        
        public static ParallaxEnvironmentController Instance { get; private set; }

        private Vector2 mousePosition;
        private float screenWidth;
        private float screenHeight;
        private Vector2 mouseRelativeToCenter;

        public Vector2 MouseRelativeToCenter { get { return mouseRelativeToCenter; } }
        public Vector2 ScreenDimensions { get { return new Vector2(screenWidth, screenHeight); } }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            SetFields();
        }

        private void SetFields()
        {
            mousePosition = Input.mousePosition;
            mouseRelativeToCenter = GetMousePositionRelativeToCenter();
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }

        // Update is called once per frame
        void Update()
        {
            mouseRelativeToCenter = GetMousePositionRelativeToCenter();
            //Debug.Log($"Screen size: {screenWidth}, {screenHeight}");
            //Debug.Log($"Mouse relative to center: {mouseRelativeToCenter}");
        }



        private Vector2 GetMousePositionRelativeToCenter()
        {
            var mousePositionClamped = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0f, screenWidth), Mathf.Clamp(Input.mousePosition.y, 0f, screenHeight));
            var worldPos = Camera.main.ScreenToWorldPoint(mousePositionClamped);
            var worldPos0Z = new Vector3(worldPos.x, worldPos.y, 0f);

            return worldPos0Z;

            //UpdateScreenDimensions();
            //var screenCenter = GetCenterOfScreen();
            //var mousePositionClamped = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0f, screenWidth), Mathf.Clamp(Input.mousePosition.y, 0f, screenHeight));
            //var mouseRelativeToCenter = mousePositionClamped - screenCenter;
            //return mouseRelativeToCenter;
        }

        private Vector2 GetCenterOfScreen()
        {
            var screenCenter = new Vector2(screenWidth / 2f, screenHeight / 2f);
            return screenCenter;
        }

        private void UpdateScreenDimensions()
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
    }
}

