using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    private List<Renderer> renderList = null;

    private float cutOffVal = 0;

    private bool isDisappeared = false;
    private bool isDisappearing = false;

    private void Awake()
    {
        renderList = SelectDissolveRenders();
    }

    private void Update()
    {
        if (isDisappearing && cutOffVal < 1)
        {
            cutOffVal = Vector2.MoveTowards(new Vector2(cutOffVal, 0), Vector2.right, Time.deltaTime).x;

        }else if (!isDisappearing && cutOffVal > 0)
        {
            cutOffVal = Vector2.MoveTowards(new Vector2(cutOffVal, 0), Vector2.zero, Time.deltaTime).x;
        }

        if (cutOffVal == 1)
            isDisappeared = true;
        else
            isDisappeared = false;

        foreach (Renderer r in renderList)
        {
            r.material.SetFloat("_Cutoff", cutOffVal);
        }
    }

    public List<Renderer> SelectDissolveRenders()
    {
        return SelectRenderers(gameObject);
    }

    private List<Renderer> SelectRenderers(GameObject obj)
    {

        if (obj.transform.childCount != 0)
        {
            List<Renderer> n = new List<Renderer>();

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                try
                {
                    n.AddRange(SelectRenderers(obj.transform.GetChild(i).gameObject));
                } catch (Exception) { }
            }

            if (obj.GetComponent<Renderer>())
                n.Add(obj.GetComponent<Renderer>());

            return n;

        }
        else
        {
            if (obj.GetComponent<Renderer>())
            {
                List<Renderer> r = new List<Renderer>();
                r.Add(obj.GetComponent<Renderer>());
                return r;
            }
        }

        return null;

    }

    public void Dissappear() => isDisappearing = true;

    public void Appear() => isDisappearing = false;

    public bool IsDisappeared()
    {
        return isDisappeared;
    }
}
