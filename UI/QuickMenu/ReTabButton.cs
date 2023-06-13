using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReTabButton : UiElement
    {
        private static GameObject _tabButtonPrefab;
        private static GameObject TabButtonPrefab
        {
            get
            {
                if (_tabButtonPrefab == null)
                {
                    _tabButtonPrefab = MenuEx.QMInstance.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings").gameObject;
                }
                return _tabButtonPrefab;
            }
        }

        protected ReTabButton(string name, string tooltip, string pageName, Sprite sprite) : base(TabButtonPrefab, TabButtonPrefab.transform.parent, $"Page_{name}")
        {
            var menuTab = RectTransform.GetComponent<MenuTab>();
            menuTab.field_Public_String_0 = GetCleanName($"QuickMenuReMod{pageName}");
            menuTab.field_Private_MenuStateController_0 = MenuEx.QMenuStateCtrl;

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

        public static ReTabButton Create(string name, string tooltip, string pageName, Sprite sprite)
        {
            return new ReTabButton(name, tooltip, pageName, sprite);
        }
    }
}
