using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Assets.DialogueSystem
{
    public interface IDialogueElementClickable : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        static event Action SendClickedSignal;

        new void OnPointerEnter(PointerEventData eventData);

        new void OnPointerExit(PointerEventData eventData);

        new public void OnPointerClick(PointerEventData eventData);
    }
}
