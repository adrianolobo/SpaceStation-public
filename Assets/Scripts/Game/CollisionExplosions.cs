using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExplosions : Singleton<CollisionExplosions>
{
    public GameObject ExplosionParticles;

    public void explode(Vector3 position)
    {
        Instantiate(ExplosionParticles, position, Quaternion.identity);
    }
}
