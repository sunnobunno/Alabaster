using Assets.Dialogue_System.Controllers;
using Assets.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ContentBoxController : MonoBehaviour, IDialogueElementControllerWithContent
{
    //[SerializeField] private string content;

    private TextMeshProUGUI contentTextMesh;
    private RectTransform rectTransform;

    public string Content
    {
        get => contentTextMesh.text;
        set => contentTextMesh.text = value;
    }

    public UnityEngine.Color TextColor
    {
        get => contentTextMesh.color;
        set => contentTextMesh.color = value;

    }

    private void Awake()
    {
        SetElementComponentReferences();
    }


    public void InitializeElement(string content)
    {

    }

    private void SetElementComponentReferences()
    {
        contentTextMesh = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetContent(string content)
    {
        Content = content;
    }

    public void ResizeElement()
    {
        Vector2 textMeshPreferredSize = contentTextMesh.GetPreferredValues();
        rectTransform.sizeDelta = new Vector2(DialogueUIController.Instance.DialogueWidth, textMeshPreferredSize.y);
    }

    public void GreyOutElement(bool isGrey)
    {
        if (isGrey)
        {
            contentTextMesh.color = Color.gray;
        }
        else
        {
            contentTextMesh.color = Color.white;
        }
    }
}

[CustomEditor(typeof(ContentBoxController))]
public class ContentBoxControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Resize Box"))
        {
            Selection.activeGameObject.GetComponent<ContentBoxController>().ResizeElement();
        }
    }
}
