using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bunny_TK.UI
{
    /// Used for UI that can be shown/hidden.
    /// Main Functionalities:
    ///     - panel visibility controls with events and UnityEvents.
    ///     - can add/removed a button or toggle that controls visibility, even at runtime.
    ///     - button or toggle visibility can be synced with panel's visibility.
    public class SimpleAdditionalUI : BaseAdditionalUI
    {
        [Header("References")]
        [SerializeField] protected Selectable selectable;
        [SerializeField] protected GameObject panel;
        [SerializeField] protected BaseAnimatedUI panelAnimatedUI;
        [SerializeField] public UnityEventBool onVisible;

        [Header("Settings")]
        [SerializeField] protected bool autoAssignVisibilityToggleToSelectable = false;
        [SerializeField] protected bool isVisibleAtStart = false;
        [SerializeField] protected bool isSelectableVisibleAtStart = false;
        [SerializeField, PropertySet("SelectableBehaviour")] protected Visibility selectableBehaviour;

        [Header("Current Status")]
        [SerializeField, PropertySet("IsVisible")]
        private bool isVisible;
        [SerializeField, PropertySet("IsSelectableVisible")]
        private bool isSelectableVisible;
        private bool hasSelectable;

        #region Properties
        public override bool IsVisible
        {
            get
            {
                if (panelAnimatedUI != null)
                    return isVisible = panelAnimatedUI.IsVisible;
                else if (onVisible.GetPersistentEventCount() > 0)
                    return isVisible;
                else
                    return isVisible;
            }

            set
            {
                isVisible = value;

                if (panelAnimatedUI != null)
                    panelAnimatedUI.SetVisible(value);
                else if (onVisible.GetPersistentEventCount() > 0)
                    onVisible.Invoke(value);
                else
                    panel.gameObject.SetActive(value);

                if (HasSelectable && selectable.GetType() == typeof(Toggle))
                {
                    var tog = selectable as Toggle;
                    if (tog.isOn != value)
                        tog.isOn = value;
                }

                UpdateSelectableVisibility(value);

                base.IsVisible = value;
            }
        }

        public Selectable VisibilitySelectable
        {
            get { return selectable; }
            private set
            {
                //Maybe do something when it already has a button
                selectable = value;
                if (value != null)
                    isSelectableVisible = value.gameObject.activeSelf;
                else
                    isSelectableVisible = false;
            }
        }
        public bool HasSelectable
        {
            get
            {
                return hasSelectable = selectable != null;
            }
        }
        public bool IsSelectableVisible
        {
            get
            {
                if (HasSelectable == false)
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

        public Visibility SelectableBehaviour
        {
            get
            {
                return selectableBehaviour;
            }

            set
            {
                selectableBehaviour = value;
                UpdateSelectableVisibility(IsVisible);
            }
        }
        #endregion Properties

        #region Unity CallBacks
        protected virtual void OnEnable()
        {
            if (autoAssignVisibilityToggleToSelectable && HasSelectable)
            {
                AddVisibilityControlSelectable();
            }
        }
        protected virtual void Start()
        {
            IsVisible = isVisibleAtStart;
            IsSelectableVisible = isSelectableVisibleAtStart;
            UpdateSelectableVisibility(IsVisible);
        }
        protected virtual void OnDisable()
        {
            if (autoAssignVisibilityToggleToSelectable && HasSelectable)
            {
                RemoveVisibilityControlSelectable();
            }
        }
        #endregion Unity CallBacks

        #region Visibility Controls
        public virtual void SetVisible(bool isVisible)
        {
            IsVisible = isVisible;
        }
        public virtual void ToggleVisibility()
        {
            IsVisible = !IsVisible;
        }
        public virtual void ToggleVisibilityButton()
        {
            IsSelectableVisible = !IsSelectableVisible;
        }

        public virtual void SetVisibleAll(bool isVisible)
        {
            IsVisible = isVisible;
            IsSelectableVisible = isVisible;
        }
        public virtual void ToggleVisibleAll()
        {
            bool val = !IsVisible;
            IsSelectableVisible = val;
            IsVisible = val;
        }

        private void UpdateSelectableVisibility(bool mainVisibility)
        {
            switch (selectableBehaviour)
            {
                case Visibility.Indipendent:
                    return;
                case Visibility.SyncWithHidden:
                    if (mainVisibility == false)
                        IsSelectableVisible = mainVisibility;
                    return;
                case Visibility.SyncWithVisible:
                    if (mainVisibility == true)
                        IsSelectableVisible = mainVisibility;
                    return;
                case Visibility.SyncAll:
                    IsSelectableVisible = mainVisibility;
                    return;
                case Visibility.Inverted:
                    IsSelectableVisible = !mainVisibility;
                    return;
                default:
                    return;
            }
        }
        #endregion Visibility Controls

        #region Custom Visibility Controls
        public virtual void AddVisibilityControlSelectable(GameObject selectable, bool assignToggleVisibility)
        {
            Button button = selectable.GetComponent<Button>();
            if (button != null)
            {
                AddVisibilityControlSelectable(button, assignToggleVisibility);
                return;
            }

            Toggle toggle = selectable.GetComponent<Toggle>();
            if (toggle != null)
            {
                AddVisibilityControlSelectable(toggle, assignToggleVisibility);
                return;
            }
        }
        public virtual void AddVisibilityControlSelectable(Selectable selectable, bool assignToggleVisibility)
        {
            VisibilitySelectable = selectable;

            if (assignToggleVisibility && selectable != null)
            {
                if (selectable.GetType() == typeof(Button))
                {
                    Button button = this.selectable as Button;
                    button.onClick.RemoveListener(ToggleVisibility);
                    button.onClick.AddListener(ToggleVisibility);
                }
                else if (selectable.GetType() == typeof(Toggle))
                {
                    Toggle toggle = this.selectable as Toggle;
                    toggle.onValueChanged.RemoveListener(SetVisible);
                    toggle.onValueChanged.AddListener(SetVisible);
                }
            }
        }
        public virtual void AddVisibilityControlSelectable()
        {
            AddVisibilityControlSelectable(selectable, true);
        }

        public virtual void AddCustomActionOnSelectablClick(Action custom)
        {
            if (selectable.GetType() == typeof(Button))
                AddCustomActionOnButtonClick(custom);
            else if (selectable.GetType() == typeof(Toggle))
            {
                AddCustomActionOnToggleClick((bool temp) =>
                {
                    custom.Invoke();
                });
            }
        }
        public virtual void AddCustomActionOnButtonClick(Action custom)
        {
            if (HasSelectable == false) return;
            if (selectable.GetType() == typeof(Button))
            {
                ((Button)selectable).onClick.AddListener(custom.Invoke);
            }
        }
        public virtual void AddCustomActionOnToggleClick(Action<bool> custom)
        {
            if (HasSelectable == false) return;
            if (selectable.GetType() == typeof(Toggle))
            {
                ((Toggle)selectable).onValueChanged.AddListener(custom.Invoke);
            }
        }

        public virtual void RemoveVisibilityControlSelectable()
        {
            if (!HasSelectable) return;

            if (selectable.GetType() == typeof(Button))
            {
                ((Button)selectable).onClick.RemoveListener(ToggleVisibility);
            }
            else if (selectable.GetType() == typeof(Toggle))
            {
                ((Toggle)selectable).onValueChanged.RemoveListener(SetVisible);
            }

            autoAssignVisibilityToggleToSelectable = false;
        }
        #endregion Custom Visibility Controls
    }

    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }

    public enum Visibility
    {
        Indipendent = 0,
        /// <summary>
        /// Hide when main is hidden, but do NOT show when main is visible.
        /// </summary>
        SyncWithHidden = 1,
        /// <summary>
        /// Show when main is visible, but do NOT hide when main is hidden.
        /// </summary>
        SyncWithVisible = 2,
        SyncAll = 3,
        Inverted = 4
    }
}