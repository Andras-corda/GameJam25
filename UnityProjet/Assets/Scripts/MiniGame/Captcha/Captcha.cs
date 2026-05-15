using UnityEngine;

// Ce bouton ne fait que confirmer le choix final du joueur
public class Captcha : MonoBehaviour
{
    [SerializeField] private CaptchaEvent _captchaEvent;
    private void OnMouseDown()
    {
        _captchaEvent.OnValidate();
    }
}
