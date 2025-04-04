using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceDisapear : MonoBehaviour
{
    MeshRenderer render;
    SkinnedMeshRenderer skinnedRender;
    bool faded;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        if (!render)
        {
            skinnedRender = GetComponent<SkinnedMeshRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Floor(Time.timeSinceLevelLoad * 10) % 2 == 0 && !faded)
        {
            if (render)
            {
                render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, render.material.color.a - 0.01f);


                if (render.material.color.a <= 0.1f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                skinnedRender.material.color = new Color(skinnedRender.material.color.r, skinnedRender.material.color.g, skinnedRender.material.color.b, skinnedRender.material.color.a - 0.01f);

                if (skinnedRender.material.color.a <= 0.1f)
                {
                    Destroy(gameObject);
                }
            }
            faded = true;
        }
        else if (Mathf.Floor(Time.timeSinceLevelLoad * 10) % 2 != 0)
        {
            faded = false;
        }
    }
}
