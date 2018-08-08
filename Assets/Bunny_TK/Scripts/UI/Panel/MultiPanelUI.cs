using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Bunny_TK.UI
{
    public class MultiPanelUI : SimpleAdditionalUI
    {
        [Header("MultiPanelUI")]
        [SerializeField] protected Transform tabsPanel;
        [SerializeField] protected Transform additionalUIPanel;
        [SerializeField] protected List<SimpleAdditionalUI> additionalUIs = new List<SimpleAdditionalUI>();

        [Header("MultiPanelUI: Settings")]
        [SerializeField] protected bool additionalUIAreExclusive = true;
        [SerializeField] protected bool alwaysShowSomething = true;

        [Header("MultiPanelUI: Initialization")]
        [SerializeField] protected bool moveAdditionalUIToAdditionalPanel = true;
        [SerializeField] protected bool moveAdditionalUIButtonToTabsPanel = true;
        [SerializeField] protected bool autoCreateTabButton = true;
        [SerializeField] protected bool autoFindAdditionalUIs = true;
        [SerializeField] protected bool autoInitializeCurrentAdditionalUIs = true;
        [SerializeField] protected bool autoInitializeUseButtonAsTab = true;
        [SerializeField] protected Selectable prefabTabButton;

        protected List<SimpleAdditionalUI> currentVisibleAdditionalUIs = new List<SimpleAdditionalUI>();

        #region Unity Callbacks
        protected override void OnEnable()
        {
            base.OnEnable();
            OnChangedVisibility += MultiPanelUI_OnChangedVisibility;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            OnChangedVisibility -= MultiPanelUI_OnChangedVisibility;

        }
        protected override void Start()
        {
            base.Start();

            if (autoFindAdditionalUIs)
            {
                var uis = additionalUIPanel.GetComponentsInChildren<SimpleAdditionalUI>();
                additionalUIs.AddRange(uis.ToList());
                additionalUIs.RemoveAll(ui => ui == null);
            }

            //AutoInitialize ui's in list.
            if (autoInitializeCurrentAdditionalUIs)
            {
                List<SimpleAdditionalUI> copy = new List<SimpleAdditionalUI>(additionalUIs);
                copy.RemoveAll(ui => ui == null);
                additionalUIs.Clear();

                for (int i = 0; i < copy.Count; i++)
                    AddUI(copy[i], i, true);
            }
            VerifyIfShowSomething();
        }
        #endregion Unity Callbacks

        #region AddRemove
        public void AddUI(SimpleAdditionalUI additionalUI, int index, bool useButtonAsTab)
        {
            if (additionalUI == null) return;

            bool alreadyInList = additionalUIs.Contains(additionalUI);
            if (moveAdditionalUIToAdditionalPanel)
            {
                additionalUI.transform.SetParent(additionalUIPanel);
                additionalUI.transform.SetSiblingIndex(index);
            }

            bool createTabButton = autoCreateTabButton;
            if (additionalUI != null)
            {
                if (additionalUI.HasSelectable && useButtonAsTab)
                {
                    additionalUI.VisibilitySelectable.transform.SetParent(tabsPanel);
                    additionalUI.VisibilitySelectable.transform.SetSiblingIndex(index);
                    createTabButton = false;
                }
            }

            if (createTabButton)
            {
                GameObject tabButton = CreateTabButton(prefabTabButton, index);
                additionalUI.AddVisibilityControlSelectable(tabButton, true);
            }
            else
            {
                additionalUI.RemoveVisibilityControlSelectable();
                additionalUI.AddVisibilityControlSelectable();
            }

            if (alreadyInList == false)
            {
                if (index < additionalUIs.Count)
                {
                    if (index < 0) index = 0;
                    additionalUIs.Insert(index, additionalUI);
                }
                else
                {
                    index = additionalUIs.Count;
                    additionalUIs.Add(additionalUI);
                }
                additionalUI.OnChangedVisibility += AdditionalUI_OnChangedVisibility;
            }

            VerifyIfShowSomething();
        }
        public void AddUI(SimpleAdditionalUI additionalUI)
        {
            AddUI(additionalUI, additionalUIs.Count, true);
        }
        public bool RemoveUI(SimpleAdditionalUI additionalUI, bool hide = true)
        {
            if (additionalUIs.Contains(additionalUI) == false) return false;

            additionalUIs.Remove(additionalUI);
            currentVisibleAdditionalUIs.Remove(additionalUI);

            additionalUI.OnChangedVisibility -= AdditionalUI_OnChangedVisibility;
            if (hide)
                additionalUI.SetVisibleAll(false);

            VerifyIfShowSomething();
            return true;
        }
        #endregion AddRemove

        #region Visibility Handler
        private void AdditionalUI_OnChangedVisibility(BaseAdditionalUI addUI)
        {
            SimpleAdditionalUI simpleUI = addUI as SimpleAdditionalUI;

            if (addUI.IsVisible == true)
            {
                currentVisibleAdditionalUIs.Add(simpleUI);
                if (additionalUIAreExclusive == false) return;

                //Se i pannelli sono esclusivi nascondi gli altri.
                //Da notare che impostando i pannelli a non visibili questo handler verrà richiamato,
                //andando così nel blocco else.
                foreach (var sAddUI in additionalUIs)
                    if (sAddUI != simpleUI)
                        sAddUI.IsVisible = false;
            }
            else
            {
                bool wasVisible = currentVisibleAdditionalUIs.Remove(simpleUI);
                if (alwaysShowSomething == false) return;

                //Verifica se c'è ancora qualche pannello visibile con il tab/bottone/toggle visibile.
                if (additionalUIs.Exists(ui => ui.IsVisible) == false)
                {
                    //Se il multipanel deve sempre mostrare almeno un pannello
                    if (alwaysShowSomething)
                    {
                        //Click su un pannello già visibile quindi,
                        //Reimposto a visibile quello appena cliccato (se ha ancora il tab/bottone/toggle visibile)
                        //Altrimenti cerco il primo...
                        if (wasVisible && simpleUI.IsSelectableVisible)
                            simpleUI.IsVisible = true;
                        else
                            additionalUIs.Find(ui => ui.IsSelectableVisible)?.SetVisible(true);
                    }
                }
            }
        }
        private void MultiPanelUI_OnChangedVisibility(BaseAdditionalUI obj)
        {
            if (obj.IsVisible)
                VerifyIfShowSomething();
        }
        #endregion Visibility Handler

        #region Utils
        protected virtual GameObject CreateTabButton(Selectable prefab, int index)
        {
            var gameO = Instantiate(prefab.gameObject);

            if (moveAdditionalUIButtonToTabsPanel)
            {
                gameO.transform.SetParent(tabsPanel);
                gameO.transform.SetSiblingIndex(index);
            }

            return gameO;
        }
        private void VerifyIfShowSomething()
        {
            if (additionalUIs.Exists(ui => ui.IsVisible && ui.IsSelectableVisible) == false)
            {
                if (alwaysShowSomething)
                {
                    additionalUIs.Find(ui => ui.IsSelectableVisible)?.SetVisible(true);
                }
            }
        }
        #endregion Utils
    }
}
