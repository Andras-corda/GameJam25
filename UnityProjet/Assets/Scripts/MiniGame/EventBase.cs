using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EventBase : MonoBehaviour
{
    [Header("Base from EventBase")]
    [SerializeField] protected string _name;
    [SerializeField] protected float _timer;
    public float timeLast;
    public bool isRunning;

    [Header("Focus")]
    [SerializeField] private bool _needsFocus = true;
    [SerializeField] private Collider2D _windowCollider;

    protected int _currentDifficulty; // 0 = Easy | 1 = Normal | 2 = Hard
    private Coroutine _timerCoroutine;
    protected bool _eventWon = false;
    private bool _hasFocus = false;
    private void Update()
    {
        if (!_hasFocus)
        {
            if (_needsFocus)
                CheckFocusClick();
            return;
        }

        if (isRunning)
            OnUpdate();
    }
    private void CheckFocusClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_windowCollider != null && _windowCollider.OverlapPoint(mouseWorld))
        {
            _hasFocus = true;
            Debug.Log($"[Event] '{_name}' — Focus obtenu !");
            StartEventInternal();
        }
    }
    public virtual void StartEvent()
    {
        _eventWon = false;
        isRunning = false;
        timeLast = _timer;

        if (_needsFocus)
        {
            _hasFocus = false;
            Debug.Log($"[Event] '{_name}' — En attente de focus...");
        }
        else
        {
            _hasFocus = true;
            Debug.Log($"[Event] '{_name}' — Démarrage immédiat.");
            StartEventInternal();
        }
    }

    private void StartEventInternal()
    {
        isRunning = true;
        if (_windowCollider != null) _windowCollider.enabled = false;
        _timerCoroutine = StartCoroutine(TimerCoroutine());
        StartCoroutine(RunEvent());
    }

    public virtual void EndEvent()
    {
        if (!isRunning) return;

        isRunning = false;
        _hasFocus = false;
        timeLast = Mathf.Max(0f, timeLast);

        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }

        GameManager.Instance.OnEventFinished(_eventWon, timeLast);
        Debug.Log($"[Event] '{_name}' terminé — Succès : {_eventWon} | Temps restant : {timeLast:F2}s");
    }
    protected void WinEvent()
    {
        _eventWon = true;
        EndEvent();
    }

    protected void LoseEvent()
    {
        _eventWon = false;
        timeLast = 0f;
        EndEvent();
    }
    private IEnumerator TimerCoroutine()
    {
        timeLast = _timer;

        while (timeLast > 0f)
        {
            timeLast -= Time.deltaTime;
            yield return null;
        }

        timeLast = 0f;
        _eventWon = false;
        EndEvent();
    }
    protected virtual IEnumerator RunEvent()
    {
        yield break;
    }

    protected virtual void OnUpdate() { }
    public float TimeRatio => _timer > 0f ? timeLast / _timer : 0f;
    public int ComputeScore() => _eventWon ? Mathf.RoundToInt(TimeRatio * 100f) : 0;
}