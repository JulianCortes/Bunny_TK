using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.DataDriven.UI.Template
{
    [CreateAssetMenu(fileName = "RectTemplate", menuName = "Data Driven/Rect Template")]
    public class RectTemplate : GenericTemplate<RectTransform>
    {
        public TemplateSettingVector2 anchorMin = new TemplateSettingVector2();
        public TemplateSettingVector2 anchorMax = new TemplateSettingVector2();
        public TemplateSettingVector2 anchoredPosition = new TemplateSettingVector2();
        public TemplateSettingVector2 sizeDelta = new TemplateSettingVector2();
        public TemplateSettingVector2 offsetMax = new TemplateSettingVector2();
        public TemplateSettingVector2 offsetMin = new TemplateSettingVector2();
        public TemplateSettingVector2 pivot = new TemplateSettingVector2();

        public override void ApplyTemplate(RectTransform targetComponent)
        {
            if (anchorMin.apply) targetComponent.anchorMin = anchorMin.value;
            if (anchorMax.apply) targetComponent.anchorMax = anchorMax.value;
            if (sizeDelta.apply) targetComponent.sizeDelta = sizeDelta.value;
            if (offsetMax.apply) targetComponent.offsetMax = offsetMax.value;
            if (offsetMin.apply) targetComponent.offsetMin = offsetMin.value;
            if (pivot.apply) targetComponent.pivot = pivot.value;
            if (anchoredPosition.apply) targetComponent.anchoredPosition = anchoredPosition.value;
        }

        public override void CopyFrom<T>(T other)
        {
            RectTemplate otherTemplate = null;
            if (other.GetType() == typeof(RectTemplate))
                otherTemplate = other as RectTemplate;

            RectTransform otherRect = null;
            if (other.GetType() == typeof(RectTransform))
                otherRect = other as RectTransform;


            if (otherRect == null && otherTemplate == null) return;

            if (otherRect != null)
                CopyFrom(otherRect);

            if (otherTemplate != null)
                CopyFrom(otherTemplate);
        }

        public void CopyFrom(RectTemplate template)
        {
            anchorMin.value = template.anchorMin.value;
            anchorMax.value = template.anchorMax.value;
            sizeDelta.value = template.sizeDelta.value;
            offsetMax.value = template.offsetMax.value;
            offsetMin.value = template.offsetMin.value;
            pivot.value = template.pivot.value;
            anchoredPosition.value = template.anchoredPosition.value;

            anchorMin.apply = template.anchorMin.apply;
            anchorMax.apply = template.anchorMax.apply;
            sizeDelta.apply = template.sizeDelta.apply;
            offsetMax.apply = template.offsetMax.apply;
            offsetMin.apply = template.offsetMin.apply;
            pivot.apply = template.pivot.apply;
            anchoredPosition.apply = template.anchoredPosition.apply;
        }

        public void CopyFrom(RectTransform rect)
        {
            if (anchorMin.apply) anchorMin.value = rect.anchorMin;
            if (anchorMax.apply) anchorMax.value = rect.anchorMax;
            if (sizeDelta.apply) sizeDelta.value = rect.sizeDelta;
            if (offsetMax.apply) offsetMax.value = rect.offsetMax;
            if (offsetMin.apply) offsetMin.value = rect.offsetMin;
            if (pivot.apply) pivot.value = rect.pivot;
            if (anchoredPosition.apply) anchoredPosition.value = rect.anchoredPosition;

        }


    }
}
