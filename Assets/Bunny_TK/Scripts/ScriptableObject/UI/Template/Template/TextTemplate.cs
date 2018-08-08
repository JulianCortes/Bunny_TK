using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven.UI.Template
{

    [CreateAssetMenu(fileName = "TextTemplate", menuName = "Data Driven/Text Template")]
    public class TextTemplate : GenericTemplate<Text>
    {
        public TemplateSettingFont font = new TemplateSettingFont();
        public TemplateSettingFontStyle fontStyle = new TemplateSettingFontStyle();
        public TemplateSettingInt fontSize = new TemplateSettingInt();
        public TemplateSettingFloat lineSpacing = new TemplateSettingFloat();
        public TemplateSettingBool richText = new TemplateSettingBool();

        public TemplateSettingTextAlignment alignment = new TemplateSettingTextAlignment();
        public TemplateSettingBool alignByGeometry = new TemplateSettingBool();
        public TemplateSettingHorizontalWrapMode horizontalOverflow = new TemplateSettingHorizontalWrapMode();
        public TemplateSettingVerticalWrapMode verticalOverflow = new TemplateSettingVerticalWrapMode();
        public TemplateSettingBool bestFit = new TemplateSettingBool();
        public TemplateSettingInt fontMinSize = new TemplateSettingInt();
        public TemplateSettingInt fontMaxSize = new TemplateSettingInt();

        public TemplateSettingColor color = new TemplateSettingColor();
        public TemplateSettingMaterial material = new TemplateSettingMaterial();
        public TemplateSettingBool raycastTarget = new TemplateSettingBool();

        public override void ApplyTemplate(Text targetText)
        {
            if (font.apply) targetText.font = font.value;
            if (fontStyle.apply) targetText.fontStyle = fontStyle.value;
            if (fontSize.apply) targetText.fontSize = fontSize.value;
            if (lineSpacing.apply) targetText.lineSpacing = lineSpacing.value;
            if (richText.apply) targetText.supportRichText = richText.value;

            if (alignment.apply) targetText.alignment = alignment.value;
            if (alignByGeometry.apply) targetText.alignByGeometry = alignByGeometry.value;

            if (horizontalOverflow.apply) targetText.horizontalOverflow = horizontalOverflow.value;
            if (verticalOverflow.apply) targetText.verticalOverflow = verticalOverflow.value;

            if (bestFit.apply) targetText.resizeTextForBestFit = bestFit.value;
            if (fontMinSize.apply) targetText.resizeTextMinSize = fontMinSize.value;
            if (fontMaxSize.apply) targetText.resizeTextMaxSize = fontMaxSize.value;

            if (color.apply) targetText.color = color.value;
            if (material.apply) targetText.material = material.value;
            if (raycastTarget.apply) targetText.raycastTarget = raycastTarget.value;
        }

        public override void CopyFrom<T>(T other)
        {
            TextTemplate otherTemplate = null;
            if (other.GetType() == typeof(TextTemplate))
                otherTemplate = other as TextTemplate;

            Text otherText = null;
            if (other.GetType() == typeof(Text))
                otherText = other as Text;

            if (otherText == null && otherTemplate == null) return;

            if (otherText != null)
                CopyFrom(otherText);

            if (otherTemplate != null)
                CopyFrom(otherTemplate);
        }
        public void CopyFrom(Text other)
        {
            if(font.apply) font.value = other.font;
            if (fontStyle.apply) fontStyle.value = other.fontStyle;
            if (fontSize.apply) fontSize.value = other.fontSize;
            if (lineSpacing.apply) lineSpacing.value = other.lineSpacing;
            if (richText.apply) richText.value = other.supportRichText;

            if (alignment.apply) alignment.value = other.alignment;
            if (alignByGeometry.apply) alignByGeometry.value = other.alignByGeometry;
            if (horizontalOverflow.apply) horizontalOverflow.value = other.horizontalOverflow;
            if (verticalOverflow.apply) verticalOverflow.value = other.verticalOverflow;
            if (bestFit.apply) bestFit.value = other.resizeTextForBestFit;
            if (fontMinSize.apply) fontMinSize.value = other.resizeTextMinSize;
            if (fontMaxSize.apply) fontMaxSize.value = other.resizeTextMaxSize;

            if (color.apply) color.value = other.color;
            if (material.apply) material.value = other.material;
            if (raycastTarget.apply) raycastTarget.value = other.raycastTarget;

            //font.apply = true;
            //fontStyle.apply = true;
            //fontSize.apply = true;
            //lineSpacing.apply = true;
            //richText.apply = true;

            //alignment.apply = true;
            //alignByGeometry.apply = true;
            //horizontalOverflow.apply = true;
            //verticalOverflow.apply = true;
            //bestFit.apply = true;
            //fontMinSize.apply = true;
            //fontMaxSize.apply = true;

            //color.apply = true;
            //material.apply = true;
            //raycastTarget.apply = true;
        }
        public void CopyFrom(TextTemplate other)
        {
            font.value = other.font.value;
            fontStyle.value = other.fontStyle.value;
            fontSize.value = other.fontSize.value;
            lineSpacing.value = other.lineSpacing.value;
            richText.value = other.richText.value;

            alignment.value = other.alignment.value;
            alignByGeometry.value = other.alignByGeometry.value;
            horizontalOverflow.value = other.horizontalOverflow.value;
            verticalOverflow.value = other.verticalOverflow.value;
            bestFit.value = other.bestFit.value;
            fontMinSize.value = other.fontMinSize.value;
            fontMaxSize.value = other.fontMaxSize.value;

            color.value = other.color.value;
            material.value = other.material.value;
            raycastTarget.value = other.raycastTarget.value;

            font.apply = other.font.apply;
            fontStyle.apply = other.fontStyle.apply;
            fontSize.apply = other.fontSize.apply;
            lineSpacing.apply = other.lineSpacing.apply;
            richText.apply = other.richText.apply;

            alignment.apply = other.alignment.apply;
            alignByGeometry.apply = other.alignByGeometry.apply;
            horizontalOverflow.apply = other.horizontalOverflow.apply;
            verticalOverflow.apply = other.verticalOverflow.apply;
            bestFit.apply = other.bestFit.apply;
            fontMinSize.apply = other.fontMinSize.apply;
            fontMaxSize.apply = other.fontMaxSize.apply;

            color.apply = other.color.apply;
            material.apply = other.material.apply;
            raycastTarget.apply = other.raycastTarget.apply;
        }
    }
}