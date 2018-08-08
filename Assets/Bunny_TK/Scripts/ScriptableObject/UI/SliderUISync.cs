using Bunny_TK.DataDriven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Slider))]
public class SliderUISync : MonoBehaviour
{
    public FloatVariable value;
    public FloatVariable minValue;
    public FloatVariable maxValue;
    public bool lockValueWhenPressed = true;

    [FormerlySerializedAs("OnPause")]
    public UnityEvent OnLocked;
    [FormerlySerializedAs("OnResume")]
    public UnityEvent OnUnlocked;
    public  Slider slider;

    private UIEventLauncher handleEventLauncher;
    private bool isLocked = false;
    private float lockedValue;

    void OnValidate()
    {
        slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
        if (lockValueWhenPressed)
        {
            if (handleEventLauncher == null)
            {
                handleEventLauncher = slider.GetComponent<UIEventLauncher>();
                if (handleEventLauncher == null)
                    handleEventLauncher = slider.gameObject.AddComponent<UIEventLauncher>();
            }

            handleEventLauncher.OnPointerDown += EventLauncher_OnPointerDown;
            handleEventLauncher.OnPointerUp += EventLauncher_OnPointerUp; ;
        }
        slider.onValueChanged.AddListener(OnValueChanged);
    }
    void OnDisable()
    {
        if (handleEventLauncher != null)
        {
            handleEventLauncher.OnPointerDown -= EventLauncher_OnPointerDown;
            handleEventLauncher.OnPointerUp -= EventLauncher_OnPointerUp;
        }

        slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    void Update()
    {
        if (slider == null) return;
        if (value == null) return;


        if (minValue != null)
            slider.minValue = minValue;
        if (maxValue != null)
            slider.maxValue = maxValue;

        if (isLocked)
        {
            value.runtimeValue = lockedValue = slider.value;
        }
        else
        {
            slider.value = value.runtimeValue;
        }
    }

    private void OnValueChanged(float value)
    {
        if (slider == null) return;
        if (this.value == null) return;
        this.value.runtimeValue = value;
    }

    private void EventLauncher_OnPointerUp(UIEventLauncher obj)
    {
        isLocked = false;
        if (lockValueWhenPressed)
        {
            OnUnlocked.Invoke();
            slider.value = lockedValue;
        }
    }

    private void EventLauncher_OnPointerDown(UIEventLauncher obj)
    {
        if (lockValueWhenPressed)
        {
            isLocked = true;
            lockedValue = slider.value;
            OnLocked.Invoke();
        }
    }
}
