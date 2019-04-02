using UnityEngine;
using System.Collections;
using Unity.Entities;


public class CubeSystem1 : ComponentSystem
{
    private struct Filter
    {
        public Transform t;
        public CubeComponent cc;
    }

    protected override void OnUpdate()
    {
        float t = Time.deltaTime;
        foreach (var cube in GetEntities<Filter>())
        {
            cube.t.transform.Translate(0, t, 0);
            cube.t.localScale = new Vector3(1, Mathf.Sin(cube.cc.theta) * 5, 1);
        }
    }
}

public class CubeSystem2 : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentArray<CubeComponent> components;
    }

    [Inject] private Data data;

    protected override void OnUpdate()
    {
        float t = Time.deltaTime;
        for (int i = 0; i < data.Length; i ++)
        {
            data.components[i].theta += t;
        }
    }
}

