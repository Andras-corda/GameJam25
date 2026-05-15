using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Wire — Fil draggable identifié par une couleur.
/// Le joueur tire le fil vers le port de la même couleur.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Wire : MonoBehaviour
{
    // ── État ──────────────────────────────────────────────────────────────────

    public Color WireColor { get; private set; }
    private bool _isDragging = false;
    private bool _isConnected = false;
    private LineRenderer _line;
    private WireEvent _owner;
    private Vector3 _startPos;
    private Transform _startPort;
    public void Init(Color color, Transform startPort, WireEvent owner)
    {
        WireColor = color;
        _startPort = startPort;
        _owner = owner;
        _startPos = transform.position;

        // Applique la couleur au SpriteRenderer du fil
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = color;

        // LineRenderer pour dessiner le fil entre le port et la position courante
        _line = gameObject.AddComponent<LineRenderer>();
        _line.positionCount = 2;
        _line.startWidth = 0.08f;
        _line.endWidth = 0.08f;
        _line.useWorldSpace = true;

        // Applique la couleur au LineRenderer
        _line.startColor = color;
        _line.endColor = color;

        UpdateLine(_startPos);
    }
    private void OnMouseDown()
    {
        if (_isConnected) return;
        _isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!_isDragging) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        transform.position = mouseWorld;
        UpdateLine(mouseWorld);
    }

    private void OnMouseUp()
    {
        if (!_isDragging) return;
        _isDragging = false;

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld);

        if (hit != null)
        {
            Port port = hit.GetComponent<Port>();
            if (port != null)
            {
                TryConnect(port);
                return;
            }
        }

        ResetPosition();
    }
    private void TryConnect(Port port)
    {
        if (port.WireColor == WireColor && !port.isOccupied)
        {
            _isConnected = true;
            port.isOccupied = true;
            transform.position = port.transform.position;
            UpdateLine(port.transform.position);
            _owner.OnWireConnected();
            Debug.Log($"[Wire] Fil {WireColor} connecté !");
        }
        else
        {
            Debug.Log($"[Wire] Mauvais port ou déjà occupé.");
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.position = _startPos;
        UpdateLine(_startPos);
    }

    private void UpdateLine(Vector3 endPos)
    {
        if (_line == null || _startPort == null) return;
        _line.SetPosition(0, _startPort.position);
        _line.SetPosition(1, endPos);
    }
}
