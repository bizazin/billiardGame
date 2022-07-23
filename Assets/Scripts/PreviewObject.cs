using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _screenPos;
    private float _angleOffset;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            _screenPos = _camera.WorldToScreenPoint(transform.position);
            Vector3 vector3 = Input.mousePosition - _screenPos;
            _angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vector3.y, vector3.x)) * Mathf.Rad2Deg;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 vector3 = Input.mousePosition - _screenPos;
            float angle = Mathf.Atan2(vector3.y, vector3.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, - (angle + _angleOffset), 0);
        }
    }
}
