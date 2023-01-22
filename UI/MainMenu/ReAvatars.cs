using ReMod.Core.VRChat;
using UnityEngine;
using UnityEngine.UI;

namespace ReMod.Core.UI.MainMenu
{
    public class ReAviButton
    {
        public ReAviButton(string title, Sprite icon)
        {
            var toinst = QuickMenuEx.userInterface.transform.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/VerticalLayoutGroup User/Cell_MM_SidebarListItem (Favorites)");
            var inst = GameObject.Instantiate(toinst, toinst.parent).gameObject;
            inst.name = "Cell_MM_SidebarListItem (" + title + ")";
            var txt = inst.transform.Find("Mask/Text_Name").GetComponent<TMPro.TextMeshProUGUI>();
            txt.richText = true;
            txt.text = title;         
            inst.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            var btn = inst.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();           
        }
    }
}
