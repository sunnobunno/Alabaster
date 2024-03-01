using Alabaster.DialogueSystem;
using Alabaster.DialogueSystem.Controllers;
using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueBoxBackground : MonoBehaviour, IDialogueElementClickable<IFlowObject>
{
    
    public bool Hover { get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hover = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
