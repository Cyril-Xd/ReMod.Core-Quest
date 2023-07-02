using AGeneralQuestMod.Internals.Code;
using MelonLoader;
using ReMod.Core.VRChat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;

namespace AGeneralQuestMod.ReMod_Extensions.MainMenu;

public class ReMMOptionSelector : ReMMSectionElement
{
    private static GameObject _selectorPrefab;
    private static GameObject SelectorPrefab
    {
        get
        {
            if(_selectorPrefab == null)
            {
                _selectorPrefab = MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/ShapeFollowsCameraRotation").gameObject;
            }
            return _selectorPrefab;
        }
    }

    private Button buttonBack;
    private Button buttonForward;

    private TextMeshProUGUIEx OptionTextComponent;

    private List<string> Options = new();

    public ReMMCategorySection Section { get; private set; }

    public string CurrentOption { get; private set; }
    public int CurrentOptionIndex { get; private set; }

    public ReMMOptionSelector(string title, List<string> options, string tooltip, uint defaultOptionIndex = 0, Transform parent = null, bool separator = true, Action onBack = null, Action onForward = null, ReMMCategorySection section = null) : 
    base(SelectorPrefab, parent, tooltip, true, separator) 
    {
        //avoid nullreferences
        onBack ??= Utils.EMPTY_CALLBACK;
        onForward ??= Utils.EMPTY_CALLBACK;

        LeftItemContainer.Find("Title").GetComponent<TextMeshProUGUIEx>().text = title;
        
        //reassign styleelement because it is not attached to the main parent
        StyleElement = RightItemContainer.GetComponent<StyleElement>();

        if (section != null)
        {
            Section = section;
        }

        Options = options;

        buttonBack = RightItemContainer.Find("ButtonLeft").GetComponent<Button>();
        buttonForward = RightItemContainer.Find("ButtonRight").GetComponent<Button>();

        OptionTextComponent = RightItemContainer.Find("OptionSelectionBox/Text_MM_H3").GetComponent<TextMeshProUGUIEx>();

        if (Options == null)
        {
            MelonLogger.Warning("ReMMOptionSelector has no options");
            OptionTextComponent.text = "";
        }

        buttonBack.onClick.AddListener(new Action(onBack));
        buttonBack.onClick.AddListener(new Action(() =>
        {
            //because the argument is unsigned, we have to wrap manually
            if (CurrentOptionIndex == 0)
            {
                Set((uint)(Options.Count - 1));
            }
            else
            {
                Set((uint)(CurrentOptionIndex - 1));
            }
        }
        ));

        buttonForward.onClick.AddListener(new Action(onForward));
        buttonForward.onClick.AddListener(new Action(() => Set((uint)(CurrentOptionIndex + 1))));

        //trigger the fix when category is opened
        Section.Category.ButtonObj.GetComponent<Button>().onClick.AddListener(new Action(() => MelonCoroutines.Start(fix1())));

        Set(defaultOptionIndex);
    }

    public IEnumerator fix1()
    {
        //waits a bit then refreshes the selected option
        //i don't know why we have to do this, but we do
        yield return new WaitForSeconds(0.025f);
        try
        {
            Set();
        }
        catch (Exception) { }
    }

    //argument is a uint to discourage passing a negative argument
    public string Set(uint index = 9999)
    {
        //this basically becomes a refresh if <index> is 9999 (don't call this with that argument)
        if (index == 9999)
        {
            return Set((uint)CurrentOptionIndex);
        }

        //once again, manual wrapping
        uint newindex = (uint)(index % Options.Count);
        
        CurrentOptionIndex = (int)newindex;
        CurrentOption = Options[(int)newindex];

        OptionTextComponent.text = CurrentOption;

        return CurrentOption;
    }
}
