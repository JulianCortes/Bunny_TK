using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Bunny_TK.DataDriven.UI
{
    [ExecuteInEditMode]
    public class UIStyle : MonoBehaviour
    {
        public UITemplate template;

        public Image image;
        public Text text;
        public Selectable selectable;

        // Use this for initialization
        protected virtual void OnEnable()
        {
            image = GetComponent<Image>();
            text = GetComponent<Text>();
            selectable = GetComponent<Selectable>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            ApplyStyle();
        }

        public virtual void ApplyStyle(UITemplate template)
        {
            this.template = template;

            if (template == null) return;

            if (image != null)
            {
                image.color = template.baseColor;
                image.sprite = template.sprite;
            }

            if (text != null)
            {
                text.color = template.fontColor;
                text.font = template.font;
            }

            if (selectable != null)
            {
                selectable.colors = template.selectableColors;
            }
        }

        public virtual void ApplyStyle()
        {
            ApplyStyle(template);
        }
    }
}
