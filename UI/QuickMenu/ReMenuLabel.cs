using ReMod.Core.VRChat;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuLabel : UiElement
	{
		private static GameObject _buttonPrefab;
		private static GameObject ButtonPrefab
		{
			get
            {
                if (_buttonPrefab == null)
                {
                    _buttonPrefab = MenuEx.Instance.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_Debug_Row_1/Button_FPS").gameObject;
                }
                return _buttonPrefab;
            }
		}
		public void SetSubtitle(string text, string color = "#ffffff")
		{
			TextMeshProUGUI component = base.RectTransform.Find("Text_H4").GetComponent<TextMeshProUGUI>();
			component.richText = true;
			component.text = "<color="+color+">"+text+"</color>";
		}

		public void SetText(string text, int fontsize = 46, string color = "#ffffff")
		{
			TextMeshProUGUI componentInChildren = base.RectTransform.Find("Text_H1").GetComponentInChildren<TextMeshProUGUI>();
			componentInChildren.text = "<color="+color+">"+text+"</color>";
			componentInChildren.fontSize = fontsize;
			componentInChildren.richText = true;
		}
		public ReMenuLabel(Transform transform, string text, string subtitleText, int fontsize = 46, string color = "#ffffff")  : base(ButtonPrefab, transform, "Label_" + subtitleText, true)
		{
			Object.DestroyImmediate(base.RectTransform.Find("Text_H1").GetComponent<TextBinding>());
			this.SetText(text, fontsize, color);
			this.SetSubtitle(subtitleText, color);
		}
	}
}
