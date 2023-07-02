using ReMod.Core.UI;
using ReMod.Core.VRChat;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AGeneralQuestMod.ReMod_Extensions.MainMenu;

public class ReMMCategoryDropdown : UiElement
{
    private static GameObject _dropdownPrefab;
    private static GameObject DropdownPrefab
    {
        get
        {
            if (_dropdownPrefab == null)
            {
                _dropdownPrefab = MenuEx.MMenuParent.Find("Menu_MM_Worlds/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/QM_Foldout_MyWorlds").gameObject;
            }

            return _dropdownPrefab;
        }
    }

    private TextMeshProUGUI textComponent;

    public Toggle toggle { get; private set; }

    public Transform Container { get; private set; }

    public event Action<bool> OnToggle;

    public ReMMCategoryDropdown(string title, ReMMPage page, Action<bool> onToggle) : base(DropdownPrefab, page.GetCategoryButtonContainer(), $"Dropdown_{title}")
    {
        Container = GameObject.transform;

        textComponent = GameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>();
        textComponent.text = title;
        textComponent.enabled = true;

        toggle = GameObject.transform.Find("Background_Button").GetComponent<Toggle>();
        toggle.isOn = true;

        toggle.onValueChanged = new Toggle.ToggleEvent();
        toggle.onValueChanged.AddListener(new Action<bool>(onToggle));
    }
}
