using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Material worldMaterial;

    private WorldOptions worldOptions;
    private MassManager massManager;

    private List<Chunk> chunks;


    void Start()
    {

        worldOptions = new WorldOptions(
            0.5f,   // Surface thresroll
            15f,    // Chunck size
            15,     // Resolution
            0.0001f, // Gravity
            0.02f  // Density
            );

        massManager = new MassManager(worldOptions);

        chunks = new List<Chunk>();

        Chunk chunk = Chunk.createChunk(Vector3.zero, gameObject);
        chunk.init(worldOptions, worldMaterial);
        

        for (int i = 0; i < 30; i++)
        {
            chunk.addMassPoint(new PointMass(new Vector3(Random.value * 15f, Random.value * 15f, Random.value * 15f)));
        }

        chunks.Add(chunk);

        chunk.load();

    }

    void Update()
    {
        foreach (Chunk chunk in chunks)
        {
            massManager.movePointMassesInChunk(chunk);
            chunk.load();
        }
    }

    private void OnDrawGizmos()
    {
        if (chunks == null)
            return;

        Gizmos.color = Color.red;
        foreach (Chunk chunk in chunks)
        {
            foreach (PointMass pointMass in chunk.getPointMasses())
            {
                Gizmos.DrawSphere(pointMass.getPosition(),0.1f);
            }
        }
    }

    public WorldOptions getWorldOptions()
    {
        return worldOptions;
    }

}
