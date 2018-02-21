using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbtionObject : MonoBehaviour
{
  [SerializeField]
  float radius;

  System.Action hitaction;
  public System.Action HitAction { set { hitaction = value; } }

  private void OnTriggerStay(Collider other)
  {
    Vector3 vel = transform.position - other.transform.position;
    other.attachedRigidbody.velocity = vel.normalized;

    if(IsHit(other.transform.position))
    {
      if(hitaction != null)
      {
        hitaction();
      }
      Destroy(other.gameObject);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    other.attachedRigidbody.velocity = Vector3.zero;
  }

  bool IsHit(Vector3 pos)
  {
    Vector3 thisPos = this.transform.position;

    float x = (pos.x - thisPos.x) * (pos.x - thisPos.x);
    float y = (pos.z - thisPos.z) * (pos.z - thisPos.z);

    return (x + y <= radius * radius);
  }
}
