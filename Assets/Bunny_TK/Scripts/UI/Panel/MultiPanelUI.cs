using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Protom.WallT.Istruzione.Micro
{
    //TODO
    //  - Remove additionalUI method

    public class MultiPanelUI : SimpleAdditionalUI
    {
        [Header("MultiPanelUI")]
        [SerializeField]
        protected Transform tabsPanel;
        [SerializeField]
        protected Transform additionalUIPanel;

        [SerializeField]
        protected List<SimpleAdditionalUI> additionalUIs = new List<SimpleAdditionalUI>();

        [SerializeField]
        protected GameObject prefabTabButton;

        [Header("MultiPanelUI: Settings")]
        [SerializeField] protected bool moveAdditionalUIToAdditionalPanel = true;
        [SerializeField] protected bool moveAdditionalUIButtonToTabsPanel = true;
        [SerializeField] protected bool autoCreateTabButton = true;
        [SerializeField] protected bool additionalUIAreExclusive = true;

        [Header("MultiPanelUI: Initialization")]
        [SerializeField] protected bool autoFindAdditionalUIs = true;
        [SerializeField] protected bool autoInitializeCurrentAdditionalUIs = true;
        [SerializeField] protected bool autoInitializeUseButtonAsTab = true;

        protected override void Start()
        {
            base.Start();

            if (autoFindAdditionalUIs)
            {
                var uis = additionalUIPanel.GetComponentsInChildren<SimpleAdditionalUI>();
                additionalUIs = uis.ToList();
            }

            //AutoInitialize ui's in list.
            if (autoInitializeCurrentAdditionalUIs)
            {
                List<SimpleAdditionalUI> copy = new List<SimpleAdditionalUI>(additionalUIs);
                additionalUIs.Clear();

                for (int i = 0; i < copy.Count; i++)
                    AddUIPanel(copy[i], i, true);
            }

        }

        public void AddUIPanel(SimpleAdditionalUI additionalUI, int index, bool useButtonAsTab)
        {
            //TODO
            //  - Check for null
            //  - Check for OutOfRange
            //  - Don't duplicate handler call

            bool alreadyInList = additionalUIs.Contains(additionalUI);

            if (moveAdditionalUIToAdditionalPanel)
            {
                additionalUI.transform.SetParent(additionalUIPanel);
                additionalUI.transform.SetSiblingIndex(index);
            }

            if (alreadyInList == false)
                additionalUIs.Insert(index, additionalUI);

            bool createTabButton = autoCreateTabButton;
            if (additionalUI != null)
            {
                if (additionalUI.HasSelectable && useButtonAsTab)
                {
                    additionalUI.VisibilityButton.transform.SetParent(tabsPanel);
                    additionalUI.VisibilityButton.transform.SetSiblingIndex(index);
                    createTabButton = false;
                }
            }

            if (createTabButton)
            {
                Button tabButton = CreateTabButton(prefabTabButton, index);
                //Se il bottone viene creato e i pannelli NON sono esclusivi
                //la funzione associata al click sarà quello default di SimpleAdditionalUI (TogglePanelVisibility)
                additionalUI.AddVisibilityControlButton(tabButton, additionalUIAreExclusive == false);
            }

            additionalUI.OnPanelChangedVisibility += AdditionalUI_OnChangedPanelVisibility;
            additionalUI.OnChangedVisibility += AdditionalUI_OnChangedVisibility;

            if (additionalUIAreExclusive)
            {
                additionalUI.RemoveVisibilityControlButton();
                additionalUI.SetCustomActionButton(() =>
               {
                   additionalUI.IsPanelVisible = true;
               });

            }
        }

        private void AdditionalUI_OnChangedVisibility(BaseAdditionalUI addUI)
        {
            if (addUI.IsVisible == false)
            {
                if (Application.isPlaying)
                    if (additionalUIs.Exists(ui => ui.IsVisible && ui.IsPanelVisible) == false)
                        additionalUIs.Find(ui => ui.IsVisible).IsPanelVisible = true;
                return;
            }
        }

        protected virtual Button CreateTabButton(GameObject prefab, int index)
        {
            var gameO = Instantiate(prefab);

            if (moveAdditionalUIButtonToTabsPanel)
            {
                gameO.transform.SetParent(tabsPanel);
                gameO.transform.SetSiblingIndex(index);
            }

            Button button = gameO.GetComponent<Button>();
            return button;
        }
        protected virtual void AdditionalUI_OnChangedPanelVisibility(SimpleAdditionalUI addUI)
        {
            if (additionalUIAreExclusive == false) return;
            if (addUI.IsPanelVisible == false)
            {
                if (additionalUIs.Exists(ui => ui.IsVisible && ui.IsPanelVisible) == false)
                {
                    addUI.IsPanelVisible = true;
                }
                return;
            }

            //Hide other additionalUIs
            foreach (var sAddUI in additionalUIs)
                if (sAddUI != addUI)
                    sAddUI.IsPanelVisible = false;
        }
    }
}
