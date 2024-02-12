using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.Little_Guy_Syndrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alabaster.DialogueSystem.Utilities
{
    public static class ArticyConversions
    {
        public static string IFlowObjectToText(IFlowObject aObject)
        {
            var text = ((IObjectWithText)aObject).Text;

            return text;
        }

        public static string IFlowObjectToTitle(IFlowObject aObject)
        {
            var dialogueSpeaker = aObject as IObjectWithSpeaker;
            var dialogueEntity = dialogueSpeaker.Speaker as Entity;
            var title = dialogueEntity.DisplayName;

            return title;
        }
    }
}
