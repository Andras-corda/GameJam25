using System.Collections;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


public class CatchItEvent : EventBase
{
    private GameObject _button;
    private TMP_Text _tmpBtn;
    protected override IEnumerator RunEvent()
    {
        _tmpBtn = _button.GetComponentInChildren<TMP_Text>();
        while (true)
        {
            MoveComponent();
            float speed = SpeedCalc();

            yield return null;
        }

        //return null;
    }

    public override void StartEvent()
    {   
        Debug.Log("StartEvent CatchIt");
        base.StartEvent();
    }

    public override void EndEvent()
    {
        Debug.Log("EndEvent CatchIt");
        Destroy(_button);
    }
     public float DistanceCalculator(Vector3 mousePos)
    {
        return Vector3.Distance(
           transform.position,
           mousePos);
    }
    private float SpeedCalc()
    {
        return 5f;
        
    }
    private void MoveComponent()
    { 
        transform.position += Vector3.right * SpeedCalc() * Time.deltaTime;
    }
    
}
