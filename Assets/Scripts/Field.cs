using Towers;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field ActiveField { get; private set; } 
    
    public Material defaultMaterial;
    public Material hoverErrorMaterial;
    public Material hoverOkMaterial;

    public int row;
    public int column;

    public Tower tower;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetCoords(int x, int y)
    {
        this.row = y;
        this.column = x;
    }
    
    private void OnMouseEnter()
    {
        if (Tower.OnHand)
        {
            _meshRenderer.material = CanPlace(Tower.OnHand) ? hoverOkMaterial : hoverErrorMaterial;   
        }

        ActiveField = this;
    }

    private void OnMouseExit()
    {
        _meshRenderer.material = defaultMaterial;
        if (ActiveField == this)
        {
            ActiveField = null;
        }
    }

    public void PlaceTower(Tower t)
    {
        tower = t;
        _meshRenderer.material = defaultMaterial;
    }

    public bool CanPlace(Tower other)
    {
        return tower == null;
    }
}
