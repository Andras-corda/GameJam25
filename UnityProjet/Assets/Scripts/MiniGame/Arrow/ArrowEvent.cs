using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : EventBase
{
    [Header("Sprite renderer from prefab")]
    public SpriteRenderer[] Arrows;
    [SerializeField] private KeyCode[] Sequence;
    private int _currentIndex;
    private int _arrowNb;

    [Header("Arrow Key")]
    public static readonly KeyCode[] PossibleKeys = 
        { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    //private System.Random _generator = new System.Random();

    private void Awake()
    {
        _currentDifficulty = 0;
        KeyCode[] Sequence = new KeyCode[_arrowNb];
        GenerateSequence();
    }
    void Start()
    {
        
    }
    void Update()
    {

    }
    public override void StartEvent()
    {

    }

    public override void EndEvent()
    {

    }

    protected override IEnumerator RunEvent()
    {
        return null;
    }
    private void GenerateSequence()
    {
        for (int i = 0; i < Sequence.Length; i++)
            Sequence[i] = PossibleKeys[Random.Range(0, PossibleKeys.Length)];
    }
}
