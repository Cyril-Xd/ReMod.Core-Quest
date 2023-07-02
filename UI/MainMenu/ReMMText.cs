using ReMod.Core.VRChat;
using UnityEngine.UI;
using UnityEngine;
using VRC.UI.Elements.Controls;
using Object = UnityEngine.Object;
using TMPro;

namespace AGeneralQuestMod.ReMod_Extensions.MainMenu;

public class ReMMText : ReMMSectionElement
{
    private static GameObject _textprefab;
    private static GameObject TextPrefab
    {
        get
        {
            if (_textprefab == null)
            {
                _textprefab = MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/AlwaysShowVisualAide").gameObject;
            }

            return _textprefab;
        }
    }

    private TextMeshProUGUIEx _textComponent;

    public string Text
    {
        get => _textComponent.text;
        set => _textComponent.text = value;
    }

    public ReMMText(string title, string menutext, string tooltip, Transform parent = null, int fontSize = 30, bool separator = true) : base(TextPrefab, parent, tooltip, true, separator)
    {
        Object.Destroy(gameObject.GetComponent<Toggle>());
        Object.Destroy(RightItemContainer.Find("ButtonElement_CheckBox").gameObject);

        _textComponent = LeftItemContainer.Find("Title").GetComponent<TextMeshProUGUIEx>();
        Text = title;
        _textComponent.fontSize = fontSize;

        var text = Object.Instantiate(MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/AlwaysShowVisualAide/LeftItemContainer/Title").gameObject, RightItemContainer);
        text.GetComponent<TextMeshProUGUI>().text = menutext;
        text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
        text.GetComponent<TextMeshProUGUI>().fontSize = fontSize;  
    }
}