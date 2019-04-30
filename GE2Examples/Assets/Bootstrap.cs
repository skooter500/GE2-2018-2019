using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Jobs;

public struct Sway : IComponentData
{
    public float theta;
}

class A
{
    public float a;
}

struct B
{
    public float b;
}

public class Bootstrap : MonoBehaviour
{
    private EntityArchetype swayArchitype;

    private RenderMesh swayMesh;
    public Mesh mesh;
    public Material material;

    private EntityManager entityManager;
    
    // Start is called before the first frame update
    void Start()
    {


        entityManager = World.Active.GetOrCreateManager<EntityManager>();

        swayArchitype = entityManager.CreateArchetype(typeof(Position), typeof(Rotation), typeof(Sway));

        swayMesh = new RenderMesh();
        swayMesh.mesh = mesh;
        swayMesh.material = material;

        for (int row = -100; row < 100; row++)
        {
            for (int col = -100; col < 100; col++)
            {
                Entity swayEntity = entityManager.CreateEntity(swayArchitype);

                Position p = new Position();
                p.Value = new float3(col * 2, 0, row * 2);

                Rotation r = new Rotation();
                r.Value = new quaternion();

                entityManager.SetComponentData(swayEntity, p);
                entityManager.SetComponentData(swayEntity, r);

                entityManager.AddSharedComponentData(swayEntity, swayMesh);
                
            }
        }
    }
}

public struct SwayJob : IJobProcessComponentData<Position, Rotation, Sway>
{
    public float frequency;
    public float amplitude;
    public float timeDelta;
    public void Execute(ref Position p, ref Rotation r, ref Sway s)
    {
        r.Value = Quaternion.AngleAxis(Mathf.Sin(s.theta) * amplitude, Vector3.right);
        s.theta += Mathf.PI * 2.0f * frequency * timeDelta;
    }
}

public class SwaySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var sj = new SwayJob()
        {
            frequency = 0.5f
            , amplitude = 60
            , timeDelta = Time.deltaTime
        };

        return sj.Schedule(this, inputDeps);
    }
}
