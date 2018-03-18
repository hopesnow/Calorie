//************************************************
//SprayWaterParticle
//Author yt-hrd
//************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//************************************************
//SprayWaterParticle
//放水処理
//************************************************
public class SprayWaterParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private float dischargePower = 0.5f;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    bool isPlaying = false;

    //衝突時処理（プレイヤー処理)
    //プレイヤーのタグの場合は吹き飛ばす力を加算する
    //タグが直指定なので工夫必要かも
    private void OnParticleCollision(GameObject other)
    {
        int eventNum = ps.GetCollisionEvents(other, collisionEvents);

        if (other.tag == "Player")
        {
            var mover = other.GetComponent<IPlayerMover>();

            for (int i = 0; i < eventNum; i++)
            {
                Vector3 force = collisionEvents[i].velocity * dischargePower;
                mover.AddOutsideForce(force);
            }
        }
    }

    //反射とか反射しないとかがあるため
    //破棄したい場合はこっちで別途判定とって殺す
    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        for (int i = 0; i < numEnter; i++)
        {
            var particle = enter[i];
            particle.remainingLifetime = 0f;
            enter[i] = particle;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

    //発射したりしなかったり
    public void SetActiveEmitter(bool _isPlaying)
    {
        if (_isPlaying && !isPlaying)
        {
            isPlaying = true;
            ps.Play();
        }
        else if (!_isPlaying)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            isPlaying = false;
        }
    }

    //どのレイヤーと判定とるか
    //自分との判定排除　直書き多いので修正必須
    public void SetupCollision(int playerNo = 0)
    {
        string[] layerName = new string[5];

        int cnt = 0;
        for (int i = 0; i < 4; i++)
        {
            if (i == playerNo)
                continue;

            layerName[cnt] = "Player" + i.ToString();
            cnt++;
        }

        layerName[3] = "Block";
        layerName[4] = "ReflectionObject";

        var with = ps.collision;
        with.collidesWith = LayerMask.GetMask(layerName);
    }

    //何とトリガー判定するか、
    //ステージオブジェクト+playerになると思われ 
    public void SetupTrigger(Collider[] coliders)
    {
        for (int i = 0; i < coliders.Length; i++)
        {
            ps.trigger.SetCollider(i, coliders[i]);
        }
    }
}