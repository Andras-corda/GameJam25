using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : EventBase
{
    [Header("Prefab d'une flèche")]
    [SerializeField] private GameObject _arrowPrefab;

    [Header("Slots (enfants, positions fixes)")]
    [SerializeField] private List<Transform> _slots;

    [Header("Sprites directionnels")]
    [SerializeField] private Sprite _spriteUp;
    [SerializeField] private Sprite _spriteDown;
    [SerializeField] private Sprite _spriteLeft;
    [SerializeField] private Sprite _spriteRight;

    private KeyCode[] _sequence;
    private SpriteRenderer[] _spawnedArrows;
    private int _currentIndex;

    public static readonly KeyCode[] PossibleKeys =
        { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    private void Awake()
    {
        _currentDifficulty = 0;
    }

    protected override void OnUpdate()
    {
        CheckInput();
    }
    public override void StartEvent()
    {
        _currentIndex = 0;
        GenerateSequence();
        SpawnArrows();
        base.StartEvent();
    }

    public override void EndEvent()
    {
        DestroyArrows();
        base.EndEvent();
    }

    protected override IEnumerator RunEvent()
    {
        while (isRunning)
            yield return null;
    }
    private void GenerateSequence()
    {
        _sequence = new KeyCode[_slots.Count];
        for (int i = 0; i < _sequence.Length; i++)
            _sequence[i] = PossibleKeys[Random.Range(0, PossibleKeys.Length)];
    }
    private void SpawnArrows()
    {
        _spawnedArrows = new SpriteRenderer[_slots.Count];

        for (int i = 0; i < _slots.Count; i++)
        {
            GameObject obj = Instantiate(_arrowPrefab, _slots[i]);
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            sr.sprite = GetSprite(_sequence[i]);
            _spawnedArrows[i] = sr;
        }
    }

    private void DestroyArrows()
    {
        if (_spawnedArrows == null) return;
        foreach (var sr in _spawnedArrows)
            if (sr != null) Destroy(sr.gameObject);
        _spawnedArrows = null;
    }

    private void SetArrowDone(int index)
    {
        if (_spawnedArrows == null || index >= _spawnedArrows.Length) return;
        Color c = _spawnedArrows[index].color;
        c.a = 0.3f;
        _spawnedArrows[index].color = c;
    }

    private Sprite GetSprite(KeyCode key) => key switch
    {
        KeyCode.UpArrow => _spriteUp,
        KeyCode.DownArrow => _spriteDown,
        KeyCode.LeftArrow => _spriteLeft,
        KeyCode.RightArrow => _spriteRight,
        _ => _spriteUp
    };
    private void CheckInput()
    {
        foreach (KeyCode key in PossibleKeys)
        {
            if (!Input.GetKeyDown(key)) continue;

            if (key == _sequence[_currentIndex])
            {
                SetArrowDone(_currentIndex);
                _currentIndex++;

                if (_currentIndex >= _sequence.Length)
                    WinEvent();
            }
            else
            {
                LoseEvent();
            }

            break;
        }
    }
}