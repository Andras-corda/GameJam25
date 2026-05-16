using UnityEngine;
using TMPro;
public class GestionHud : MonoBehaviour
{
    public TextMeshProUGUI score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateScore(int input)
    {
        score.text = $" Current score : { input } pts";

    }
}
