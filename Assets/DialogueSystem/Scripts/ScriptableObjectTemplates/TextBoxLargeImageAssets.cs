using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DialogueSystem {

}

[CreateAssetMenu(fileName = "TextBoxLargeImageAsset", menuName = "ScriptableObjects/CreateTextBoxLargeImageAsset")]
public class TextBoxLargeImageAssets : ScriptableObject
{
    public Sprite center;
    public Sprite top;
    public Sprite bottom;
    public Sprite left;
    public Sprite right;
    public Sprite topLeft;
    public Sprite topRight;
    public Sprite bottomLeft;
    public Sprite bottomRight;
}
