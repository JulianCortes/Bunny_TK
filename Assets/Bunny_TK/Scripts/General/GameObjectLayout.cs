using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class GameObjectLayout : MonoBehaviour
{
    public float distance = .01f;
    public GameObject prefab;
    public bool align = true;
    private List<Transform> items;
    private float xStart;
    private bool pause = false;
    private void Start()
    {
        if (pause) return;
        items = GetComponentsInChildren<Transform>(true).ToList();
        items.Remove(this.transform);
        items.RemoveAll(t => t.parent != this.transform);

        align = false;

    }

    private void Update()
    {
        if (!align) return;
        if (Application.isEditor && !Application.isPlaying)
        {
            items = GetComponentsInChildren<Transform>(true).ToList();
            items.Remove(this.transform);
            items.RemoveAll(t => t.parent != this.transform);
            xStart = -distance * (items.Count - 2) / 2;
            xStart -= distance / 2f;

            for (int i = 0; i < items.Count; i++)
            {
                Vector3 targetPos = Vector3.zero;
                targetPos.x = xStart;
                xStart += distance;

                items[i].localPosition = targetPos;
            }
        }
    }

    [MethodButton]
    public void SubstituteChildren()
    {
        pause = true;
        items = GetComponentsInChildren<Transform>(true).ToList();
        items.Remove(this.transform);
        items.RemoveAll(t => t.parent != this.transform);

        for (int i = 0; i < items.Count; i++)
        {
            GameObject newObject = Instantiate(prefab, transform);
            newObject.transform.position = items[i].transform.position;
            newObject.transform.rotation = items[i].transform.rotation;
            newObject.SetActive(items[i].gameObject.activeInHierarchy);
            DestroyImmediate(items[i].gameObject);
        }
    }

}
