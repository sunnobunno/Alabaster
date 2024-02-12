using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DialogueSystem
{
    interface IDialogueElementControllerWithContent : IDialogueElementController<string>
    {
        public string Content { get; set; }

        public UnityEngine.Color TextColor { get; set; }

        public void SetContent(string content);
    }
}
