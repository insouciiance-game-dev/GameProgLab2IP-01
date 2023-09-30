using System;
using UnityEngine;

public class ItemLooter : MonoBehaviour
{
    public event Action<GameObject> ItemLooted; 

    [field: SerializeField]
    public string LootTag { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(LootTag))
            return;

        ItemLooted?.Invoke(collision.gameObject);

        Destroy(collision.gameObject);
    }
}
