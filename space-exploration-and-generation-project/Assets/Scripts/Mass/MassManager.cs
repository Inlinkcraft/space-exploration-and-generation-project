using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassManager
{

    WorldOptions worldOptions;

    public MassManager(WorldOptions worldOptions)
    {
        this.worldOptions = worldOptions;
    }

    /*
    public void movePointMassesInChunk(Chunk chunk)
    {
        List<PointMass> pointMasses = chunk.getPointMasses();

        Dictionary<float, Vector3> gravities = new Dictionary<float, Vector3>();
        Dictionary<float, Vector3> densities = new Dictionary<float, Vector3>();

        foreach (PointMass currPointMass in pointMasses)
        {

            Vector3 velocity = currPointMass.getVelocity();
            Vector3 totalGravity = Vector3.zero;
            Vector3 totalDensity = Vector3.zero;

            foreach (PointMass otherPointMass in pointMasses)
            {
                if (currPointMass == otherPointMass)
                    continue;

                float key = (currPointMass.GetHashCode() * otherPointMass.GetHashCode()) / 100f;

                if (gravities.ContainsKey(key))
                {
                    totalGravity += -gravities[key];
                    totalDensity += -densities[key];
                }
                else
                {
                    Vector3 gravity = gravityForce(currPointMass, otherPointMass);
                    Vector3 density = densityForce(currPointMass, otherPointMass);
                    gravities.Add(key, gravity);
                    densities.Add(key, density);
                    totalGravity += gravity;
                    totalDensity += density;
                }

                //totalDensity += densityForce(currPointMass, gravities[key]);

            }

            Vector3 resultingForce = totalGravity + totalDensity;
            currPointMass.setVelocity(velocity + resultingForce/currPointMass.getMass());

            currPointMass.move();

        }

    }
    */
    private Vector3 gravityForce(PointMass currPointMass, PointMass otherPointMass)
    {

        Vector3 pointsVector = currPointMass.getPosition() - otherPointMass.getPosition();

        float distSqr = Vector3.SqrMagnitude(pointsVector);

        float force = worldOptions.getGravityForceConstant() * (currPointMass.getMass() * otherPointMass.getMass()/distSqr);

        return -pointsVector.normalized * force;
    }

    /*
    private Vector3 densityForce(PointMass currPointMass, Vector3 gravity)
    {
        return -currPointMass.getDensity() * worldOptions.getDensityForceConstant() * gravity; 
    }
    */
    
    private Vector3 densityForce(PointMass currPointMass, PointMass otherPointMass)
    {
        //Vector3 pointsVector = currPointMass.getPosition() - otherPointMass.getPosition();

        //float distSqr = Mathf.Pow(Vector3.SqrMagnitude(pointsVector),2);

        //float force = worldOptions.getGravityForceConstant() * (currPointMass.getDensity() * otherPointMass.getDensity() / distSqr);

        return Vector3.zero; //pointsVector.normalized * force;
    }
    
}
