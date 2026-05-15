using UnityEngine;

[CreateAssetMenu(fileName = "CaptchaData", menuName = "Captcha/CaptchaData")]
public class CaptchaData : ScriptableObject
{
    [Header("Contenu")]
    public Sprite[] captchaSet;         // Sprites normaux
    public Sprite[] captchaSetSelected; // Sprites version sélectionnée (même ordre)
    public Sprite questionSprite;

    [Header("Réponses")]
    [Tooltip("true = cette image est une bonne réponse, même ordre que captchaSet")]
    public bool[] _response;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (captchaSet == null) return;

        // Synchronise _response avec captchaSet
        if (_response == null || _response.Length != captchaSet.Length)
        {
            System.Array.Resize(ref _response, captchaSet.Length);
            UnityEditor.EditorUtility.SetDirty(this);
        }

        // Synchronise captchaSetSelected avec captchaSet
        if (captchaSetSelected == null || captchaSetSelected.Length != captchaSet.Length)
        {
            System.Array.Resize(ref captchaSetSelected, captchaSet.Length);
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}
