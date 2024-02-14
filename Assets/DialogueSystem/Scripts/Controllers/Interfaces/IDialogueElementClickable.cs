using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Alabaster.DialogueSystem
{
    public interface IDialogueElementClickable<T> : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        static event Action<T> SendClickedSignal;

        new void OnPointerEnter(PointerEventData eventData);

        new void OnPointerExit(PointerEventData eventData);

        new public void OnPointerClick(PointerEventData eventData);
    }
}
