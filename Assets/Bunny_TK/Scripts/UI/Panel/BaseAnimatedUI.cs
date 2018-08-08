using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.UI
{
    public abstract class BaseAnimatedUI : MonoBehaviour
    {
        [SerializeField]
        protected Animator animator;
        public virtual bool IsInTransition { get; set; }
        public virtual bool IsVisible { get; set; }

        public virtual void SetVisible(bool isVisible)
        {
            IsVisible = isVisible;
        }
        public virtual void ToggleVisible()
        {
            SetVisible(!IsVisible);
        }
    }
}
