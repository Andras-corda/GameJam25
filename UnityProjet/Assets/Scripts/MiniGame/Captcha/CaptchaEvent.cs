using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptchaEvent : EventBase
{
    [Header("Attribute")]
    [SerializeField] private TextMeshPro _question;

    [Header("OBJ References")]
    [SerializeField] private Captcha _captcha;
    [SerializeField] private CaptchaData _data;

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
}
