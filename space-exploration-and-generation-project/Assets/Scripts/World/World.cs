using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    public ComputeShader terrainShader;
    public Material terrainMaterial;
    
    private WorldOptions worldOptions;

    private MassManager massManager;

    private Queue<ChunkOutTerrainData> chunkToUpdate;

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

        chunkToUpdate = new Queue<ChunkOutTerrainData>();

        chunks = new List<Chunk>();

        massManager = new MassManager(worldOptions);

        // Test
        chunkToUpdate = new Queue<ChunkOutTerrainData>();
        Chunk chunk = Chunk.createChunk(Vector3.zero, this);
        chunks.Add(chunk);

    }

    void Update()
    {
        if(chunkToUpdate.Count > 0){
            updateChunkMesh(chunkToUpdate.Dequeue());
        }
    }

    void OnDestroy(){
        
        chunkToUpdate.Clear();
        foreach (Chunk chunk in chunks)
        {
            chunk.unLoad();
        }

    }

    public WorldOptions getWorldOptions()
    {
        return worldOptions;
    }

    public void terrainUpdate(ChunkOutTerrainData chunkData){
        chunkToUpdate.Enqueue(chunkData);
    }

    private void updateChunkMesh(ChunkOutTerrainData chunkData){
        
        Mesh mesh = new Mesh();
        mesh.Clear();

        mesh.vertices = chunkData.vertices;
        mesh.triangles = chunkData.triangles;

        mesh.RecalculateNormals();

        chunkData.chunkGameObject.GetComponent<MeshFilter>().mesh = mesh;

    }

}
