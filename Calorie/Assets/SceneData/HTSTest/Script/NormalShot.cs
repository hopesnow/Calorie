using UnityEngine;

public class NormalShot : MonoBehaviour
{
    private Vector3 vec;

    public void Init(Vector3 vec)
    {
        this.vec = vec.normalized;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // collision.gameObject.GetComponent
    }
}
