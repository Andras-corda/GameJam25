using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class CaptchaCell : MonoBehaviour
{
    private SpriteRenderer _sr;
    private int _index;
    private CaptchaEvent _owner;
    private Sprite _spriteNormal;
    private Sprite _spriteSelected;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Init(int index, Sprite normal, Sprite selected, CaptchaEvent owner)
    {
        _index = index;
        _owner = owner;
        _spriteNormal = normal;
        _spriteSelected = selected;
        _sr.sprite = _spriteNormal;
    }

    public void SetSelected(bool selected)
    {
        _sr.sprite = selected ? _spriteSelected : _spriteNormal;
    }

    public void Clear()
    {
        _sr.sprite = null;
    }

    private void OnMouseDown()
    {
        Debug.Log($"CaptchaCell cliquée : index {_index}");
        _owner.OnCellClicked(_index);
    }
}
