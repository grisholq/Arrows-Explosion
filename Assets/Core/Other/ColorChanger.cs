using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _renderer;

    private Color _defaultColor = Color.black;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}