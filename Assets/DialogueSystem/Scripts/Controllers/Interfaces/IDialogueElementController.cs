using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.DialogueSystem
{
    public interface IDialogueElementController<T>
    {
        public void InitializeElement(T initializeData);

        public void ResizeElement();

        public void GreyOutElement(bool isGrey);
    }
}