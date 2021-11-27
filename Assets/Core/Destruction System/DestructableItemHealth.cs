using UnityEngine;

public class DestructableItemHealth : Health
{
    private DestructableItem _destructableItem;

    private void Awake()
    {
        _destructableItem = GetComponent<DestructableItem>();
        _destructableItem.Damaged += Damage;
    }
}