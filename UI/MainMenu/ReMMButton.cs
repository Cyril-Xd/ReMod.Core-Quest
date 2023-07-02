using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using Object = UnityEngine.Object;

namespace AGeneralQuestMod.ReMod_Extensions.MainMenu;

public class ReMMButton : ReMMSectionElement
{
    private readonly Button _buttonComponent;

    private static GameObject _buttonprefab;
    private static GameObject ButtonPrefab
    {
        get
        {
            if (_buttonprefab == null)
            {
                _buttonprefab = MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/AlwaysShowVisualAide").gameObject;
            }

            return _buttonprefab;
        }
    }

    private TextMeshProUGUIEx _textComponent;
    public string Text
    {
        get => _textComponent.text;
        set => _textComponent.text = value;
    }

    public bool Interactable
    {
        get => _buttonComponent.interactable;
        set
        {
            _buttonComponent.interactable = value;

            StyleElement?.OnEnable();
        }
    }

    public ReMMButton(string title, string buttontext, string tooltip, Action onClick, Transform parent = null, bool separator = true) : base(ButtonPrefab, parent, tooltip, true, separator)
    {
        Object.Destroy(gameObject.GetComponent<Toggle>());
        Object.Destroy(RightItemContainer.Find("ButtonElement_CheckBox").gameObject);

        var button = new ReMMAvatarButton(buttontext, tooltip, onClick, null, RightItemContainer);
        _buttonComponent = button.btn;

        var hlg = RightItemContainer.gameObject.GetComponent<HorizontalLayoutGroup>();

        hlg.childAlignment = TextAnchor.MiddleRight;
        hlg.childControlWidth = true;   

        _textComponent = LeftItemContainer.Find("Title").GetComponent<TextMeshProUGUIEx>();
        Text = title;
    }
}