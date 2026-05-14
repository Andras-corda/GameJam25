using UnityEngine;

[System.Serializable]
public class CaptchaData
{
    public Sprite[] captchaSet;
    public string captchaQuestion;
    public bool[] _response;
}
