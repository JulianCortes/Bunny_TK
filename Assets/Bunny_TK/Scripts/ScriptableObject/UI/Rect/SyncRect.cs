using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRect : MonoBehaviour
{
    public RectTransform original;


    RectTransform target;


    private void Update()
    {
        if (target == null)
            target = GetComponent<RectTransform>();

        target.anchorMin = original.anchorMin;
        target.anchorMax = original.anchorMax;
        target.anchoredPosition = original.anchoredPosition;
        target.sizeDelta = original.sizeDelta;
        target.offsetMax = original.offsetMax;
        target.offsetMin = original.offsetMin;
        target.pivot = original.pivot;
    }

}
