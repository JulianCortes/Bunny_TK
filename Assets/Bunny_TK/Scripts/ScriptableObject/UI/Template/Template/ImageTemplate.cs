using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven.UI.Template
{
    [CreateAssetMenu(fileName = "ImageTemplate", menuName = "Data Driven/Image Template")]
    public class ImageTemplate : GenericTemplate<Image>
    {
        public TemplateSettingSprite sourceImage = new TemplateSettingSprite();
        public TemplateSettingColor color = new TemplateSettingColor();
        public TemplateSettingMaterial material = new TemplateSettingMaterial();
        public TemplateSettingBool raycastTarget = new TemplateSettingBool();

        public TemplateSettingImageType type = new TemplateSettingImageType();
        public TemplateSettingFillMethod fillMethod = new TemplateSettingFillMethod();

        public override void ApplyTemplate(Image targetImage)
        {
            if (sourceImage.apply) targetImage.sprite = sourceImage.value;
            if (color.apply) targetImage.color = color.value;
            if (material.apply) targetImage.material = material.value;
            if (raycastTarget.apply) targetImage.raycastTarget = raycastTarget.value;

            if (type.apply) targetImage.type = type.value;
            if (fillMethod.apply) targetImage.fillMethod = fillMethod.value;
        }

        public void UpdateToAll()
        {
            foreach (var style in GameObject.FindObjectsOfType<ImageStyle>().Where(style => style.template == this))
                style.ApplyTemplate();
        }

        public override void CopyFrom<T>(T other)
        {
            ImageTemplate otherTemplate = null;
            if (other.GetType() == typeof(ImageTemplate))
                otherTemplate = other as ImageTemplate;

            Image otherSelectable = null;
            if (other.GetType() == typeof(Image))
                otherSelectable = other as Image;


            if (otherSelectable == null && otherTemplate == null) return;

            if (otherSelectable != null)
                CopyFrom(otherSelectable);

            if (otherTemplate != null)
                CopyFrom(otherTemplate);
        }

        public void CopyFrom(Image other)
        {
            if(sourceImage.apply) sourceImage.value = other.sprite;
            if (color.apply) color.value = other.color;
            if (material.apply) material.value = other.material;
            if (raycastTarget.apply) raycastTarget.value = other.raycastTarget;
            if (type.apply) type.value = other.type;
            if (fillMethod.apply) fillMethod.value = other.fillMethod;

            //sourceImage.apply = true;
            //color.apply = true;
            //material.apply = true;
            //raycastTarget.apply = true;
            //type.apply = true;
            //fillMethod.apply = true;
        }
        public void CopyFrom(ImageTemplate other)
        {
            sourceImage.value = other.sourceImage.value;
            color.value = other.color.value;
            material.value = other.material.value;
            raycastTarget.value = other.raycastTarget.value;
            type.value = other.type.value;
            fillMethod.value = other.fillMethod.value;

            sourceImage.apply = other.sourceImage.apply;
            color.apply = other.color.apply;
            material.apply = other.material.apply;
            raycastTarget.apply = other.raycastTarget.apply;
            type.apply = other.type.apply;
            fillMethod.apply = other.fillMethod.apply;
        }
    }
}
