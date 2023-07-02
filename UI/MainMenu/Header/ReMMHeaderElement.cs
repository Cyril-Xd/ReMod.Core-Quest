using UnityEngine;
using VRC.UI.Elements.Tooltips;

namespace AGeneralQuestMod.ReMod_Extensions.MainMenu.Header;

public class ReMMHeaderElement
{
    public GameObject gameObject { get; private set; }

    public Transform Container => gameObject.transform.parent;

    public ReMMPageEx Page { get; private set; }

    private UiTooltip Tooltip;

    public ReMMHeaderElement(GameObject prefab, ReMMPageEx page, string tooltip) 
    {
        gameObject = Object.Instantiate(prefab, page.MenuObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Header_MM_H2/RightItemContainer"));
        Page = page;
        Tooltip = gameObject.GetComponent<UiTooltip>() ?? null;
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
    }
}
