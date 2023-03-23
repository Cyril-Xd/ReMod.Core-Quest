using System;
using TMPro;
using UnityEngine;
using VRC.DataModel;
using VRC.UI.Elements.Controls;

namespace ReMod.Core.VRChat
{
    public static class PopupManagerExtensions
    {
        private static VRCInputField _KeyboardComponent;
        private static GameObject keyboardGameObject;
        
        public static void Alert(string title, string content, Action leftBtnAction = null, Action rightBtnAction = null, string leftBtnText = "Yes", string rightBtnText = "No", bool showInMainMenu = false)
        {
            if (showInMainMenu)
                MenuEx.MMInstance.Method_Public_Void_String_String_String_String_Action_Action_String_TextAlignmentOptions_0(title, content, leftBtnText, rightBtnText, leftBtnAction, rightBtnAction);
            else
                MenuEx.Instance.Method_Public_Void_String_String_String_String_Action_Action_String_TextAlignmentOptions_0(title, content, leftBtnText, rightBtnText, leftBtnAction, rightBtnAction);
        }

        public static void MainMenuBigAlert(string title, string content)
        {
            MenuEx.MMInstance.Method_Public_Void_String_String_PDM_0(title, content);
        }
        
        public enum KeyBoardType
        {
            Standard = EnumPublicSealedvaStNuSe4vUnique.Standard,
            Numeric = EnumPublicSealedvaStNuSe4vUnique.Numeric,
            Search = EnumPublicSealedvaStNuSe4vUnique.Search
        }

        public static void InputPopup(string Title, Action<string> EndString, Action<string> RealTimeString = null, Action OnClose = null, KeyBoardType keyBoardType = KeyBoardType.Standard, string Placeholder = "Enter Text", string OkButton = "OK", bool MultiLine = true, int CharLimit = 0)
        {
            if (_KeyboardComponent == null)
            {
                keyboardGameObject = new GameObject("ReMod.Core_KeyBoard");
                UnityEngine.Object.DontDestroyOnLoad(keyboardGameObject);
                _KeyboardComponent = keyboardGameObject.AddComponent<VRCInputField>();
            }

            try
            {
                KeyboardData One = new();
                KeyboardData Two = One.Method_Public_KeyboardData_String_String_String_String_String_0(Title, Placeholder, "3", OkButton);
                KeyboardData Three = Two.Method_Public_KeyboardData_Action_1_String_Action_1_String_Action_Boolean_0(RealTimeString, EndString, OnClose, false);
                KeyboardData Four = Three.Method_Public_KeyboardData_EnumPublicSealedvaStNuSe4vUnique_Boolean_0((EnumPublicSealedvaStNuSe4vUnique) keyBoardType, true);
                KeyboardData Five = Four.Method_Public_KeyboardData_InputType_ContentType_Int32_Boolean_Boolean_InterfacePublicAbstractBoStVoAc1VoAcSt1BoUnique_0(TMP_InputField.InputType.Standard, TMP_InputField.ContentType.Standard, CharLimit, MultiLine, false, null);
                Five._isWorldKeyboard = true;
                _KeyboardComponent._keyboardData = Five;
                _KeyboardComponent.Method_Private_Void_0();
            }
            catch {}
        }
    }
}
