using ReMod.Core.VRChat;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;

namespace ReMod.Core.UI.MainMenu.Header;

public class ReMMHeaderDropdown : ReMMHeaderElement
{
    private static GameObject _dropdownPrefab;
    private static GameObject DropdownPrefab
    {
        get
        {
            if (_dropdownPrefab == null)
            {
                _dropdownPrefab = MenuEx.MMenuParent.Find("Menu_MM_Profile/ScrollRect/Viewport/VerticalLayoutGroup/Playlists (3)/Header_MM_H2/RightItemContainer/Field_MM_SortBy").gameObject;
            }
            return _dropdownPrefab;
        }
    }

    public VRCDropdown DropdownComponent { get; private set; }
    public TextMeshProUGUI TextComponent { get; private set; }

    public string SelectedElement { get; private set; }
    public int SelectedIndex { get; private set; }

    //the index of the selected element is passed in onItemSelected
    public ReMMHeaderDropdown(string name, string tooltip, Sprite icon, ReMMPage page, Action<int> onItemSelected = null) : base(DropdownPrefab, page, tooltip)
    {
        //prevent nullreference
        onItemSelected ??= (i) => {};

        gameObject.name = $"Field_MM_{name}";

        DropdownComponent = gameObject.GetComponent<VRCDropdown>();
        TextComponent = gameObject.transform.Find("Text_Header").GetComponent<TextMeshProUGUI>();

        TextComponent.text = name;

        gameObject.transform.Find("Icon_SortBy").GetComponent<Image>().overrideSprite = icon;

        DropdownComponent.onValueChanged.AddListener(new Action<int>((i) =>
        {
            SelectedElement = ElementAtIndex(i);
            SelectedIndex = i;
        }));
        DropdownComponent.onValueChanged.AddListener(new Action<int>(onItemSelected));
    }

    public void AddItem(string name)
    {
        var tmp = new Il2CppSystem.Collections.Generic.List<string>();
        tmp.Add(name);
        DropdownComponent.AddOptions(tmp);
    }
    public void AddItems(Il2CppSystem.Collections.Generic.List<string> names) => DropdownComponent.AddOptions(names);

    public string ElementAtIndex(int index) => DropdownComponent.options.get_Item(index).text;
}
