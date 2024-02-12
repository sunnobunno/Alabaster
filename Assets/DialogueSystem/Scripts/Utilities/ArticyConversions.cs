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

        public static string BranchToText(Branch branch)
        {
            var branchTarget = branch.Target;
            var text = IFlowObjectToText((ArticyObject)branchTarget);

            return text;
        }

        public static IFlowObject ArticyRefToIFlowObject(ArticyRef aRef)
        {
            var aObject = aRef.GetObject();

            return aObject;
        }


    }
}
