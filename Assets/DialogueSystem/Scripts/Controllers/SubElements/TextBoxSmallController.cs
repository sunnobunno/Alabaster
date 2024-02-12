using Articy.Little_Guy_Syndrome;
using Assets.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBoxSmallController : MonoBehaviour, IDialogueElementControllerWithContent
{
    [SerializeField] private GameObject contentObject;
    [SerializeField] private GameObject centerObject;
    [SerializeField] private float contentMinWidth;

    private TextMeshProUGUI contentTextMesh;
    private IDialogueElementControllerWithContent contentObjectController;
    private RectTransform contentObjectRectTransform;
    private RectTransform centerObjectRectTransform;

    //private float contentMarginLeft;
    //private float contentMarginRight;

    public string Content
    {
        get => contentObjectController.Content;
        set => contentObjectController.Content = value;
    }

    public UnityEngine.Color TextColor
    {
        get => contentObjectController.TextColor;
        set => contentObjectController.TextColor = value;

    }

    private void Awake()
    {
        SetElementComponentReferences();
    }

    public void InitializeElement(string initializeData)
    {
        SetContent(initializeData);
    }

    private void SetElementComponentReferences()
    {
        contentTextMesh = contentObject.GetComponent<TextMeshProUGUI>();
        contentObjectController = contentObject.GetComponent<IDialogueElementControllerWithContent>();
        contentObjectRectTransform = contentObject.GetComponent<RectTransform>();
        centerObjectRectTransform = centerObject.GetComponent<RectTransform>();

        //contentMarginLeft = contentObjectRectTransform.transform.localPosition.x;
        //contentMarginRight = contentObjectRectTransform.transform.localPosition.x;
    }

    public void ResizeElement()
    {
        float contentPreferredWidth = contentTextMesh.preferredWidth;

        contentObjectRectTransform.sizeDelta = new Vector2(contentPreferredWidth, contentObjectRectTransform.sizeDelta.y);
        centerObjectRectTransform.sizeDelta = new Vector2(contentPreferredWidth, centerObjectRectTransform.sizeDelta.y);

        //Debug.Log(contentObjectRectTransform.sizeDelta);
        //Debug.Log(centerObjectRectTransform.sizeDelta);

        GetComponent<RectTransform>().sizeDelta = RectTransformSizeFitter.GetSizeOfChildren(this.gameObject);
    }

    public void SetContent(string content)
    {
        Content = content;
        ResizeElement();
    }

    public void GreyOutElement(bool isGrey)
    {
        contentObjectController.GreyOutElement(isGrey);
    }
}
