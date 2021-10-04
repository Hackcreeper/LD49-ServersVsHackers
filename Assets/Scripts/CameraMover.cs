using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public static CameraMover Instance { get; private set; }
    
    public float smoothFactor = 3f;
    
    private Vector3 _targetPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _targetPosition = transform.position;
    }

    public void MoveTo(Vector3 position)
    {
        _targetPosition = position;
    }
    
    private void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            _targetPosition,
            smoothFactor * Time.deltaTime
        );
    }
}