using ReMod.Core.VRChat;
using System;
using UnityEngine;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;

namespace ReMod.Core.UI.MainMenu
{
    public class ReMMenuPage : UiElement, IButtonSystem
    {
        
        private static GameObject _mmmenuPagePrefab;
        private static GameObject MMMenuPagePrefab
        {
            get
            {
                if (_mmmenuPagePrefab == null)
                {
                    _mmmenuPagePrefab = QuickMenuEx.MMenuParent.transform.Find("Menu_Avatars").gameObject;
                }
               
                return _mmmenuPagePrefab;
            }
        }
        public UIPage UiPage { get; }
        public event Action OnOpen;
        public ReMMenuPage(string text, bool isRoot = false, string color = "#ffffff") : base(MMMenuPagePrefab, QuickMenuEx.MenuParent, $"Menu_{text}", false)
        {
            UnityEngine.Object.DestroyImmediate(GameObject.GetComponent<MainMenuAvatars>());
        }

        public void Open()
        {
            UiPage.gameObject.active = true;
            QuickMenuEx.MenuStateCtrl.Method_Public_Void_String_ObjectPublicStBoAc1ObObUnique_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(UiPage.field_Public_String_0);

            OnOpen?.Invoke();
        }

        public static ReMMenuPage Create(string text, bool isRoot)
        {
            return new ReMMenuPage(text, isRoot);
        }
    }
}