using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protom.WallT.Istruzione.Micro
{
    public abstract class BaseAdditionalUI : MonoBehaviour
    {
        private bool _isVisible;
        public virtual bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnChangedVisibility?.Invoke(this);
            }
        }

        [MethodButton]
        public virtual void ToggleVisible()
        {
            IsVisible = !IsVisible;
        }

        public event Action<BaseAdditionalUI> OnChangedVisibility;
    }
}
