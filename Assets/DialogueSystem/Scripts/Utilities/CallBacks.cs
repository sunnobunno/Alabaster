using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alabaster.DialogueSystem.Utilities
{
    public static class CallBacks
    {
        public delegate void VoidCallBack();
        public delegate void CallBackWithGameObject(GameObject gameObject);

        public static void CallBackAtEndOfFrame(VoidCallBack callBack, MonoBehaviour callingObject)
        {
            IEnumerator callBackAtEndOfFrame = CoCallBackAtEndOfFrame(callBack);
            callingObject.StartCoroutine(callBackAtEndOfFrame);
        }

        public static IEnumerator CoCallBackAtEndOfFrame(VoidCallBack callBack)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke();
        }

        public static IEnumerator CoCallBackAtEndOfFrame(CallBackWithGameObject callBack, GameObject gameObject)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke(gameObject);
        }
    }

}

