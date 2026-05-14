using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEvent : EventBase
{
    [Header("Wire Attribute")]
    private int _wireQuantity;
    private List<GameObject> _wires;
    private List<Transform> _spawnPointPort;
   
    [Header("Static prefab")]
    static readonly GameObject wirePrefab;
    static readonly GameObject PortPrefab;

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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

    private void InstantiateWires()
    {

    }
    private void InstantiatePort()
    {

    }

}
