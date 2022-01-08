using System.Collections.Generic;
using UnityEngine;

public class FloatMass2Grid
{

    public static readonly float MASS_MINIMUM = 0.02f;

    /*
    public static void generateChunkGrid(Chunk chunk, float[,,] grid, WorldOptions worldOptions)
    {

        List<PointMass> pointMasses = chunk.getPointMasses();
        float gridSpaceSize = worldOptions.getChunkSize() / worldOptions.getTerrainResolution();

        foreach(PointMass pointMass in pointMasses)
        {

            // Get the position of the vertice from the origin of the chunk
            Vector3 relative2ChunkPos = pointMass.getPosition() - chunk.transform.position;

            if (relative2ChunkPos.x > worldOptions.getChunkSize() || relative2ChunkPos.y > worldOptions.getChunkSize() || relative2ChunkPos.z > worldOptions.getChunkSize() || relative2ChunkPos.x < 0 || relative2ChunkPos.y < 0 || relative2ChunkPos.z < 0)
                continue;

            // Get min and max ids for distance calculation
            int minX = Mathf.FloorToInt(relative2ChunkPos.x / gridSpaceSize);
            int minY = Mathf.FloorToInt(relative2ChunkPos.y / gridSpaceSize);
            int minZ = Mathf.FloorToInt(relative2ChunkPos.z / gridSpaceSize);

            int maxX = Mathf.CeilToInt(relative2ChunkPos.x / gridSpaceSize);
            int maxY = Mathf.CeilToInt(relative2ChunkPos.y / gridSpaceSize);
            int maxZ = Mathf.CeilToInt(relative2ChunkPos.z / gridSpaceSize);

            int[] minMax = new int[] { minX, minY, minZ, maxX, maxY, maxZ };

            // distribute
            distribute(pointMass, gridSpaceSize, worldOptions.getChunkSize(), pointMass.getMass(), grid, minMax, null);

        }
    }
    */
    private static void distribute(PointMass pointMass, float gridSpaceSize, float chunkSize, float currMass, float[,,] grid, int[] currMinMax, int[] preMinMax, int iteration = 0)
    {
        SortedList<float, int[]> gridVerticesDistances = new SortedList<float, int[]>();

        // Get distance and put it in the sorted list
        for (int x = currMinMax[0]; x < currMinMax[3]; x++)
        {
            for (int y = currMinMax[1]; y < currMinMax[4]; y++)
            {
                for (int z = currMinMax[2]; z < currMinMax[5]; z++)
                {

                    if(preMinMax != null)
                    {
                        
                        if ((preMinMax[0] >= x && x <= preMinMax[3]) || (preMinMax[1] >= y && y <= preMinMax[4]) || (preMinMax[2] >= z && z <= preMinMax[5]))
                        {
                            continue;
                        }
                        
                    }

                    // Calculate Distance
                    float dist = Vector3.SqrMagnitude(pointMass.getPosition() - new Vector3(x * gridSpaceSize, y * gridSpaceSize, z * gridSpaceSize));
                    int[] ids = new int[]{ y, z, x };

                    if (!gridVerticesDistances.ContainsKey(dist))
                    {
                        gridVerticesDistances.Add(dist, ids);
                    }

                }
            }
        }

        // Set amount In grid
        for(int i = 0; i < gridVerticesDistances.Keys.Count && currMass > MASS_MINIMUM; i++)
        {
            float decayRatio = massDecayRatio(pointMass.getDensity(), currMass);
            float mass2Lost = decayRatio * currMass;

            int[] ids = gridVerticesDistances[gridVerticesDistances.Keys[i]];

            if(0 <= ids[0] && ids[0] < chunkSize && 0 <= ids[1] && ids[1] < chunkSize && 0 <= ids[2] && ids[2] < chunkSize)
            {
                float spaceLeft = 1 - grid[ids[0], ids[1], ids[2]];

                if (mass2Lost < spaceLeft)
                {
                    grid[ids[0], ids[1], ids[2]] += mass2Lost;
                    currMass -= mass2Lost;
                }
                else
                {
                    grid[ids[0], ids[1], ids[2]] = 1;
                    currMass -= spaceLeft;
                }
            }
            else
            {
                // TODO : THIS IS AN OTHER CHUNK
            }

        }

        // If their some mass left
        if(currMass > MASS_MINIMUM && iteration <= 100)
        {
            int[] newMinMax = new int[6];

            newMinMax[0] = currMinMax[0] - 1;
            newMinMax[1] = currMinMax[1] - 1;
            newMinMax[2] = currMinMax[2] - 1;
            newMinMax[3] = currMinMax[3] + 1;
            newMinMax[4] = currMinMax[4] + 1;
            newMinMax[5] = currMinMax[5] + 1;

            iteration++;

            distribute(pointMass, gridSpaceSize, chunkSize, currMass, grid, newMinMax, currMinMax, iteration);
        }
    }

    private static float massDecayRatio (float denstity, float mass)
    {
        float value;

        if(mass <= 1)
        {
            value = Mathf.Exp((1 / denstity) * (-mass));
        }
        else
        {
            value = Mathf.Exp((1 / denstity) * (-1));
        }

        return value;
    }

}
