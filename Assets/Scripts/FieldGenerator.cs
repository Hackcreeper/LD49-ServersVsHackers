using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    public GameObject fieldPrefab;
    public int rows = 5;
    public int columns = 9;

    private void Start()
    {
        for (var x = 0; x < columns; x++)
        {
            for (var y = 0; y < rows; y++)
            {
                var field = Instantiate(
                    fieldPrefab,
                    new Vector3(x-4, 0, y-2),
                    Quaternion.identity
                );

                field.name = $"Field_{x}_{y}";
                field.GetComponent<Field>().SetCoords(x, y);
                field.transform.SetParent(transform);
            }
        }
    }
}
