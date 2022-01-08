using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SurfaceGenerator
{

    private float surfaceThresroll;
    private int resolutionGridSize;
    private float resolutionGridStep;

    public SurfaceGenerator(float surfaceThresroll, int resolutionGridSize, float resolutionGridStep)
    {
        this.surfaceThresroll = surfaceThresroll;
        this.resolutionGridSize = resolutionGridSize;
        this.resolutionGridStep = resolutionGridStep;
    }

    public Mesh getSurfaceForChunk(Chunk chunk, WorldOptions worldOptions)
    {
        // Create a Marching grid
        float[,,] grid = new float[resolutionGridSize, resolutionGridSize, resolutionGridSize];

        // Insert all mass points in the grid
        //FloatMass2Grid.generateChunkGrid(chunk, grid, worldOptions);

        // Create MarchingCube object
        MarchingCube marchingCube = new MarchingCube(grid, resolutionGridStep, resolutionGridSize);
        marchingCube.generate(surfaceThresroll);

        // Ask a list of vertex
        Vector3[] vertices = marchingCube.getVertices();

        // Ask a list of tri
        int[] triangles = marchingCube.getTriangles();

        Vector2[] uvs = marchingCube.getUvs();

        // Make mesh
        Mesh mesh = new Mesh();
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        return mesh;
    }

}

