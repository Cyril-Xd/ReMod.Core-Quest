using ReMod.Core.VRChat;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Tooltips;
using Object = UnityEngine.Object;

namespace AGeneralQuestMod.ReMod_Extensions.MainMenu;

public class ReMMSectionElement
{
    public Transform LeftItemContainer { get; protected set; }
    public Transform RightItemContainer { get; protected set; }

    public GameObject gameObject { get; protected set; }

    public StyleElement StyleElement { get; protected set; }

    public UiToggleTooltip Tooltip { get; protected set; }
    public string TooltipText
    {
        get => Tooltip.text;
        set => Tooltip.text = value;
    }

    public ReMMSectionElement(GameObject prefab, Transform container, string tooltip, bool sizefitter = true, bool separator = true) 
    {
        gameObject = Object.Instantiate(prefab, container);

        LeftItemContainer = gameObject.transform.Find("LeftItemContainer") ?? null;
        RightItemContainer = gameObject.transform.Find("RightItemContainer") ?? null;

        StyleElement = gameObject.GetComponent<StyleElement>() ?? null;

        Tooltip = gameObject.GetComponent<UiToggleTooltip>() ?? null;
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

        if(sizefitter)
        {
            ContentSizeFitter csf = RightItemContainer.gameObject.AddComponent<ContentSizeFitter>();
            csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        if (separator) Object.Instantiate(MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/Separator").gameObject, container);
    }

    public ReMMSectionElement(GameObject prefab, ReMMCategorySection section, string tooltip, bool sizefitter = true) : this(prefab, section.ContentArea, tooltip, sizefitter) { }
}
