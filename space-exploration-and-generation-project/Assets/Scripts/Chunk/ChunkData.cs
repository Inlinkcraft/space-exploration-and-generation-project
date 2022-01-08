using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChunkInTerrainData
{
    public World chunkWorld;
    public GameObject chunkGameObject;

    public float resolution;
    public float size;

    public float[,,] points;
}

public struct ChunkOutTerrainData
{
    public GameObject chunkGameObject;

    public Vector3[] vertices;
    public int[] triangles;
}
