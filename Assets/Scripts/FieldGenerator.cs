using System.Collections.Generic;
using Fields;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public static FieldGenerator Instance { get; private set; }
    
    public GameObject placeableFieldPrefab;
    public GameObject enemySpawnFieldPrefab;
    public GameObject serverFieldPrefab;
    public GameObject usbFieldPrefab;

    public int rows = 5;
    public int columns = 9;
    public bool firstColumnUsbSlots;

    private Dictionary<int, List<Field>> _rows = new Dictionary<int, List<Field>>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (var y = 0; y < rows; y++)
        {
            _rows.Add(y, new List<Field>());
            
            _rows[y].Add(CreateField(serverFieldPrefab, 0, y));

            for (var x = 1; x < columns + 1; x++)
            {
                var prefab = firstColumnUsbSlots && x == 1 ? usbFieldPrefab : placeableFieldPrefab;
                
                _rows[y].Add(CreateField(prefab, x, y));
            }

            _rows[y].Add(CreateField(enemySpawnFieldPrefab, columns + 1, y));
        }
    }

    private Field CreateField(GameObject prefab, int x, int y)
    {
        var field = Instantiate(
            prefab,
            new Vector3(x - ((columns + 1) / 2f), 0, y - ((rows - 1) / 2f)),
            Quaternion.identity
        );

        field.name = $"Field_{x}_{y}";
        field.transform.SetParent(transform);
        
        var component = field.GetComponent<Field>();
        component.SetCoords(x, y);

        return component;
    }
    
    public Field GetPreviousFieldInRow(int row, Field currentField)
    {
        if (currentField == null)
        {
            return _rows[row][_rows[row].Count - 1];
        }

        if (currentField.column <= 0)
        {
            return _rows[row][0];
        }
        
        return _rows[row][currentField.column - 1];
    }
    
    public Field GetNextFieldInRow(int row, Field currentField)
    {
        if (currentField == null)
        {
            return _rows[row][0];
        }

        if (currentField.column >= columns + 1)
        {
            return _rows[row][columns + 1];
        }
        
        return _rows[row][currentField.column + 1];
    }

    public void Clean()
    {
        Instance = null;

        foreach (var row in _rows.Values)
        {
            row.ForEach(field => field.tower?.NewLevelLoaded());
        }
    }

    public void CompromiseRow(int row)
    {
        foreach (var field in _rows[row])
        {
            field.Compromise();
        }
    }
}