using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

    private bool isLoaded;

    private SurfaceGenerator surfaceGenerator;

    private Mesh surface;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private List<PointMass> pointMasses;

    private WorldOptions worldOptions;

    public static Chunk createChunk(Vector3 pos, GameObject parent)
    {
        GameObject chunk = new GameObject();
        chunk.transform.position = pos;
        chunk.transform.name = "Chunk: " + pos + "";
        chunk.transform.parent = parent.transform;

        Chunk chunkComponent = chunk.AddComponent<Chunk>();

        return chunkComponent;
    }
    public void init(WorldOptions worldOptions, Material worldMaterial)
    {
        isLoaded = false;

        this.worldOptions = worldOptions;

        float surfaceThresroll = worldOptions.getSurfaceThresroll();
        float resolutionStepSize = worldOptions.getChunkSize() / worldOptions.getTerrainResolution();
        int resolutionGridSize = worldOptions.getTerrainResolution() + 1;

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = worldMaterial;

        pointMasses = new List<PointMass>();
        surfaceGenerator = new SurfaceGenerator(surfaceThresroll, resolutionGridSize, resolutionStepSize);
    }

    public List<PointMass> getPointMasses()
    {
        return pointMasses;
    }

    public void addMassPoint(PointMass massPoint)
    {
        pointMasses.Add(massPoint);
    }
    public void removeMassPoint(PointMass massPoint)
    {
        pointMasses.Remove(massPoint);
    }

    public bool getIsLoaded()
    {
        return isLoaded;
    }

    public void load()
    {
        isLoaded = true;

        // Generate Surface
        surface = surfaceGenerator.getSurfaceForChunk(this, worldOptions);
        meshRenderer.enabled = true;
        meshFilter.mesh = surface;
    }

    public void unload()
    {
        isLoaded = false;

        meshFilter.mesh = null;
        meshRenderer.enabled = false;
        surface = null;
    }

}
