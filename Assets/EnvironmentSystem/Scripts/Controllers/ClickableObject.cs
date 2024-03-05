using Alabaster.EnvironmentSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.EnvironmentSystem
{
    public class ClickableObject : MonoBehaviour, IClickableObject
    {
        public void Clicked()
        {
            Debug.Log("Hiiii");
        }
    }
}


