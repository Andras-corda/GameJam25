using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère la logique de la scène d'exploration (vue top-down) :
/// Détection des zones d'interaction (bureau, machine à caffé)
/// Communication avec PlayerController pour bloquer le mouvement
/// Déclenchement du changement de scène
/// </summary>
public class ExplorationManager : MonoBehaviour
{
    public static ExplorationManager Instance { get; private set; }

    [Header("Références")]
    [SerializeField] private PlayerController playerController; // Script sur le GameObject Player

    [Header("Scène bureau")]
    [SerializeField] private string _deskSceneName = "DeskScene"; // Nom exact dans Build Settings

    private InteractionZone _nearZone = null;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Update()
    {
        CheckInteraction();
    }
    public void OnPlayerEnterZone(InteractionZone zone)
    {
        _nearZone = zone;
        Debug.Log($"[Exploration] Zone détectée : {zone.ZoneType}");
        // TODO : afficher UI "Appuie sur E"
    }
    public void OnPlayerExitZone(InteractionZone zone)
    {
        if (_nearZone == zone) _nearZone = null;
        Debug.Log($"[Exploration] Zone quittée : {zone.ZoneType}");
        // TODO : cacher UI
    }

    // Interaction
    private void CheckInteraction()
    {
        if (_nearZone == null) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        switch (_nearZone.ZoneType)
        {
            case ZoneType.Desk: GoToDesk(); break;
            case ZoneType.CoffeeMachine: UseCoffeeMachine(); break;
        }
    }
    private void GoToDesk()
    {
        Debug.Log("[Exploration] → Chargement de la scène bureau...");
        playerController.SetMovement(false);
        // TODO : lancer une transition (fondu, animation) avant LoadScene
        SceneManager.LoadScene(_deskSceneName);
    }

    private void UseCoffeeMachine()
    {
        Debug.Log("[Exploration] ☕ Machine à café utilisée !");
        // Exemple : SessionManager.Instance.AddCoffeeBonus();
    }
}
