using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptchaEvent : EventBase
{
    [Header("UI")]
    [SerializeField] private SpriteRenderer _questionRenderer;

    [Header("Slots")]
    [SerializeField] private List<CaptchaCell> _slots;

    [Header("Données")]
    [SerializeField] private CaptchaData[] _possibleData;

    private CaptchaData _currentData;
    private HashSet<int> _selectedIndexes = new HashSet<int>();

    private void Awake()
    {
        _currentDifficulty = 0;
    }
    public override void StartEvent()
    {
        _selectedIndexes.Clear();
        PickRandomData();
        DisplayQuestion();
        SetupSlots();
        base.StartEvent();
    }

    public override void EndEvent()
    {
        ClearSlots();
        base.EndEvent();
    }

    protected override IEnumerator RunEvent()
    {
        while (isRunning)
            yield return null;
    }
    private void PickRandomData()
    {
        _currentData = _possibleData[Random.Range(0, _possibleData.Length)];
    }

    private void DisplayQuestion()
    {
        if (_questionRenderer != null)
            _questionRenderer.sprite = _currentData.questionSprite;
    }

    private void SetupSlots()
    {
        int total = Mathf.Min(_currentData.captchaSet.Length, _slots.Count);

        for (int i = 0; i < total; i++)
            _slots[i].Init(i, _currentData.captchaSet[i], _currentData.captchaSetSelected[i], this);
    }

    private void ClearSlots()
    {
        foreach (var slot in _slots)
            slot.Clear();
    }

    public void OnCellClicked(int index)
    {
        if (!isRunning) return;

        if (_selectedIndexes.Contains(index))
            _selectedIndexes.Remove(index);
        else
            _selectedIndexes.Add(index);

        _slots[index].SetSelected(_selectedIndexes.Contains(index));
    }

    public void OnValidate()
    {
        if (!isRunning) return;

        if (CheckAnswer())
            WinEvent();
        else
            LoseEvent();
    }

    private bool CheckAnswer()
    {
        for (int i = 0; i < _currentData._response.Length; i++)
        {
            if (_currentData._response[i] != _selectedIndexes.Contains(i))
                return false;
        }
        return true;
    }
}

