using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;

namespace ReMod.Core.UI.MainMenu
{
    public class ReMMTab : UiElement
    {
        private static GameObject _mmtabButtonPrefab;
        private static GameObject MMTabButtonPrefab
        {
            get
            {
                if (_mmtabButtonPrefab == null)
                {
                    _mmtabButtonPrefab = MenuEx.MMenuTabs.transform.Find("Page_Settings").gameObject;
                }
                return _mmtabButtonPrefab;
            }
        }

        protected ReMMTab(string name, string tooltip, string pageName, Sprite sprite) : base(MMTabButtonPrefab, MMTabButtonPrefab.transform.parent, $"Page_{name}")
        {
            var menuTab = RectTransform.GetComponent<MenuTab>();
            menuTab.field_Public_String_0 = GetCleanName($"MainMenuReMod{pageName}");                    
            menuTab.field_Private_MenuStateController_0 = MenuEx.MMenuStateCtrl;

            var button = GameObject.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(new Action(menuTab.ShowTabContent));

            var uiTooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            
            uiTooltip.text = tooltip;
            uiTooltip.alternateText = tooltip;
            
            //uiTooltip.Method_Public_UiTooltip_String_0(tooltip);
            //uiTooltip.Method_Public_UiTooltip_String_1(tooltip);
            uiTooltip.Method_Public_UiTooltip_String_2(tooltip);
            uiTooltip.Method_Public_UiTooltip_String_3(tooltip);
            //uiTooltip.Method_Public_UiTooltip_String_4(tooltip);
            //uiTooltip.Method_Public_UiTooltip_String_PDM_2(tooltip);

            var iconImage = RectTransform.Find("Icon").GetComponent<Image>();
            iconImage.sprite = sprite;
            iconImage.overrideSprite = sprite;
        }

        public static ReMMTab Create(string name, string tooltip, string pageName, Sprite sprite)
        {
            return new ReMMTab(name, tooltip, pageName, sprite);
        }
    }
}