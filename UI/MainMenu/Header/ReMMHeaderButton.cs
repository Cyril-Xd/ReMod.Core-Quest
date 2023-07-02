using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ReMod.Core.UI.MainMenu.Header;

public class ReMMHeaderButton : ReMMHeaderElement
{
    private static GameObject _buttonPrefab;
    private static GameObject ButtonPrefab
    {
        get
        {
            if (_buttonPrefab == null)
            {
                _buttonPrefab = MenuEx.MMenuParent.Find("Menu_Social/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Header_MM_H2/Button_ToggleNavPanel").gameObject;
            }
            return _buttonPrefab;
        }
    }

    private Button buttonComponent;

    public ReMMHeaderButton(string tooltip, Sprite icon, ReMMPage page, Action onClick) : base(ButtonPrefab, page, tooltip)
    {
        gameObject.name = $"Button_header";

        gameObject.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;

        buttonComponent = gameObject.GetComponent<Button>();
        buttonComponent.onClick.AddListener(new Action(onClick));
    }
}
