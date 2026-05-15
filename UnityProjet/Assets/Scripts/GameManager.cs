using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    [Header("Mini-jeux disponibles (prefabs)")]
    [SerializeField] private List<GameObject> _eventPrefabs;

    [Header("Spawn")]
    [SerializeField] private List<Transform> _spawnPoints;

    [Header("Crash")]
    [SerializeField] private GameObject _crashEventPrefab;

    [Header("Timing")]
    [SerializeField] private float _timeBetweenEvents = 10f;

    private EventBase _currentEvent = null;
    private bool _isCrashPending = false;
    private bool _isGameOver = false;
    private Coroutine _spawnCoroutine = null;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(EventLoop());
    }

    private IEnumerator EventLoop()
    {
        SpawnRandomEvent();

        while (!_isGameOver)
        {
            yield return new WaitForSeconds(_timeBetweenEvents);

            if (_isGameOver) yield break;

            SpawnRandomEvent();
        }
    }

    private void SpawnRandomEvent()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            Debug.LogWarning("[GameManager] Aucun spawnPoint assigné");
            return;
        }

        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        if (_currentEvent != null)
            Destroy(_currentEvent.gameObject);

        GameObject prefab = _eventPrefabs[Random.Range(0, _eventPrefabs.Count)];
        GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        _currentEvent = obj.GetComponent<EventBase>();
        _currentEvent.StartEvent();

        Debug.Log($"[GameManager] Mini-jeu lancé : {obj.name}");
    }

    private void SpawnCrashEvent()
    {
        if (_currentEvent != null)
            Destroy(_currentEvent.gameObject);

        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        GameObject obj = Instantiate(_crashEventPrefab, spawnPoint.position, Quaternion.identity);
        _currentEvent = obj.GetComponent<EventBase>();
        _currentEvent.StartEvent();

        Debug.Log("[GameManager] CRASH — mini-jeu de récupération lancé");
    }

    public void OnEventFinished(bool won, float timeLeft)
    {
        if (_currentEvent != null)
        {
            // Calcule et enregistre le score
            int score = _currentEvent.ComputeScore();
            SessionManager.Instance.AddScore(score);
        }

        if (_currentEvent != null)
        {
            Destroy(_currentEvent.gameObject);
            _currentEvent = null;
        }

        if (won)
        {
            SessionManager.Instance.RegisterWin();
            Debug.Log($"[GameManager] Réussi Temps restant : {timeLeft:F2}s");
        }
        else
        {
            SessionManager.Instance.RegisterLoss();

            if (!_isCrashPending)
            {
                _isCrashPending = true;
                SessionManager.Instance.TriggerCrash();

                if (_spawnCoroutine != null)
                {
                    StopCoroutine(_spawnCoroutine);
                    _spawnCoroutine = null;
                }

                SpawnCrashEvent();
            }
            else
            {
                GameOver();
            }
        }
    }
    private void OnCrashEventWon()
    {
        _isCrashPending = false;
        Debug.Log("[GameManager] Crash résolu — reprise de la boucle");
        _spawnCoroutine = StartCoroutine(EventLoop());
    }

    private void GameOver()
    {
        _isGameOver = true;

        if (_currentEvent != null)
        {
            Destroy(_currentEvent.gameObject);
            _currentEvent = null;
        }

        Debug.Log("[GameManager] GAME OVER");
        Debug.Log(SessionManager.Instance.GetSummary());

        // TODO : charger la scène de fin / afficher l'écran de score
    }
}