using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.MainMenu;

public class ReMMUserButton
{
    private VRC.UI.Elements.Tooltips.UiTooltip _tooltip;

    public string Tooltip
    {
        get => _tooltip != null ? _tooltip.text : "";
        set
        {
            if (_tooltip == null) return;
            _tooltip.text = value;
        }
    }
    public ReMMUserButton(string name, string tooltip, Action onClick, Sprite icon, Transform parent)
    {
        var toinst = MenuEx.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_UserDetail/ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content/JoinBtn");
        var inst = GameObject.Instantiate(toinst, parent).gameObject;
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
            _tooltip.text = tooltip;
        }
        inst.transform.Find("Text_ButtonName/Icon").GetComponent<Image>().overrideSprite = icon;
        var btn = inst.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(new Action(onClick));
    }

}

public class ReMMAvatarButton
{
    private VRC.UI.Elements.Tooltips.UiTooltip _tooltip;

    public Button btn;

    public string Tooltip
    {
        get => _tooltip != null ? _tooltip.text : "";
        set
        {
            if (_tooltip == null) return;
            _tooltip.text = value;
        }
    }

    public GameObject gameObject { get; private set; }

    public ReMMAvatarButton(string name, string tooltip, Action onClick, Sprite icon, Transform parent)
    {
        var toinst = MenuEx.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Panel_SelectedAvatar/ScrollRect/Viewport/VerticalLayoutGroup/Button_AvatarDetails");
        gameObject = Object.Instantiate(toinst, parent).gameObject;
        gameObject.name = "MMAviButton_" + name;
        var txt = gameObject.transform.Find("Text_ButtonName").GetComponent<TMPro.TextMeshProUGUI>();
        txt.richText = true;
        txt.text = name;
        var uiTooltips = gameObject.GetComponents<VRC.UI.Elements.Tooltips.UiTooltip>();
        if (uiTooltips.Length > 0)
        {
            //Fuck tooltips, all my friends hate tooltips
            _tooltip = uiTooltips[0];

            for (int i = 1; i < uiTooltips.Length; i++)
                Object.DestroyImmediate(uiTooltips[i]);
        }

        if (_tooltip != null)
        {
            _tooltip.text = tooltip;
            _tooltip.alternateText = tooltip;
             
            //_tooltip.Method_Public_UiTooltip_String_0(tooltip);
            //_tooltip.Method_Public_UiTooltip_String_1(tooltip);
            _tooltip.Method_Public_UiTooltip_String_2(tooltip);
            _tooltip.Method_Public_UiTooltip_String_3(tooltip);
            //_tooltip.Method_Public_UiTooltip_String_4(tooltip);
            //_tooltip.Method_Public_UiTooltip_String_PDM_2(tooltip);
        }

        var hlg2 = gameObject.AddComponent<HorizontalLayoutGroup>();
        hlg2.childAlignment = TextAnchor.MiddleRight;

        var csf = gameObject.transform.Find("Background_Button").gameObject.AddComponent<ContentSizeFitter>();
        csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        var csf2 = gameObject.AddComponent<ContentSizeFitter>();

        var hlg = gameObject.transform.Find("Background_Button").gameObject.AddComponent<HorizontalLayoutGroup>();
        hlg.childControlWidth = true;
        hlg.childControlHeight = true;
        hlg.childAlignment = TextAnchor.MiddleCenter;
        hlg.padding = new RectOffset(25, 25, 17, 17);

        csf2.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        csf2.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        gameObject.transform.Find("Text_ButtonName/Icon").gameObject.SetActive(icon != null);
        Object.Instantiate(gameObject.transform.Find("Text_ButtonName").gameObject, gameObject.transform.Find("Background_Button"));
        Object.Destroy(gameObject.transform.Find("Text_ButtonName").gameObject);

        btn = gameObject.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(new Action(onClick));
    }
}
