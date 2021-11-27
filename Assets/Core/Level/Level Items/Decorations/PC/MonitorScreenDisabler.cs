using UnityEngine;
using System.Collections.Generic;

public class MonitorScreenDisabler : MonoBehaviour
{
    private MeshRenderer[] _meshRenderers;

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void DisableScreen()
    {
        foreach (var renderer in _meshRenderers)
        {
            renderer.material.color = Color.black;
        }
    }
}