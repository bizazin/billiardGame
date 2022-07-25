using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Trajectory : MonoBehaviour
{
    [SerializeField] private CircleIcon _circleIcon;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LineRenderer _trajectory;
    [SerializeField] private LineRenderer _hittedTrajectory;

    private const int RIGHT_ANGLE = 90;
    private const int RAY_SCALE = 10;
    private const float TRAJECTORY_AFTER_BOUNCE_SCALE = .1f;
    private const float RAISE_LINE_ABOVE_BALLS = 2.01f;

    private float _collRadius;

    public Vector3 Direction { get; private set; }

    private void Start()
    {
        _collRadius = GetComponent<SphereCollider>().radius;
    }

    private void FixedUpdate()
    {
        var ray = CreateRay();
        var dots = GetTrajectory(ray);

        Direction = dots.Direction;

        DrawLine(new TrajectoryDots(dots.Direction, dots.BounceDirection, dots.HittedBallPosition, dots.HitDirection));

    }

    private Ray CreateRay()
    {
        var ray = new Ray(transform.position, transform.right);
        Debug.DrawRay(transform.position, transform.right * RAY_SCALE, Color.white);
        return ray;
    }

    private TrajectoryDots GetTrajectory(Ray ray)
    {
        //https://www.real-world-physics-problems.com/physics-of-billiards.html
        var dots = new TrajectoryDots();
        if (Physics.SphereCast(ray, _collRadius, out RaycastHit hit, _layerMask))
        {
            dots.Direction = hit.point + hit.normal * _collRadius;
            dots.BounceDirection = dots.Direction;
            dots.HitDirection = dots.Direction;
            dots.HittedBallPosition = dots.Direction;

            if (hit.transform.TryGetComponent(out Ball _))
            {
                float angleOfBounce = Mathf.Abs(Vector3.Angle(hit.normal, Vector3.right) - RIGHT_ANGLE);
                HitAngle angle = CalculateAngles(dots.Direction, angleOfBounce, hit);

                dots.BounceDirection = dots.Direction + angle.Quat * Vector3.right * TRAJECTORY_AFTER_BOUNCE_SCALE;

                Vector3 hitPos = hit.point - hit.normal * _collRadius;
                hitPos.y *= RAISE_LINE_ABOVE_BALLS;
                dots.HittedBallPosition = hitPos;

                Vector3 hitDir = dots.Direction + angle.HitQuat * Vector3.right * TRAJECTORY_AFTER_BOUNCE_SCALE;
                hitDir.y *= RAISE_LINE_ABOVE_BALLS;
                dots.HitDirection = hitDir;
            }

            _circleIcon.SetIconPosition(dots.Direction);
        }
        return dots;
    }

    private HitAngle CalculateAngles(Vector3 direction, float angleOfBounce, RaycastHit hit)
    {
        var angle = new HitAngle();
        if (hit.point.z > hit.transform.position.z && hit.point.x < hit.transform.position.x)
        {
            angle.Quat = Quaternion.Euler(0, -angleOfBounce, 0);
            angle.HitQuat = angle.Quat * Quaternion.Euler(0, RIGHT_ANGLE, 0);
        }
        else if (hit.point.z > hit.transform.position.z && hit.point.x > hit.transform.position.x)
        {
            angle.Quat = Quaternion.Euler(0, angleOfBounce - 180, 0);
            angle.HitQuat = angle.Quat * Quaternion.Euler(0, -RIGHT_ANGLE, 0);
        }
        else if (hit.point.z < hit.transform.position.z && hit.point.x > hit.transform.position.x)
        {
            angle.Quat = Quaternion.Euler(0, 180 - angleOfBounce, 0);
            angle.HitQuat = angle.Quat * Quaternion.Euler(0, RIGHT_ANGLE, 0);
        }
        else if (hit.point.z < hit.transform.position.z && hit.point.x < hit.transform.position.x)
        {
            angle.Quat = Quaternion.Euler(0, angleOfBounce, 0);
            angle.HitQuat = angle.Quat * Quaternion.Euler(0, -RIGHT_ANGLE, 0);
        }
        return angle;
    }

    private void DrawLine(TrajectoryDots dots)
    {
        _trajectory.SetPosition(0, transform.position);
        _trajectory.SetPosition(1, dots.Direction);
        _trajectory.SetPosition(2, dots.BounceDirection);

        _hittedTrajectory.SetPosition(0, dots.HittedBallPosition);
        _hittedTrajectory.SetPosition(1, dots.HitDirection);
    }
}

public class TrajectoryDots
{
    public Vector3 Direction { get; set; }
    public Vector3 BounceDirection { get; set; }

    public Vector3 HittedBallPosition { get; set; }
    public Vector3 HitDirection { get; set; }

    public TrajectoryDots()
    {
        Direction = Vector3.zero;
        BounceDirection = Vector3.zero;
        HittedBallPosition = Vector3.zero;
        HitDirection = Vector3.zero;
    }

    public TrajectoryDots(Vector3 direction, Vector3 bounceDirection, Vector3 hittedBallPosition, Vector3 hitDirection)
    {
        Direction = direction;
        BounceDirection = bounceDirection;
        HittedBallPosition = hittedBallPosition;
        HitDirection = hitDirection;
    }
}

public class HitAngle
{
    public Quaternion Quat { get; set; }
    public Quaternion HitQuat { get; set; }

    public HitAngle()
    {
        Quat = Quaternion.identity;
        HitQuat = Quaternion.identity;
    }

    public HitAngle(Quaternion quat, Quaternion hitQuat)
    {
        Quat = quat;
        HitQuat = hitQuat;
    }
}

