using UnityEngine;
public enum ZoneType { Desk, CoffeeMachine }

public class InteractionZone : MonoBehaviour
{
    [SerializeField] public ZoneType ZoneType; // Type de zone interactible
    [SerializeField] public GameObject PopUp; // Popup appuyer sur E

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExplorationManager.Instance?.OnPlayerEnterZone(this);
            PopUp.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExplorationManager.Instance?.OnPlayerExitZone(this);
            PopUp.SetActive(false);
        }
            
    }
}
