using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven.UI.Template
{
    [System.Serializable]
    public class TemplateSetting<T> : BaseTemplateSetting
    {
        /// Too bad most components uses properties and not methods for setting (and getting) values.
        /// So I can't use either ref nor Action/Delegate in ApplySetting().
        /// 
        /// For now when trying to set the value I need to check "ignore" before assigning value (w/o using ApplySetting methods..).
        /// 
        /// There are ways to create an Action from a property field:
        ///     - Reflection        (worst)
        ///     - Expression Tree   (faster than Reflection)
        ///     - Static Action     (faster than all of the above)
        ///     
        /// But IMHO it's not worth it.


        /// <summary>
        /// If TRUE, this setting will be applied.
        /// </summary>
        public bool apply = true;
        public T value;

        public void ApplySetting(ref T value)
        {
            if (apply)
                value = this.value;
        }

        public void ApplySetting(Action<T> setMethod)
        {
            if (apply)
                setMethod.Invoke(value);
        }
    }

    public abstract class BaseTemplateSetting
    {
    }

    //FONT Settings
    [System.Serializable]
    public class TemplateSettingFont : TemplateSetting<Font> { }
    [System.Serializable]
    public class TemplateSettingFontStyle : TemplateSetting<FontStyle> { }
    [System.Serializable]
    public class TemplateSettingTextAlignment : TemplateSetting<TextAnchor> { }
    [System.Serializable]
    public class TemplateSettingHorizontalWrapMode : TemplateSetting<HorizontalWrapMode> { }
    [System.Serializable]
    public class TemplateSettingVerticalWrapMode : TemplateSetting<VerticalWrapMode> { }

    //GENERAL
    [System.Serializable]
    public class TemplateSettingFloat : TemplateSetting<float> { }
    [System.Serializable]
    public class TemplateSettingInt : TemplateSetting<int> { }
    [System.Serializable]
    public class TemplateSettingBool : TemplateSetting<bool> { }
    [System.Serializable]
    public class TemplateSettingColor : TemplateSetting<Color> { }
    [System.Serializable]
    public class TemplateSettingMaterial : TemplateSetting<Material> { }
    [System.Serializable]
    public class TemplateSettingSprite : TemplateSetting<Sprite> { }
    [System.Serializable]
    public class TemplateSettingVector2 : TemplateSetting<Vector2> { }


    //IMAGE Settings
    [System.Serializable]
    public class TemplateSettingImageType : TemplateSetting<Image.Type> { }
    [System.Serializable]
    public class TemplateSettingFillMethod : TemplateSetting<Image.FillMethod> { }

    //Selectable Settings
    [System.Serializable]
    public class TemplateSettingColorBlock : TemplateSetting<ColorBlock> { }
    [System.Serializable]
    public class TemplateSettingSelectableTransition : TemplateSetting<Selectable.Transition> { }
    [System.Serializable]
    public class TemplateSettingSpriteState : TemplateSetting<SpriteState> { }

}