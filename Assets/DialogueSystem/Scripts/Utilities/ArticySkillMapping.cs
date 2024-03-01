using Articy.Little_Guy_Syndrome;
using Articy.Little_Guy_Syndrome.GlobalVariables;
using Articy.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DialogueSystem
{
    public static class ArticySkillMapping
    {
        public static Dictionary<SkillEnum, int> NameToGlobalVariable { get; private set; } = new Dictionary<SkillEnum, int>()
        {
            {SkillEnum.VOID, ArticyGlobalVariables.Default.SkillLevels.SkillLevelVoid},
            {SkillEnum.DETERMINATION, ArticyGlobalVariables.Default.SkillLevels.SkillLevelDetermination }
        };
    }
}
