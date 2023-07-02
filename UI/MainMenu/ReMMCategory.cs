using ReMod.Core.UI;
using ReMod.Core.Unity;
using ReMod.Core.VRChat;
using System;
using TMPro;
using UnityEngine;
using VRC.UI.Elements.Controls;
using Object = UnityEngine.Object;

namespace AGeneralQuestMod.ReMod_Extensions.MainMenu;


public class ReMMCategoryEx : UiElement
{
    private static GameObject _categorybuttonPrefab;
    private static GameObject CategoryButtonPrefab
    {
        get
        {
            if (_categorybuttonPrefab == null)
            {
                _categorybuttonPrefab = MenuEx.MMenuParent.transform.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_Audio & Voice").gameObject;
            }

            return _categorybuttonPrefab;
        }
    }
    
    private static GameObject _categorycounterbuttonPrefab;
    private static GameObject CategoryCounterButtonPrefab
    {
        get
        {
            if (_categorycounterbuttonPrefab == null)
            {
                _categorycounterbuttonPrefab = MenuEx.MMenuParent.transform.Find("Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/VerticalLayoutGroup User/Cell_MM_SidebarListItem (Other)").gameObject;
            }

            return _categorycounterbuttonPrefab;
        }
    }

    private static GameObject _categorycontainerPrefab;
    private static GameObject CategoryContainerPrefab
    {
        get
        {
            if (_categorycontainerPrefab == null)
            {
                _categorycontainerPrefab = MenuEx.MMenuParent.transform.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice").gameObject;
            }

            return _categorycontainerPrefab;
        }
    }

    public TextMeshProUGUI CounterText { get; private set; }
    public GameObject ButtonObj { get; private set; }
    protected GameObject ContainerObj;
    protected TextMeshProUGUI ButtonText;
    protected ReMMPageEx Menu;

    public event Action onOpen;
    public event Action onClose;

    public ReMMCategoryEx(ReMMPageEx menu, string btnText, string tooltip, Transform parent = null, bool hasCounter = false) : base(hasCounter ? CategoryCounterButtonPrefab : CategoryButtonPrefab, parent ?? menu.GetCategoryButtonContainer(), btnText)
    {
        Menu = menu;
        ButtonObj = GameObject;
        ButtonObj.name = $"CategoryBtn-{btnText}";
        ButtonObj.transform.Find("Icon").gameObject.SetActive(false);

        CounterText = hasCounter ? ButtonObj.transform.Find("Count_BG/Text_Number").GetComponent<TextMeshProUGUI>() : null;

        ButtonText = ButtonObj.transform.Find("Mask/Text_Name").GetComponent<TextMeshProUGUI>();
        SetButtonText(btnText);

        ContainerObj = Object.Instantiate(CategoryContainerPrefab, menu.GetCategoryChildContainer());
        ContainerObj.name = $"CatetgoryChild-{btnText}";
        for(int i = ContainerObj.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(ContainerObj.transform.GetChild(i).gameObject);
        }

        var edl = ContainerObj.AddComponent<EnableDisableListener>();
        edl.OnEnableEvent += () => onOpen?.Invoke();
        edl.OnEnableEvent -= () => onClose?.Invoke();

        var rbs = ButtonObj.GetComponent<RadioButtonSelector>();
        rbs.field_Public_String_0 = $"-CatetgoryChild-{btnText}";
        rbs.field_Public_TextMeshProUGUI_0 = menu.GetCategoryTitle();
        rbs.field_Public_GameObject_1 = ContainerObj;

        SetTooltipText(tooltip);
    }

    public ReMMCategoryEx(Transform transform) : base(transform) => Menu = new ReMMPageEx(transform.parent.parent.parent.parent.parent.parent);

    public ReMMCategorySection AddSection(string title, bool collapsible = false, string color = null) => new ReMMCategorySection(title, collapsible, GetContainer(), this, color);


    public void SetButtonText(string newText) => ButtonText.text = newText;

    public void SetTooltipText(string newToolTip)
    {
        var uiTooltip = ButtonObj.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
        uiTooltip.text = newToolTip;
        uiTooltip.alternateText = newToolTip;
        uiTooltip.Method_Public_UiTooltip_String_0(newToolTip);
        uiTooltip.Method_Public_UiTooltip_String_1(newToolTip);
        uiTooltip.Method_Public_UiTooltip_String_2(newToolTip);
        uiTooltip.Method_Public_UiTooltip_String_3(newToolTip);
        uiTooltip.Method_Public_UiTooltip_String_4(newToolTip);
        uiTooltip.Method_Public_UiTooltip_String_PDM_2(newToolTip);
    }

    public Transform GetContainer() => ContainerObj.transform;

    public ReMMPageEx GetMenu() => Menu;
    public static ReMMPageEx Create(string text, Sprite icon, bool isRoot) => new ReMMPageEx(text, icon, isRoot);
}
