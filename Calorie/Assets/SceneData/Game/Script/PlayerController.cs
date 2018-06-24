using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    IPlayerMover mover;
    [SerializeField] private NormalShot shotPrefab;

    private void Start()
    {
        mover = GetComponent<IPlayerMover>();
    }

    public void Move(Vector3 vector)
    {
        mover.Move(vector, 2);
    }

    public void Shot(Transform parent)
    {
        var shot = Instantiate(shotPrefab, parent);
        shot.transform.localPosition = this.transform.localPosition + new Vector3(0f, 1f, 0f);
        shot.Init(this.transform.forward, this.gameObject.name);
    }
}