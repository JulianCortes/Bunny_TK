using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven.UI.Template
{
    [CreateAssetMenu(fileName = "SelectableTemplate", menuName = "Data Driven/Button Selectable")]
    public class SelectableTemplate : GenericTemplate<Selectable>
    {
        public TemplateSettingSelectableTransition transition = new TemplateSettingSelectableTransition();
        public TemplateSettingColorBlock colorBlock = new TemplateSettingColorBlock();
        public TemplateSettingSpriteState spriteState = new TemplateSettingSpriteState();

        public override void ApplyTemplate(Selectable targetSelectable)
        {
            if (transition.apply) targetSelectable.transition = transition.value;
            if (colorBlock.apply) targetSelectable.colors = colorBlock.value;
            if (spriteState.apply) targetSelectable.spriteState = spriteState.value;
        }

        public override void CopyFrom<T>(T other)
        {
            SelectableTemplate otherTemplate = null;
            if (other.GetType() == typeof(SelectableTemplate))
                otherTemplate = other as SelectableTemplate;

            Selectable otherSelectable = null;
            if (other.GetType() == typeof(Selectable))
                otherSelectable = other as Selectable;


            if (otherSelectable == null && otherTemplate == null) return;

            if (otherSelectable != null)
                CopyFrom(otherSelectable);

            if (otherTemplate != null)
                CopyFrom(otherTemplate);
        }

        public void CopyFrom(Selectable other)
        {
            if (transition.apply) transition.value = other.transition;
            if (colorBlock.apply) colorBlock.value = other.colors;
            if (spriteState.apply) spriteState.value = other.spriteState;

            //transition.apply = true;
            //colorBlock.apply = true;
            //spriteState.apply = true;
        }

        public void CopyFrom(SelectableTemplate other)
        {
            transition.value = other.transition.value;
            colorBlock.value = other.colorBlock.value;
            spriteState.value = other.spriteState.value;

            transition.apply = other.transition.apply;
            colorBlock.apply = other.colorBlock.apply;
            spriteState.apply = other.spriteState.apply;
        }
    }

}
