using Alabaster.EnvironmentSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugValue01 : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshPro;

    private void Awake()
    {
        SetReferences();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SetReferences()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TextMeshPro.text = ParallaxEnvironmentController.Instance.MouseWorldPosRelativeToCenter.ToString();
    }
}
