using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWaterParticle : MonoBehaviour
{
  [SerializeField]
  ParticleSystem ps;

  List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

  private void OnParticleCollision(GameObject other)
  {
    int eventNum = ps.GetCollisionEvents(other, collisionEvents);

    if (other.tag == "Player")
    {
      var mover = other.GetComponent<IPlayerMover>();

      for(int i = 0; i < eventNum; i++)
      {
        Vector3 force = collisionEvents[i].velocity * 0.5f;
        mover.AddOutsideForce(force);       
      }
    }
  }

  private void OnParticleTrigger()
  {
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

    for(int i = 0; i < numEnter; i++)
    {
      var particle = enter[i];
      particle.remainingLifetime = 0;
      enter[i] = particle;
    }

    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
  }
}
