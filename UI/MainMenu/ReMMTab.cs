using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using Object = UnityEngine.Object;

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
                    _mmtabButtonPrefab = QuickMenuEx.MMenuTabs.transform.Find("Page_Settings").gameObject;
                }
                return _mmtabButtonPrefab;
            }
        }

        protected ReMMTab(string name, string tooltip, string pageName, Sprite sprite) : base(MMTabButtonPrefab, MMTabButtonPrefab.transform.parent, $"Page_{name}")
        {
            var menuTab = MMTabButtonPrefab.gameObject;
            menuTab.name = GetCleanName($"MainMenuReMod{pageName}");
            var menuTabComp = menuTab.GetComponent<MenuTab>();           
            menuTabComp.field_Private_MenuStateController_0 = QuickMenuEx.MenuStateCtrl;

            var button = GameObject.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(new Action(menuTabComp.ShowTabContent));

            var uiTooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            uiTooltip.field_Public_String_0 = tooltip;
            uiTooltip.field_Public_String_1 = tooltip;

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