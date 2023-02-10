using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ammo : MonoBehaviour
{
    [SerializeField] private float force;

    private void Start()
    {
        var collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Kill();

            var rigid = enemy.gameObject.GetOrAddComponent<Rigidbody>();
            rigid.AddForce(Vector3.down * force, ForceMode.Impulse);
        }
    }
}
