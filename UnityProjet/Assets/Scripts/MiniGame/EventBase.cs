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
    public virtual void StartEvent()
    {

    }
    public virtual void EndEvent()
    {

    }
    
    protected virtual IEnumerator RunEvent()
    {
        return null;
    }
}
