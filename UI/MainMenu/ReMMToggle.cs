using Oculus.Platform;
using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.MainMenu;

public class ReMMToggle
{
    private readonly Toggle _toggleComponent;

    private static GameObject _toggleprefab;
    private static GameObject TogglePrefab
    {
        get
        {
            if(_toggleprefab == null)
            {
                _toggleprefab = MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/AlwaysShowVisualAide").gameObject;
            }

            return _toggleprefab;
        }
    }

    private bool _valueHolder;
    public bool Value
    {
        get => _valueHolder;
        set => Toggle(value);
    }

    private StyleElement _toggleStyleElement;

    private TextMeshProUGUIEx _textComponent;
    public string Text
    {
        get => _textComponent.text;
        set => _textComponent.text = value;
    }

    private UiToggleTooltip _tooltip;

    public string Tooltip
    {
        get => _tooltip != null ? _tooltip.text : "";
        set
        {
            if (_tooltip == null) return;
            _tooltip.text = value;
        }
    }

    public bool Interactable
    {
        get => _toggleComponent.interactable;
        set
        {
            _toggleComponent.interactable = value;

            _toggleStyleElement?.OnEnable();
        }
    }

    public GameObject toggleObj {get; protected set;}

    public ReMMToggle(string title, string tooltip, Action<bool> onToggle, bool defaultState = false, Transform parent = null, bool separator = true)
    {
        toggleObj = Object.Instantiate(TogglePrefab, parent);

        _toggleComponent = toggleObj.GetComponent<Toggle>();

        _textComponent = toggleObj.transform.Find("LeftItemContainer/Title").GetComponent<TextMeshProUGUIEx>();
        Text = title;

        _toggleStyleElement = toggleObj.GetComponent<StyleElement>();

        _toggleComponent.onValueChanged = new Toggle.ToggleEvent();
        _toggleComponent.onValueChanged.AddListener(new Action<bool>((b) =>
        {
            var handle = toggleObj.GetComponent<ToggleSetting>()._handle;
            toggleObj.transform.Find("RightItemContainer/ButtonElement_CheckBox/On_Container").gameObject.SetActive(b);
            toggleObj.transform.Find("RightItemContainer/ButtonElement_CheckBox/Off_Container").gameObject.SetActive(!b);
            toggleObj.transform.Find("RightItemContainer/ButtonElement_CheckBox/On_Container/On_Text").gameObject.SetActive(b);
            toggleObj.transform.Find("RightItemContainer/ButtonElement_CheckBox/Off_Container/Off_Text").gameObject.SetActive(!b);
            handle.localPosition += new Vector3(b == true ? handle.rect.width * 2 : -handle.rect.width * 2, 0f, 0f);
        }));
        _toggleComponent.onValueChanged.AddListener(new Action<bool>(onToggle));

        _tooltip = toggleObj.GetComponent<UiToggleTooltip>();
        _tooltip.text = tooltip;
        _tooltip.alternateText = tooltip;
        
        if (separator) Object.Instantiate(MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/Separator").gameObject, parent);
        Toggle(defaultState, false);
    }

    public void Toggle(bool value, bool callback = true)
    {
        _valueHolder = value;
        _toggleComponent.Set(value, callback);
    }
}
 
