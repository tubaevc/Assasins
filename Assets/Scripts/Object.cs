using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
  private float objectHealth = 120f;

  public void ObjectHitDamage(float amount)
  {
    objectHealth -= amount;
    if (objectHealth <= 0f)
    {
      DestroyObject();
    }
  }

  private void DestroyObject()
  {
    Destroy(gameObject);
  }
}
