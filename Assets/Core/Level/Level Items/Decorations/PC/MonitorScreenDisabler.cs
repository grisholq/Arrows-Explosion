using UnityEngine;
using System.Collections.Generic;

public class MonitorScreenDisabler : MonoBehaviour
{
    private MeshRenderer[] _meshRenderers;

    [SerializeField]
    private Material destroyMaterial;
    private Material defaultMaterial;

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        if (_meshRenderers.Length > 0)
            defaultMaterial = _meshRenderers[0].material;
    }

    public void DisableScreen()
    {
        foreach (var renderer in _meshRenderers)
        {
            renderer.material = destroyMaterial;
        }
    }
}