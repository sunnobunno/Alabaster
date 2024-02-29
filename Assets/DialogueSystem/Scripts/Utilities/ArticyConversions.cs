using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.Little_Guy_Syndrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.DialogueSystem;
using Articy.Little_Guy_Syndrome.GlobalVariables;

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

        public static Dialogue_Choice_Properties GetDialogueChoiceProperties(IFlowObject aObject)
        {
            var articyObject = (ArticyObject)aObject;
            var aObjectRef = (ArticyRef)articyObject;
            var aObjectProperties = aObjectRef.GetObject<Dialogue_Choice_Properties>();

            return aObjectProperties;
        }

        public static bool GetIsSkillCheck(IFlowObject aObject)
        {
            var aObjectProperties = GetDialogueChoiceProperties(aObject);

            var isSkillCheck = aObjectProperties.Template.SkillCheckFeature.IsSkillCheck;

            return isSkillCheck;
        }

        public static SkillEnum GetSkillEnum(IFlowObject aObject)
        {
            var aObjectProperties = GetDialogueChoiceProperties(aObject);

            var skillEnum = aObjectProperties.Template.SkillCheckFeature.SkillEnum;

            return skillEnum;
        }

        public static int GetSkillCheckRequirement(IFlowObject aObject)
        {
            var aObjectProperties = GetDialogueChoiceProperties(aObject);

            var skillCheckRequirement = aObjectProperties.Template.SkillCheckFeature.SkillCheckRequirement;

            return (int)skillCheckRequirement;
        }

        public static int GetSkillLevel(IFlowObject aObject)
        {
            var skillEnum = GetSkillEnum(aObject);

            var skillLevel = ArticySkillMapping.NameToGlobalVariable[skillEnum];

            return skillLevel;
        }
    }
}
