using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMass
{

    private Vector3 position;
    private Vector3 velocity;

    private float mass;
    private float density;

    public PointMass() : this(Vector3.zero) { }

    public PointMass(Vector3 position) : this(position, 10f, 1.44269504089f) { }

    public PointMass(Vector3 position, float mass, float density):this(position, Vector3.zero, mass, density){}

    public PointMass(Vector3 position, Vector3 velocity, float mass, float density)
    {
        this.position = position;
        this.velocity = velocity;

        this.mass = mass;
        this.density = density;
    }

    public void setPosition(Vector3 position)
    {
        this.position = position;
    }

    public void setVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public Vector3 getPosition()
    {
        return position;
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }

    public float getMass()
    {
        return mass;
    }

    public float getDensity()
    {
        return density;
    }

    public void move()
    {
        position += velocity * Time.deltaTime;
    }
}
