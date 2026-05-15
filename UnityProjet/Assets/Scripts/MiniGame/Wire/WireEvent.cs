using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WireEvent : EventBase
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _wirePrefab;
    [SerializeField] private GameObject _portPrefab;

    [Header("Slots départ (colonne gauche)")]
    [SerializeField] private List<Transform> _startSlots;

    [Header("Slots arrivée (colonne droite)")]
    [SerializeField] private List<Transform> _endSlots;

    [Header("Couleurs des fils")]
    [SerializeField] private List<Color> _wireColors; // Autant de couleurs que de slots

    private List<Wire> _spawnedWires = new List<Wire>();
    private List<Port> _spawnedPorts = new List<Port>();
    private int _connectedCount = 0;

    private void Awake()
    {
        _currentDifficulty = 0;
    }
    public override void StartEvent()
    {
        _connectedCount = 0;
        InstantiatePorts();
        InstantiateWires();
        base.StartEvent();
    }

    public override void EndEvent()
    {
        DestroyAll();
        base.EndEvent();
    }

    protected override IEnumerator RunEvent()
    {
        while (isRunning)
            yield return null;
    }
    private void InstantiatePorts()
    {
        _spawnedPorts.Clear();

        // Mélange les couleurs pour les ports d'arrivée
        List<Color> shuffledColors = new List<Color>(_wireColors);
        Shuffle(shuffledColors);

        for (int i = 0; i < _endSlots.Count; i++)
        {
            GameObject obj = Instantiate(_portPrefab, _endSlots[i].position, Quaternion.identity, _endSlots[i]);
            Port port = obj.GetComponent<Port>();
            port.Init(shuffledColors[i]);
            _spawnedPorts.Add(port);
        }
    }
    private void InstantiateWires()
    {
        _spawnedWires.Clear();

        for (int i = 0; i < _startSlots.Count; i++)
        {
            GameObject obj = Instantiate(_wirePrefab, _startSlots[i].position, Quaternion.identity, _startSlots[i]);
            Wire wire = obj.GetComponent<Wire>();
            wire.Init(_wireColors[i], _startSlots[i], this);
            _spawnedWires.Add(wire);
        }
    }

    private void DestroyAll()
    {
        foreach (var w in _spawnedWires) if (w != null) Destroy(w.gameObject);
        foreach (var p in _spawnedPorts) if (p != null) Destroy(p.gameObject);
        _spawnedWires.Clear();
        _spawnedPorts.Clear();
    }
    public void OnWireConnected()
    {
        _connectedCount++;
        Debug.Log($"[WireEvent] {_connectedCount}/{_spawnedWires.Count} fils connectés.");

        if (_connectedCount >= _spawnedWires.Count)
            WinEvent();
    }
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}