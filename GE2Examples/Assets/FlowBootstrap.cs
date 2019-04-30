using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class FlowBootstrap : MonoBehaviour
{
    public Mesh mesh;
    public Material material;

    private RenderMesh renderMesh;
    private EntityManager entityManager;
    private EntityArchetype flowArchitype;

    public float speed = 1.0f;

    public int radius = 500;

    public float noiseScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        entityManager = World.Active.GetOrCreateManager<EntityManager>();

        flowArchitype = entityManager.CreateArchetype(typeof(Position), typeof(Rotation), typeof(Scale), typeof(Flow));

        renderMesh = new RenderMesh();
        renderMesh.mesh = mesh;
        renderMesh.material = material;

        for(int row = -radius; row < radius; row ++)
        {
            for (int col = -radius; col < radius; col ++)
            {
                Entity entity = entityManager.CreateEntity(flowArchitype);
                Position p = new Position();
                p.Value = new Vector3(row, 0, col);
                entityManager.SetComponentData(entity, p);

                Rotation r = new Rotation();
                r.Value = Quaternion.identity;
                entityManager.SetComponentData(entity, r);

                Scale s = new Scale();
                s.Value = new Vector3(0.2f, 1, 1);
                entityManager.SetComponentData(entity, s);

                entityManager.SetComponentData(entity, new Flow());

                entityManager.AddSharedComponentData(entity, renderMesh);
            }
        }
    }
}

public struct Flow:IComponentData
{
    int Value;
}

public struct FlowJob : IJobProcessComponentData<Position, Rotation, Scale, Flow>
{
    public float noiseScale;
    public float dt;
    public float offset;
    public void Execute(ref Position p, ref Rotation r, ref Scale s, ref Flow c3)
    {
        //r.Value = Quaternion.AngleAxis(Mathf.PerlinNoise((p.Value.x + offset) * noiseScale, (p.Value.z + offset) * noiseScale) * 360, Vector3.up);
        s.Value = new Vector3(0.2f, Mathf.PerlinNoise((p.Value.x + offset) * noiseScale, (p.Value.z + offset) * noiseScale) * 10, 0.2f);
    }
}

public class FlowSystem : JobComponentSystem
{
    FlowBootstrap fb;
    float offset;
    protected override void OnCreateManager()
    {
        base.OnCreateManager();
        fb = GameObject.FindObjectOfType<FlowBootstrap>();
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        var fj = new FlowJob()
        {
            dt = Time.deltaTime
            , noiseScale = fb.noiseScale
            , offset = this.offset
        };

        offset += Time.deltaTime * fb.speed;
        return fj.Schedule(this, inputDeps);
    }
}
