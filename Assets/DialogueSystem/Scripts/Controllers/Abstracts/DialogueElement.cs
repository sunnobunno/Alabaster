using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.DialogueSystem
{
    public abstract class DialogueElement : MonoBehaviour
    {
        // Start is called before the first frame update
        protected virtual void Awake()
        {
            SetReferences();
            //Debug.Log($"{gameObject.name}: Element Awake");
        }

        protected virtual void Start()
        {
            SetFields();
        }

        protected abstract void SetReferences();

        protected abstract void SetFields();

        public abstract void GreyOut(bool isGrey);
    }
}


