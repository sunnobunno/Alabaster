using Alabaster.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.EnvironmentSystem
{
    public class ParallaxElement : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxPositionMultiplier;
        [SerializeField] private Vector2 parallaxRotationMultiplier;
        [SerializeField] private float parallaxAcceleration;

        private Vector2 mouseRelativeToCenter;
        private Vector2 scale;
        private Vector2 parallaxTarget;


        private void Awake()
        {
            SetReferences();
        }

        // Start is called before the first frame update
        void Start()
        {
            SetFields();
        }

        private void SetReferences()
        {

        }

        private void SetFields()
        {
            mouseRelativeToCenter = ParallaxEnvironmentController.Instance.MouseWorldPosRelativeToCenter;
            scale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameStateController.GameState == EGameState.Environment)
            {
                UpdateTransformByParallax();
            }
        }

        private void UpdateTransformByParallax()
        {
            transform.localPosition = ParallaxEnvironmentController.Instance.MouseWorldPosRelativeToCenter * parallaxPositionMultiplier;
            //parallaxTarget = ParallaxEnvironmentController.Instance.MouseRelativeToCenter * parallaxMultiplier;
            //AccelerateToParallaxTarget();

            float x = ParallaxEnvironmentController.Instance.MouseWorldPosRelativeToCenter.x;
            float y = -ParallaxEnvironmentController.Instance.MouseWorldPosRelativeToCenter.y;

            var eulerAngle = new Vector3(y, x, 0f);
            var multiplier = new Vector2(parallaxRotationMultiplier.y, parallaxRotationMultiplier.x);

            transform.eulerAngles = eulerAngle * multiplier;
        }

        private void AccelerateToParallaxTarget()
        {
            var magnitude = parallaxAcceleration;
            var currentPosition = (Vector2)transform.localPosition;
            var targetPosition = parallaxTarget;
            var positionDifference = targetPosition - currentPosition;
            var positionDifferenceAcceleration = positionDifference.normalized * magnitude;

            //if (positionDifference.magnitude <= magnitude)
            //{
            //    return;
            //}

            transform.localPosition += (Vector3)positionDifferenceAcceleration;
        }
    }
}

