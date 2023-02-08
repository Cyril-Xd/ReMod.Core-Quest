using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.MainMenu
{
    public class ReMMUserButton
    {
        private VRC.UI.Elements.Tooltips.UiTooltip _tooltip;

        public string Tooltip
        {
            get => _tooltip != null ? _tooltip.field_Public_String_0 : "";
            set
            {
                if (_tooltip == null) return;
                _tooltip.field_Public_String_0 = value;
                _tooltip.field_Public_String_1 = value;
            }
        }
        public ReMMUserButton(string name, string tooltip, Action onClick, Sprite icon)
        {
            var toinst = QuickMenuEx.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_UserDetail/ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content/JoinBtn");
            var inst = GameObject.Instantiate(toinst, toinst.parent).gameObject;
            inst.name = "MMUButton_" + name;
            var txt = inst.transform.Find("Text_ButtonName").GetComponent<TMPro.TextMeshProUGUI>();
            txt.richText = true;
            txt.text = name;
            var uiTooltips = inst.GetComponents<VRC.UI.Elements.Tooltips.UiTooltip>();
            if (uiTooltips.Length > 0)
            {
                //Fuck tooltips, all my friends hate tooltips
                _tooltip = uiTooltips[0];

                for (int i = 1; i < uiTooltips.Length; i++)
                    Object.DestroyImmediate(uiTooltips[i]);
            }

            if (_tooltip != null)
            {
                _tooltip.field_Public_String_0 = tooltip;
                _tooltip.field_Public_String_1 = tooltip;
            }
            inst.transform.Find("Text_ButtonName/Icon").GetComponent<Image>().overrideSprite = icon;
            var btn = inst.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(new Action(onClick));
        }

    }

    public class ReMMAvatarButton
    {
        private VRC.UI.Elements.Tooltips.UiTooltip _tooltip;

        public string Tooltip
        {
            get => _tooltip != null ? _tooltip.field_Public_String_0 : "";
            set
            {
                if (_tooltip == null) return;
                _tooltip.field_Public_String_0 = value;
                _tooltip.field_Public_String_1 = value;
            }
        }
        public ReMMAvatarButton(string name, string tooltip, Action onClick, Sprite icon)
        {
            var toinst = QuickMenuEx.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Panel_SelectedAvatar/ScrollRect/Viewport/VerticalLayoutGroup/Button_AvatarDetails");
            var inst = GameObject.Instantiate(toinst, toinst.parent).gameObject;
            inst.name = "MMAviButton_" + name;
            var txt = inst.transform.Find("Text_ButtonName").GetComponent<TMPro.TextMeshProUGUI>();
            txt.richText = true;
            txt.text = name;
            var uiTooltips = inst.GetComponents<VRC.UI.Elements.Tooltips.UiTooltip>();
            if (uiTooltips.Length > 0)
            {
                //Fuck tooltips, all my friends hate tooltips
                _tooltip = uiTooltips[0];

                for (int i = 1; i < uiTooltips.Length; i++)
                    Object.DestroyImmediate(uiTooltips[i]);
            }

            if (_tooltip != null)
            {
                _tooltip.field_Public_String_0 = tooltip;
                _tooltip.field_Public_String_1 = tooltip;
            }
            inst.transform.Find("Text_ButtonName/Icon").GetComponent<Image>().overrideSprite = icon;
            var btn = inst.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(new Action(onClick));
        }
    }
}