using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldOptions
{

    private float surfaceThresroll;
    private float chunkSize;
    private int terrainResolution;

    private float gravityForceConstant;
    private float densityForceConstant;

    public WorldOptions(float surfaceThresroll, float chunkSize, int terrainResolution, float gravityForceConstant, float densityForceConstant)
    {
        if(validateChunkSize(chunkSize) && validateTerrainResolution(terrainResolution) && validateSurfaceThresroll(surfaceThresroll) && validateGravityForceConstant(gravityForceConstant) && validateDensityForceConstant(densityForceConstant))
        {
            this.chunkSize = chunkSize;
            this.terrainResolution = terrainResolution;

            this.gravityForceConstant = gravityForceConstant;
            this.densityForceConstant = densityForceConstant;

            this.surfaceThresroll = surfaceThresroll;
        }
        else
        {
            throw new WorldOptionException("Wolrds Options are invalid");
        }
        
    }

    public int getTerrainResolution()
    {
        return terrainResolution;
    }

    public float getChunkSize()
    {
        return chunkSize;
    }

    public float getSurfaceThresroll()
    {
        return surfaceThresroll;
    }

    public float getGravityForceConstant()
    {
        return gravityForceConstant;
    }

    public float getDensityForceConstant()
    {
        return densityForceConstant;
    }

    private bool validateTerrainResolution(int terrainResolution)
    {
        return terrainResolution > 0;
    }

    private bool validateChunkSize(float chunkSize)
    {
        return chunkSize > 0;
    }

    private bool validateSurfaceThresroll(float surfaceThresroll)
    {
        return surfaceThresroll > 0 && surfaceThresroll < 1;
    }

    private bool validateDensityForceConstant(float densityForceConstant)
    {
        return densityForceConstant > 0;
    }

    private bool validateGravityForceConstant(float gravityForceConstant)
    {
        return gravityForceConstant > 0;
    }
}
