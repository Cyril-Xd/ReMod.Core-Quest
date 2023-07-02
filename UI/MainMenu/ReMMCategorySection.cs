using ReMod.Core.UI;
using ReMod.Core.VRChat;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.MainMenu;

public class ReMMCategorySection : UiElement
{
    private static GameObject _categorysectionPrefab;
    private static GameObject CategorySectionPrefab
    {
        get
        {
            if (_categorysectionPrefab == null)
            {
                _categorysectionPrefab = MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode").gameObject;
            }     
            
            return _categorysectionPrefab;
        }
    }

    protected GameObject TitleContainer;
    protected TextMeshProUGUIEx TitleText;

    public Transform ContentArea { get; private set; }

    public ReMMCategory Category { get; private set; }

    public string Title
    {
        get => TitleText.text;
        set => TitleText.text = value; 
    }

    public ReMMCategorySection(string title, bool collapsible = false, Transform parent = null, ReMMCategory category = null, string color = null) : base(CategorySectionPrefab, parent ?? CategorySectionPrefab.transform.parent, $"Section-{title}")
    {
        TitleContainer = GameObject.transform.Find("MM_Foldout/Label").gameObject;
        TitleText = TitleContainer.GetComponent<TextMeshProUGUIEx>();

        if (color == null)
        {
            Title = title;
        }
        else
        {
            TitleText.richText = true;
            Title = $"<color={color}>{title}</color>";
        }

        if (category != null)
        {
            Category = category;
        }

        if (collapsible == false)
        {
            TitleContainer.transform.SetAsFirstSibling();
        }

        GameObject.transform.Find("MM_Foldout/Background_Button").GetComponent<Toggle>().enabled = collapsible;
        GameObject.transform.Find("MM_Foldout/Arrow").gameObject.SetActive(collapsible);

        ContentArea = GameObject.transform.Find("Settings_Panel_1/VerticalLayoutGroup");
        for (int i = ContentArea.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(ContentArea.GetChild(i).gameObject);
        }
        //instantiate the background manually because it doesn't carry over :(
        Object.Instantiate(MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/Background_Info").gameObject, ContentArea);
    }

    public ReMMToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false, bool separator = true) => new(text, tooltip, onToggle, defaultValue, ContentArea, separator);

    public ReMMButton AddButton(string title, string buttontext, string tooltip, Action onClick, bool separator = true) => new(title, buttontext, tooltip, onClick, ContentArea, separator);

    public ReMMText AddTitledText(string title, string text, string tooltip, int fontSize, bool separator = true) => new(title, text, tooltip, ContentArea, fontSize, separator);
    public ReMMText AddText(string text, string tooltip, int fontSize, bool separator = true) => new(text, "", tooltip, ContentArea, fontSize, separator);

    public ReMMOptionSelector AddOptionSelector(string title, List<string> options, string tooltip, uint defaultOptionIndex = 0, Action onBack = null, Action onForward = null, bool separator = true) => new(title, options, tooltip, defaultOptionIndex, ContentArea, separator, onBack, onForward, this);
}
