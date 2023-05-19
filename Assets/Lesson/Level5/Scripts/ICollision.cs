using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollision
{
    void CollisionEnter(List<GameObject> otherObjects);
}
