using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Protom.WallT.Istruzione.Micro
{
    public class SimpleAdditionalUI : BaseAdditionalUI
    {
        public bool HasSelectable { get { return hasSelectable = selectable != null; } }
        public bool HasPanel { get { return hasPanel = panel != null; } }
        public Selectable VisibilityButton
        {
            get { return selectable; }
            set
            {
                //Maybe do something when it already has a button
                selectable = value;
            }
        }

        public virtual bool IsSelectableVisible
        {
            get
            {
                if (HasSelectable == false || IsVisible == false)
                    return isSelectableVisible = false;

                return selectable.gameObject.activeSelf;
            }
            set
            {
                if (HasSelectable == false) return;

                isSelectableVisible = value;
                selectable.gameObject.SetActive(value);
            }
        }
        public virtual bool IsPanelVisible
        {
            get
            {
                if (HasPanel == false)
                    return isPanelVisible = false;

                if (panelAnimatedUI != null)
                    return panelAnimatedUI.IsVisible;
                else if (customSetVisiblePanel.GetPersistentEventCount() > 0)
                    return isPanelVisible;
                else
                    return isPanelVisible = panel.gameObject.activeSelf;
            }
            set
            {
                if (HasPanel == false) return;

                if (panelAnimatedUI != null)
                    panelAnimatedUI.SetVisible(value);
                else if (customSetVisiblePanel.GetPersistentEventCount() > 0)
                    customSetVisiblePanel.Invoke(value);
                else
                    panel.gameObject.SetActive(value);

                if (HasSelectable && selectable.GetType() == typeof(Toggle))
                {
                    var tog = selectable as Toggle;
                    if (tog.isOn != value)
                        tog.isOn = value;
                }

                isPanelVisible = value;
                OnPanelChangedVisibility?.Invoke(this);
            }
        }
        public override bool IsVisible
        {
            get
            {
                return isVisible;
            }

            set
            {
                isVisible = value;
                if (isPanelVisibleOnVisible)
                    IsPanelVisible = value;
                if (isButtonVisibleOnVisible)
                    IsSelectableVisible = value;
                base.IsVisible = value;
            }
        }

        public event Action<SimpleAdditionalUI> OnPanelChangedVisibility;

        [Header("Graphic References")]
        [FormerlySerializedAs("button")]
        [SerializeField] protected Selectable selectable;
        [SerializeField] protected GameObject panel;
        [SerializeField] protected BaseAnimatedUI panelAnimatedUI;
        [SerializeField] protected UnityEventBool customSetVisiblePanel;

        [Header("Settings")]
        public bool autoAssignToggleToButton = false;
        public bool isVisibleAtStart = false;
        public bool isButtonVisibleAtStart = false;
        public bool isButtonVisibleOnVisible = true;
        public bool isPanelVisibleAtStart = false;
        public bool isPanelVisibleOnVisible = false;

        private bool hasSelectable;
        private bool hasPanel;

        [Header("Current Status (Clickable!!)")]
        [SerializeField, PropertySet("IsVisible")]
        private bool isVisible;
        [SerializeField, PropertySet("IsSelectableVisible")]
        private bool isSelectableVisible;
        [SerializeField, PropertySet("IsPanelVisible")]
        private bool isPanelVisible;

        protected virtual void OnEnable()
        {
            if (autoAssignToggleToButton)
            {
                if (HasSelectable)
                {
                    AddVisibilityControlButton();
                }
            }
        }
        protected virtual void Start()
        {
            IsVisible = isVisibleAtStart;
            IsSelectableVisible = isButtonVisibleAtStart;
            IsPanelVisible = isPanelVisibleAtStart;
        }
        protected virtual void OnDisable()
        {
            if (autoAssignToggleToButton)
            {
                if (HasSelectable)
                {
                    RemoveVisibilityControlButton();
                }
            }
        }

        /// <summary>
        /// Sets the internal status.
        /// </summary>
        /// <param name="isPanelVisible"></param>
        public virtual void UpdateVisiblePanel(bool isPanelVisible)
        {
            this.isPanelVisible = isPanelVisible;
        }
        public virtual void ToggleVisibilityPanel()
        {
            IsPanelVisible = !IsPanelVisible;
        }
        public virtual void SetVisibilityPanel(bool visibility)
        {
            IsPanelVisible = visibility;
        }
        public virtual void ToggleVisibilityButton()
        {
            IsSelectableVisible = !IsSelectableVisible;
        }

        public virtual void RemoveVisibilityControlButton()
        {
            if (!HasSelectable) return;

            if (selectable.GetType() == typeof(Button))
            {
                ((Button)selectable).onClick.RemoveListener(ToggleVisibilityPanel);
            }
            else if (selectable.GetType() == typeof(Toggle))
            {
                ((Toggle)selectable).onValueChanged.RemoveListener(SetVisibilityPanel);
            }

            autoAssignToggleToButton = false;
        }
        public virtual void AddVisibilityControlButton(Selectable selectable, bool assignToggleVisibility)
        {
            VisibilityButton = selectable;

            if (assignToggleVisibility)
            {
                if (selectable.GetType() == typeof(Button))
                {
                    ((Button)this.selectable).onClick.AddListener(this.ToggleVisibilityPanel);
                }
                else if (selectable.GetType() == typeof(Toggle))
                {
                    ((Toggle)this.selectable).onValueChanged.AddListener(this.SetVisibilityPanel);
                }
            }

        }
        public virtual void AddVisibilityControlButton()
        {
            AddVisibilityControlButton(selectable, true);
        }

        public virtual void SetCustomActionButton(Action custom)
        {
            if (selectable.GetType() == typeof(Button))
            {
                ((Button)selectable).onClick.AddListener(custom.Invoke);
            }
            else if (selectable.GetType() == typeof(Toggle))
            {
                ((Toggle)selectable).onValueChanged.AddListener(SetVisibilityPanel);
            }

        }

    }

    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }

}
