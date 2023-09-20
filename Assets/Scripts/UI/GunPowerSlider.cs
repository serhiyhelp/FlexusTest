using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunPowerSlider : MonoBehaviour
{
    [SerializeField] private Cannon   _cannon;
    [SerializeField] private Slider   _slider;
    [SerializeField] private TMP_Text _powerLabel;

    [Space]
    [SerializeField] private float _min = 1;
    [SerializeField] private float _max = 10;

    private void Start()
    {
        _slider.minValue = _min;
        _slider.maxValue = _max;
        _slider.value    = _cannon.GunPower;
        _slider.onValueChanged.AddListener(OnValueChanged);
        
        _powerLabel.text = ((int)_slider.value).ToString();
    }

    private void OnValueChanged(float val)
    {
        _cannon.GunPower = _slider.value;
        _powerLabel.text = ((int)_slider.value).ToString();
    }
    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    }
}