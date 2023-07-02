using ReMod.Core.VRChat;
using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Utilities;

namespace ReMod.Core.UI.MainMenu;

//haven't had the time to test this one so it may or may not work
//also this is incomplete
public class ReMMSlider : ReMMSectionElement
{
    private static GameObject _sliderprefab;
    private static GameObject SliderPrefab
    {
        get
        {
            if (_sliderprefab == null)
            {
                _sliderprefab = MenuEx.MMenuParent.Find("Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/EarmuffMode/Settings_Panel_1/VerticalLayoutGroup/FalloffForwardShift").gameObject;
            }

            return _sliderprefab;
        }
    }

    private TextMeshProUGUIEx _textComponent;

    public string Text
    {
        get => _textComponent.text;
        set => _textComponent.text = value;
    }

    private SnapSliderExtendedCallbacks _sliderComponent;

    public ReMMSlider(string title, string tooltip, float min, float max, Action<float> onSlide, Transform parent = null, bool separator = true) : base(SliderPrefab, parent, tooltip, false, separator)
    {
        _textComponent = gameObject.transform.Find("LeftItemContainer/Title").GetComponent<TextMeshProUGUIEx>();
        Text = title;

        _sliderComponent = gameObject.transform.Find("RightItemContainer/Slider").GetComponent<SnapSliderExtendedCallbacks>();
        _sliderComponent.onValueChanged = new Slider.SliderEvent();
        _sliderComponent.onValueChanged.AddListener(new Action<float>(onSlide));
        _sliderComponent.minValue = min;
        _sliderComponent.maxValue = max;
    }
}
