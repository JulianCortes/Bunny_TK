using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.DataDriven.UI.Template
{
    [ExecuteInEditMode]
    public class GenericStyle<TTargetComp, TTemplate> : BaseStyle
                                      where TTemplate : GenericTemplate<TTargetComp>
    {
        public bool onlyInEditor = false;
        public TTemplate template;
        protected TTargetComp targetComponent;

        public override BaseTemplate BaseTemplate
        {
            get
            {
                return template as TTemplate;
            }

            set
            {
                template = value as TTemplate;
            }
        }

        protected virtual void OnValidate()
        {
            targetComponent = GetComponent<TTargetComp>();
        }

        protected virtual void Start()
        {
            targetComponent = GetComponent<TTargetComp>();
            if (onlyInEditor == false)
                ApplyTemplate();
        }

        //Apply current template
        public override void ApplyTemplate()
        {
            if (template == null) return;
            if (targetComponent == null) return;

            template.ApplyTemplate(targetComponent);
        }

        //Create a new Template typeof(TTemplate)
        public override BaseTemplate CreateNewTemplate()
        {
            var template = ScriptableObject.CreateInstance<TTemplate>();
            template.name = $"New{template.GetType().Name}";
            return template;
        }

        public override void CopySettingsToTemplate(BaseTemplate target)
        {
            target.CopyFrom(targetComponent);
        }

    }

    public abstract class BaseStyle : MonoBehaviour
    {
        public abstract BaseTemplate BaseTemplate { get; set; }
        public bool continuosRefresh = false;

        public abstract BaseTemplate CreateNewTemplate();

        public abstract void ApplyTemplate();
        public abstract void CopySettingsToTemplate(BaseTemplate target);

        public void UpdateTemplate()
        {
            if (BaseTemplate != null)
                CopySettingsToTemplate(BaseTemplate);
        }

        protected virtual void Update()
        {
            if (continuosRefresh)
                ApplyTemplate();
        }
    }
}
