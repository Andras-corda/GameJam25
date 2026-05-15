using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Port : MonoBehaviour
{
    public Color WireColor { get; private set; }
    public bool isOccupied = false;

    public void Init(Color color)
    {
        WireColor = color;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = color;
    }

    public void Reset()
    {
        isOccupied = false;
    }
}