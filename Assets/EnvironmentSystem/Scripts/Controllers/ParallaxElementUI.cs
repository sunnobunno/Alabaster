using Alabaster.DialogueSystem;
using Alabaster.DialogueSystem.Controllers;
using Alabaster.DialogueSystem.Utilities;
using Alabaster.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Alabaster.EnvironmentSystem
{
    public class ParallaxElementUI : DialogueElement
    {
        [SerializeField] private Vector2 parallaxPositionMultiplier;
        [SerializeField] private Vector2 parallaxRotationMultiplier;
        //[SerializeField] private float parallaxAcceleration;
        [SerializeField] private bool active;
        [SerializeField] private float maxRotation;

        private Vector2 mouseRelativeToCenter;
        private Vector2 scale;
        private Vector2 parallaxTarget;
        private RectTransform rectTransform;
        private Vector3 eulerAngleOffset;
        private Vector3 positionOffset;
        private Vector3 initialPivot;

        public bool Active { get { return active; } set { active = value; } }


        protected override void Awake()
        {
            SetReferences();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            SetFields();
        }

        protected override void SetReferences()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        protected override void SetFields()
        {
            mouseRelativeToCenter = ParallaxEnvironmentController.Instance.MouseWorldPosRelativeToCenter;
            scale = transform.localScale;
            eulerAngleOffset = rectTransform.localEulerAngles;
            positionOffset = rectTransform.localPosition;
            initialPivot = rectTransform.pivot;
        }

        private void UpdateFields()
        {
            mouseRelativeToCenter = ParallaxEnvironmentController.Instance.MouseScreenPosRelativeToCenter;
        }

        public void InitializeElement<T>(T initializeData)
        {
            InitializeChild<T>(initializeData);
            ResizeElement();
        }

        public void InitializeChild<T>(T initializeData)
        {

        }

        public override void GreyOut(bool isGrey)
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameStateController.GameState == EGameState.Environment && active)
            {
                UpdateFields();
                UpdateTransformByParallax();
            }
        }

        private void UpdateTransformByParallax()
        {
            //rectTransform.pivot = new Vector2(0.5f, 0.5f);
            
            Vector3 mouseFromElement = Input.mousePosition - Camera.main.WorldToScreenPoint(rectTransform.position);
            //Debug.Log($"mouseFromElement: {mouseFromElement}, mouse position: {Input.mousePosition}, position: {rectTransform.position}");

            rectTransform.localPosition = (Vector3)(mouseFromElement * parallaxPositionMultiplier) + positionOffset;
            //parallaxTarget = ParallaxEnvironmentController.Instance.MouseRelativeToCenter * parallaxMultiplier;
            //AccelerateToParallaxTarget();

            //var nativeEulerAngle = rectTransform.localEulerAngles;
            
            float x = -mouseFromElement.x;
            float y = mouseFromElement.y;

            var eulerAngle = new Vector3(y, x, 0f);
            var multiplier = new Vector2(parallaxRotationMultiplier.y, parallaxRotationMultiplier.x);

            Vector3 newEulerAngle = (Vector3)(eulerAngle * multiplier) + eulerAngleOffset ;
            rectTransform.localEulerAngles = Vector3.ClampMagnitude(newEulerAngle, maxRotation);
            //Debug.Log(newEulerAngle);

            //Debug.Log(rectTransform.pivot);
            //rectTransform.pivot = initialPivot;
        }

        public override void ResizeElement()
        {
            if (isResized) return;
            
            CallBacks.VoidCallBackWithGameObject callBack = ResizeEnd;
            ElementResizer.EndOfFrameResizeElementByChildrenSizeDelta(this, callBack);
        }

        private void ResizeEnd(GameObject gameObject)
        {
            positionOffset = rectTransform.localPosition;
            isResized = true;
            Debug.Log("resize end");
        }

        //private void AccelerateToParallaxTarget()
        //{
        //    var magnitude = parallaxAcceleration;
        //    var currentPosition = (Vector2)transform.localPosition;
        //    var targetPosition = parallaxTarget;
        //    var positionDifference = targetPosition - currentPosition;
        //    var positionDifferenceAcceleration = positionDifference.normalized * magnitude;

        //    //if (positionDifference.magnitude <= magnitude)
        //    //{
        //    //    return;
        //    //}

        //    transform.localPosition += (Vector3)positionDifferenceAcceleration;
        //}
    }


}

