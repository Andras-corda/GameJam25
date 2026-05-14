using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EventBase : MonoBehaviour
{
    [Header("Base from EventBase")]
    [SerializeField] protected int _index;
    [SerializeField] protected string _name;
    [SerializeField] protected float _timer;
    public float timeLast;
    public bool isRunning;

    // Optionnel
    [SerializeField] protected List<string> _tags;
    // Bonus
    protected int _currentDifficulty; // 0 => Easy 1 => Normal 2 => Hard

    private Coroutine _timerCoroutine;
    protected bool _eventWon = false;

    public virtual void StartEvent()
    {
        _eventWon = false;
        isRunning = true;
        timeLast = _timer;

        _timerCoroutine = StartCoroutine(TimerCoroutine());
        StartCoroutine(RunEvent());
    }

    public virtual void EndEvent()
    {
        if (!isRunning) return;

        isRunning = false;
        timeLast = Mathf.Max(0f, timeLast);

        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }

        // On prévient directement le GameManager
        GameManager.Instance.OnEventFinished(_eventWon, timeLast);

        Debug.Log($"[Event] '{_name}' terminé — Succès : {_eventWon} | Temps restant : {timeLast:F2}s");
    }

    protected virtual IEnumerator RunEvent()
    {
        yield break;
    }
    protected virtual void OnUpdate() { }

    private IEnumerator TimerCoroutine()
    {
        timeLast = _timer;

        while (timeLast > 0f)
        {
            timeLast -= Time.deltaTime;
            yield return null;
        }

        // Timeout => défaite automatique
        timeLast = 0f;
        _eventWon = false;
        EndEvent();
    }

    // Appelé par la classe enfant quand le joueur réussit
    protected void WinEvent()
    {
        _eventWon = true;
        EndEvent();
    }

    // Appelé par la classe enfant si le joueur fait une erreur fatale
    protected void LoseEvent()
    {
        _eventWon = false;
        timeLast = 0f;
        EndEvent();
    }

    // Pourcentage de temps restant (0.0 => 1.0)
    public float TimeRatio => _timer > 0f ? timeLast / _timer : 0f;

    // Score brut (0–100) basé sur le temps restant
    public int ComputeScore() => _eventWon ? Mathf.RoundToInt(TimeRatio * 100f) : 0;
}
