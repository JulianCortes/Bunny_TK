using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven.UI
{
    [CreateAssetMenu(fileName = "UITemplate", menuName = "Data Driven/UI Template")]
    public class UITemplate : ScriptableObject
    {
        [Header("Image")]
        public Color baseColor;
        public Sprite sprite;

        [Header("Font")]
        public Font font;
        public Color fontColor;

        [Header("Selectable")]
        public ColorBlock selectableColors;
    }
}
