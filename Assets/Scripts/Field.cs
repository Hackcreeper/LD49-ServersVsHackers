using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Material defaultMaterial;
    public Material hoverMaterial;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        _meshRenderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        _meshRenderer.material = defaultMaterial;
    }
}
