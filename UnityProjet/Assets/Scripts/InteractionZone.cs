using UnityEngine;
public enum ZoneType { Desk, CoffeeMachine }

public class InteractionZone : MonoBehaviour
{
    [SerializeField] public ZoneType ZoneType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ExplorationManager.Instance?.OnPlayerEnterZone(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ExplorationManager.Instance?.OnPlayerExitZone(this);
    }
}
