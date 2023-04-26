using ReMod.Core.VRChat;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace ReMod.Core.UI.MainMenu
{
    public class ReMMCategory : UiElement
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
        private static GameObject _categorycontainerPrefab;
        private static GameObject CategoryContainerPrefab
        {
            get
            {
                if (_categorycontainerPrefab == null)
                {
                    _categorycontainerPrefab = MenuEx.MMenuParent.transform.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/Audio&Voice").gameObject;
                }

                return _categorycontainerPrefab;
            }
        }
        protected GameObject ButtonObj;
        protected GameObject ContainerObj;
        protected TextMeshProUGUI ButtonText;
        protected ReMMenuPage Menu;


        public ReMMCategory(ReMMenuPage menu, string btnText, string tooltip, Transform parent = null) : base(CategoryButtonPrefab, parent == null ? CategoryContainerPrefab.transform.parent : parent, btnText)
        {
           
            Menu = menu;
            ButtonObj = UnityEngine.Object.Instantiate(CategoryButtonPrefab, menu.GetCategoryButtonContainer());
            ButtonObj.name = $"CategoryBtn-{btnText}";
            ButtonObj.transform.Find("Icon").gameObject.SetActive(false);
            ButtonText = ButtonObj.transform.Find("Mask/Text_Name").GetComponent<TextMeshProUGUI>();
            SetButtonText(btnText);

            ContainerObj = UnityEngine.Object.Instantiate(CategoryContainerPrefab, menu.GetCategoryChildContainer());
            ContainerObj.name = $"CatetgoryChild-{btnText}";
            ContainerObj.transform.DestroyChildren();

            var rbs = ButtonObj.GetComponent<RadioButtonSelector>();
            rbs.field_Public_String_0 = $"-CatetgoryChild-{btnText}";
            rbs.field_Public_TextMeshProUGUI_0 = menu.GetCategoryTitle();
            rbs.field_Public_GameObject_1 = ContainerObj;

            SetTooltipText(tooltip);
        }

       


        public void SetButtonText(string newText)
        {
            ButtonText.text = newText;
        }

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
        }

        public Transform GetContainer()
        {
            return ContainerObj.transform;
        }

        public ReMMenuPage GetMenu()
        {
            return Menu;
        }
        public static ReMMenuPage Create(string text, Sprite icon, bool isRoot)
        {
            return new ReMMenuPage(text, icon, isRoot);
        }
    }
}