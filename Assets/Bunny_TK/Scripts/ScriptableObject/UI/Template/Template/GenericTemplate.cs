using Bunny_TK.DataDriven.UI.Template;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GenericTemplate<T> : BaseTemplate
{
    public abstract void ApplyTemplate(T targetComponent);


    public override void UpdateAll()
    {
        foreach (var style in FindObjectsOfType<BaseStyle>().Where(s => s.BaseTemplate == this))
        {
            style.ApplyTemplate();
        }
    }

    public override void ToggleContinuosRefreshAll()
    {
        continuosRefreshAll = !continuosRefreshAll;
        foreach (var style in FindObjectsOfType<BaseStyle>()?.Where(s => s.BaseTemplate == this))
            style.continuosRefresh = continuosRefreshAll;
    }

    public override List<GameObject> FindReferencesInScene()
    {
        var temp = FindObjectsOfType<BaseStyle>()?.Where(s => s.BaseTemplate == this);

        return temp.Select(t => t.gameObject).ToList();

    }
}
public abstract class BaseTemplate : ScriptableObject
{
    [SerializeField]
    [TextArea(2, 5)]
    protected string description;
    public bool continuosRefreshAll { get; protected set;}

    public abstract void ToggleContinuosRefreshAll();
    public abstract void UpdateAll();
    public abstract void CopyFrom<T>(T other);

    public abstract List<GameObject> FindReferencesInScene();
}
