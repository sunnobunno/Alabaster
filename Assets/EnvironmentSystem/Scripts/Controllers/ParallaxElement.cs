using EnvironmentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxElement : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxMultiplier;
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
        mouseRelativeToCenter = ParallaxEnvironmentController.Instance.MouseRelativeToCenter;
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTransformByParallax();
    }

    private void UpdateTransformByParallax()
    {
        transform.localPosition = ParallaxEnvironmentController.Instance.MouseRelativeToCenter * parallaxMultiplier;
        //parallaxTarget = ParallaxEnvironmentController.Instance.MouseRelativeToCenter * parallaxMultiplier;
        //AccelerateToParallaxTarget();
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
