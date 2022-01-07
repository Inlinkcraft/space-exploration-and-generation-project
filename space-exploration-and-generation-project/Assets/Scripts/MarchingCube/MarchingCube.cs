using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCube
{

    private float[,,] marchingGrid;
    private int marchingGridSideSize;
    private float resolutionGridStep;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    public MarchingCube(float[,,] marchingGrid, float resolutionGridStep, int marchingGridSideSize)
    {
        this.marchingGrid = marchingGrid;
        this.resolutionGridStep = resolutionGridStep;
        this.marchingGridSideSize = marchingGridSideSize;
    }

    public float[,,] getMarchingGrid()
    {
        return marchingGrid;
    }

    public void generate(float surfaceThresroll){

        List<Vector3> tempVertices = new List<Vector3>();
        List<int> tempTriangles = new List<int>();

        for (int y = 0; y < marchingGridSideSize - 1; y++)
        {
            for (int z = 0; z < marchingGridSideSize - 1; z++)
            {
                for (int x = 0; x < marchingGridSideSize - 1; x++)
                {

                    // Values
                    float point0V = marchingGrid[y, z, x];
                    float point1V = marchingGrid[y, z, x + 1];
                    float point2V = marchingGrid[y, z + 1, x + 1];
                    float point3V = marchingGrid[y, z + 1, x];
                    float point4V = marchingGrid[y + 1, z, x];
                    float point5V = marchingGrid[y + 1, z, x + 1];
                    float point6V = marchingGrid[y + 1, z + 1, x + 1];
                    float point7V = marchingGrid[y + 1, z + 1, x];

                    // Find the Index of the tries
                    int triIndex = 0;
                    if (point0V > surfaceThresroll)
                        triIndex |= 1;

                    if (point1V > surfaceThresroll)
                        triIndex |= 2;

                    if (point2V > surfaceThresroll)
                        triIndex |= 4;

                    if (point3V > surfaceThresroll)
                        triIndex |= 8;

                    if (point4V > surfaceThresroll)
                        triIndex |= 16;

                    if (point5V > surfaceThresroll)
                        triIndex |= 32;

                    if (point6V > surfaceThresroll)
                        triIndex |= 64;

                    if (point7V > surfaceThresroll)
                        triIndex |= 128;

                    // Get Array of tries
                    int[] triesIndex = MarchingTriTable.triTable[triIndex];

                    // points Vertex
                    Vector3 point0P = new Vector3((x) * resolutionGridStep, (y) * resolutionGridStep, (z) * resolutionGridStep);
                    Vector3 point1P = new Vector3((x+1) * resolutionGridStep, (y) * resolutionGridStep, (z) * resolutionGridStep);
                    Vector3 point2P = new Vector3((x+1) * resolutionGridStep, (y) * resolutionGridStep, (z+1) * resolutionGridStep);
                    Vector3 point3P = new Vector3((x) * resolutionGridStep, (y) * resolutionGridStep, (z+1) * resolutionGridStep);
                    Vector3 point4P = new Vector3((x) * resolutionGridStep, (y+1) * resolutionGridStep, (z) * resolutionGridStep);
                    Vector3 point5P = new Vector3((x+1) * resolutionGridStep, (y+1) * resolutionGridStep, (z) * resolutionGridStep);
                    Vector3 point6P = new Vector3((x+1) * resolutionGridStep, (y+1) * resolutionGridStep, (z+1) * resolutionGridStep);
                    Vector3 point7P = new Vector3((x) * resolutionGridStep, (y+1) * resolutionGridStep, (z+1) * resolutionGridStep);


                    // add Vectex
                    for (int i = 0; i < triesIndex.Length && triesIndex[i] != -1; i++)
                    {
                        Vector3 vertice = Vector3.zero;

                        switch (triesIndex[i])
                        {
                            case 0:
                                vertice = Vector3.Lerp(point0P, point1P, (surfaceThresroll - point0V) / (point1V - point0V));
                                break;

                            case 1:
                                vertice = Vector3.Lerp(point1P, point2P, (surfaceThresroll - point1V) / (point2V - point1V));
                                break;

                            case 2:
                                vertice = Vector3.Lerp(point2P, point3P, (surfaceThresroll - point2V) / (point3V - point2V));
                                break;

                            case 3:
                                vertice = Vector3.Lerp(point3P, point0P, (surfaceThresroll - point3V) / (point0V - point3V));
                                break;

                            case 4:
                                vertice = Vector3.Lerp(point4P, point5P, (surfaceThresroll - point4V) / (point5V - point4V));
                                break;

                            case 5:
                                vertice = Vector3.Lerp(point5P, point6P, (surfaceThresroll - point5V) / (point6V - point5V));
                                break;

                            case 6:
                                vertice = Vector3.Lerp(point6P, point7P, (surfaceThresroll - point6V) / (point7V - point6V));
                                break;

                            case 7:
                                vertice = Vector3.Lerp(point7P, point4P, (surfaceThresroll - point7V) / (point4V - point7V));
                                break;

                            case 8:
                                vertice = Vector3.Lerp(point0P, point4P, (surfaceThresroll - point0V) / (point4V - point0V));
                                break;

                            case 9:
                                vertice = Vector3.Lerp(point1P, point5P, (surfaceThresroll - point1V) / (point5V - point1V));
                                break;

                            case 10:
                                vertice = Vector3.Lerp(point2P, point6P, (surfaceThresroll - point2V) / (point6V - point2V));
                                break;

                            case 11:
                                vertice = Vector3.Lerp(point3P, point7P, (surfaceThresroll - point3V) / (point7V - point3V));
                                break;

                        }

                        if (tempVertices.Contains(vertice))
                        {
                            tempTriangles.Add(tempVertices.IndexOf(vertice));
                        }
                        else
                        {
                            tempTriangles.Add(tempVertices.Count);
                            tempVertices.Add(vertice);
                        }
                    }

                }
            }
        }

        vertices = tempVertices.ToArray();
        triangles = tempTriangles.ToArray();

        uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

    }

    public Vector3[] getVertices()
    {
        return vertices;
    }

    public int[] getTriangles()
    {
        return triangles;
    }

    public Vector2[] getUvs()
    {
        return uvs;
    }

}
