using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Chunk
{

    private bool isLoaded;

    private Thread thread;
    private Queue<ChunkInTerrainData> chunkDataQueue;

    private ComputeShader terrainShader;

    // This initialise the param of the chunk
    public void init(ComputeShader terrainShader){
        this.isLoaded = false;
        
        this.terrainShader = terrainShader;
        this.chunkDataQueue = new Queue<ChunkInTerrainData>();
    }

    // Starts the chunk tread
    public void load(){
        if(!isLoaded){
            isLoaded = true;
            thread = new Thread(new ThreadStart(work));
            thread.Start();
        }else{
            Debug.LogWarning("You shouldn't load a chunk that is already loaded");
        }
    }

    // Change the value in the given chunk
    public void update(ChunkInTerrainData chunkData){
        if(isLoaded){
            chunkDataQueue.Enqueue(chunkData);
        }else{
            Debug.LogWarning("You shouldn't update a chunk that is not loaded");
        }
        
    }

    // Stop the chunk tread
    public void unLoad(){
        if(isLoaded){
            isLoaded = false;
            //thread.Join();
        }else{
            Debug.LogWarning("You shouldn't unload a chunk that is not loaded");
        }
        
    }


    public static Chunk createChunk(Vector3 position, World world){

        Chunk chunk = new Chunk();
        chunk.init(world.terrainShader);

        chunk.load();

        ChunkInTerrainData chunkData = new ChunkInTerrainData();

        chunkData.chunkWorld = world;
        
        chunkData.chunkGameObject = new GameObject();

        chunkData.chunkGameObject.name = "Chunk";

        chunkData.chunkGameObject.transform.parent = world.gameObject.transform;

        chunkData.chunkGameObject.transform.position = position;

        chunkData.chunkGameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = chunkData.chunkGameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = world.terrainMaterial;

        chunk.update(chunkData);

        return chunk;
    }

    // The Chunk workings
    private void work(){
        while(isLoaded || chunkDataQueue.Count > 0){

            // If the chunk does not need to be updated then wait
            if(chunkDataQueue.Count == 0){
                Thread.Sleep(0);
            }else{

                ChunkInTerrainData currChunkData = chunkDataQueue.Dequeue();

                // TODO : GENERATE SURFACE


                ChunkOutTerrainData outData = new ChunkOutTerrainData();
                outData.chunkGameObject = currChunkData.chunkGameObject;
                outData.vertices = new Vector3[] {new Vector3(0,0,0), new Vector3(1,0,0), new Vector3(0,0,1)};
                outData.triangles = new int[]{0,1,2};

                currChunkData.chunkWorld.terrainUpdate(outData);

            }

        }
    }

}
