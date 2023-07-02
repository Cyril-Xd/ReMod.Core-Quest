using ReMod.Core.VRChat;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;
using System;
using ReMod.Core.UI;

namespace ReMod.Core.UI.MainMenu;

//damn these class names are getting long
public class ReMMSidebarHeaderButton : UiElement
{
    private static GameObject _buttonPrefab;
    private static GameObject ButtonPrefab
    {
        get
        {
            if (_buttonPrefab == null)
            {
                _buttonPrefab = MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/DynamicSidePanel_Header/Button_Logout").gameObject;
            }

            return _buttonPrefab;
        }
    }

    private TextMeshProUGUIEx _textComponent;
    public string Text
    {
        get => _textComponent.text;
        set => _textComponent.text = value;
    }

    public ReMMSidebarHeaderButton(ReMMPage menu, string text, string tooltip, Sprite icon, Action onClick) : base(ButtonPrefab, menu.GetSidePanelHeader(), $"sb_btn_{text}")
    {
        UnityEngine.Object.Destroy(GameObject.GetComponent<LogoutButton>());

        _textComponent = GameObject.transform.Find("Background_Field/Text_FieldContent").GetComponent<TextMeshProUGUIEx>();

        if (icon == null)
        {
            GameObject.transform.Find("Background_Field/Text_FieldContent").SetAsFirstSibling();
        }

        Text = text;
        GameObject.transform.Find("Background_Field/Icon").GetComponent<Image>().overrideSprite = icon;
        GameObject.transform.Find("Background_Field/Icon").gameObject.SetActive(icon != null);

        var Tooltip = GameObject.GetComponent<UiTooltip>();
        if (Tooltip != null)
        {
            Tooltip.text = tooltip;
            Tooltip.alternateText = tooltip;
            Tooltip.Method_Public_UiTooltip_String_0(tooltip);
            Tooltip.Method_Public_UiTooltip_String_1(tooltip);
            Tooltip.Method_Public_UiTooltip_String_2(tooltip);
            Tooltip.Method_Public_UiTooltip_String_3(tooltip);
            Tooltip.Method_Public_UiTooltip_String_4(tooltip);
            Tooltip.Method_Public_UiTooltip_String_PDM_0(tooltip);
            Tooltip.Method_Public_UiTooltip_String_PDM_2(tooltip);
            Tooltip.Method_Public_UiTooltip_String_PDM_4(tooltip);
            Tooltip.Method_Public_UiTooltip_String_PDM_5(tooltip);
            Tooltip.Method_Public_UiTooltip_String_PDM_6(tooltip);
        }

        GameObject.GetComponent<Button>().onClick.AddListener(new Action(onClick));

        menu.MenuTitleText.transform.parent.GetComponent<LayoutElement>().minHeight += 95;
        menu.MenuTitleText.transform.parent.Find("Separator").GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 95f);
    }
}
