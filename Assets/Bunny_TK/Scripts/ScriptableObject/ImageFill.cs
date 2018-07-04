using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.DataDriven
{
    public class ImageFill : MonoBehaviour
    {
        public FloatVariable currentValue;
        public FloatVariable maxValue;
        public Image targetImage;


        // Update is called once per frame
        void Update()
        {
            if (currentValue == null) return;
            if (maxValue == null) return;
            if (targetImage == null) return;

            targetImage.fillAmount = currentValue.runtimeValue / maxValue.runtimeValue;

        }

    }
}
