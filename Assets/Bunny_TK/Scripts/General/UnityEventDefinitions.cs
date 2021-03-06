﻿using UnityEngine;
using UnityEngine.Events;

namespace Bunny_TK
{
    [System.Serializable]
    public class UnityEventInt : UnityEvent<int> { }

    [System.Serializable]
    public class UnityEventFloat : UnityEvent<float> { }

    [System.Serializable]
    public class UnityEventBool : UnityEvent<bool> { }

    [System.Serializable]
    public class UnityEventString : UnityEvent<string> { }

    [System.Serializable]
    public class UnityEventGameObject : UnityEvent<GameObject> { }

    [System.Serializable]
    public class UnityEventVector3 : UnityEvent<Vector3> { }

    [System.Serializable]
    public class UnityEventQuaternion : UnityEvent<Quaternion> { }
}