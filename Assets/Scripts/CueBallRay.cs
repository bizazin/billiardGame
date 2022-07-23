using UnityEngine;

public class CueBallRay : MonoBehaviour
{
    [SerializeField] private CircleIcon _circleIcon;
    [SerializeField] private LayerMask _layerMask;

    private const int RAY_SCALE = 10;
    private float _radius;

    private void Start()
    {
        _radius = GetComponent<SphereCollider>().radius;
    }

    private void FixedUpdate()
    {
        var ray = CreateRay();

        var direction = GetDirection(ray);

        EventsManager.OnDirectionChoosed?.Invoke(direction);
    }

    private Ray CreateRay()
    {
        var ray = new Ray(transform.position, - transform.right);
        Debug.DrawRay(transform.position, - transform.right * RAY_SCALE, Color.white);
        return ray;
    }

    private Vector3 GetDirection(Ray ray)
    {
        Vector3 direction = Vector3.zero;


        if (Physics.SphereCast(ray, _radius, out RaycastHit hit, _layerMask))
        {
            float radius = _circleIcon.SpriteRadius;
            direction = hit.point + hit.normal * radius;
            _circleIcon.DrawIcon(direction);
        }
        return direction;
    }
}
