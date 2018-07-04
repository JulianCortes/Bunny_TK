using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;


[CustomEditor(typeof(UnityEngine.Object), true)]
[CanEditMultipleObjects]
public class MakeButtonEditor : Editor
{
    MethodInfo _onInspectorGuiMethod;
    MethodInfo _onSceneGuiMethod;
    List<MethodInfo> _buttonMethods = new List<MethodInfo>();

    public void OnEnable()
    {
        var type = target.GetType();
        //if (!typeof(IObjectInspectable).IsAssignableFrom(type))
        if (!typeof(MonoBehaviour).IsAssignableFrom(type))
            return;

        _onInspectorGuiMethod = target.GetType().GetMethod("OnInspectorGUI", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        _onSceneGuiMethod = target.GetType().GetMethod("OnSceneGUI", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        var meths = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.IsDefined(typeof(MethodButtonAttribute), false));
        foreach (var meth in meths)
        {
            _buttonMethods.Add(meth);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (_onInspectorGuiMethod != null)
        {
            foreach (var target in targets)
                _onInspectorGuiMethod.Invoke(target, new object[0]);
        }


        foreach (var meth in _buttonMethods)
        {
            if (GUILayout.Button(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Regex.Replace(meth.Name, "(\\B[A-Z])", " $1"))))
                foreach (var eachTarget in targets)
                    meth.Invoke(eachTarget, new object[0]);
        }
    }

    protected virtual void OnSceneGUI()
    {
        if (_onSceneGuiMethod != null)
            _onSceneGuiMethod.Invoke(target, new object[0]);
    }
}
