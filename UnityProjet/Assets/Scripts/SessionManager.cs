using UnityEngine;

/// <summary>
/// SessionManager — Script persistant entre toutes les scènes.
/// Stocke les données globales de la session de jeu.
/// </summary>
public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }
    public GestionHud obj;

    [Header("Score")]
    public int TotalScore { get; private set; }
    public float Multiplier { get; private set; }
    public int ComboCount { get; private set; }

    [Header("État du joueur")]
    public bool HasCrashed { get; private set; }
    public int MiniGamesPlayed { get; private set; }
    public int MiniGamesWon { get; private set; }

    [Header("Paramètres multiplicateur")]
    [SerializeField] private float multiplierMin = 0.5f;
    [SerializeField] private float multiplierMax = 5.0f;
    [SerializeField] private float multiplierStep = 0.5f;
    [SerializeField] private float multiplierPenalty = 1.0f;

    private void Awake()
    {
        // Si une instance existe déjà, on détruit le doublon
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Survit aux changements de scène
        InitSession();
    }

    private void InitSession()
    {
        TotalScore = 0;
        Multiplier = 1.0f;
        ComboCount = 0;
        HasCrashed = false;
        MiniGamesPlayed = 0;
        MiniGamesWon = 0;
        obj = GameObject.Find("HUDScript").GetComponent<GestionHud>();
        obj.UpdateScore(0);
    }
    public void ResetSession()
    {
        InitSession();
        obj = GameObject.Find("HUDScript").GetComponent<GestionHud>();
        Debug.Log("[Session] Session réinitialisée.");
    }

    public void AddScore(int basePoints)
    {
        obj = GameObject.Find("HUDScript").GetComponent<GestionHud>();
        int gained = Mathf.RoundToInt(basePoints * Multiplier);
        TotalScore += gained;
        obj.UpdateScore(TotalScore);
        Debug.Log($"[Session] +{gained} pts (base {basePoints} × {Multiplier}) → Total : {TotalScore}");
    }

    public void RegisterWin()
    {
        MiniGamesPlayed++;
        MiniGamesWon++;
        ComboCount++;

        Multiplier = Mathf.Min(multiplierMax, Multiplier + multiplierStep);
        Debug.Log($"[Session] WIN — Combo : {ComboCount} | Multiplicateur : {Multiplier}x");
    }

    // Quand le joueur rate un mini-jeu (hors crash d'Unreal).
    public void RegisterLoss()
    {
        MiniGamesPlayed++;
        ComboCount = 0;

        Multiplier = Mathf.Max(multiplierMin, Multiplier - multiplierPenalty);
        Debug.Log($"[Session] LOSS — Combo reset | Multiplicateur : {Multiplier}x");
    }

    // Déclenche l'état de crash (Unreal à planté).
    public void TriggerCrash()
    {
        HasCrashed = true;
        Debug.Log("[Session] !! PC CRASH !!");
    }

    // Debug
    public string GetSummary()
    {
        float winRate = MiniGamesPlayed > 0
            ? (float)MiniGamesWon / MiniGamesPlayed * 100f
            : 0f;

        return $"Score : {TotalScore}\n" +
               $"Mini-jeux joués : {MiniGamesPlayed}\n" +
               $"Mini-jeux réussis : {MiniGamesWon} ({winRate:F0}%)\n" +
               $"Multiplicateur final : {Multiplier}x\n" +
               $"PC crashé : {(HasCrashed ? "Oui" : "Non")}";
    }
}