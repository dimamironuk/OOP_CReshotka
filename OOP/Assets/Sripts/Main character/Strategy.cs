using UnityEngine;

public class HomingMovementStrategy : IMovementStrategy
{
    private Transform target;
    private float speed;
    private Transform casterTransform;

    public HomingMovementStrategy(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
    }

    public bool IsTargetValid()
    {
        return target != null && target.gameObject.activeInHierarchy;
    }

    public Vector3 GetTargetPosition()
    {
        return target.position;
    }
    public Transform GetTargetTransform()
    {
        return target;
    }

    public void Move(Transform transform, float deltaTime)
    {
        if (!IsTargetValid()) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * deltaTime
        );
    }
}

public class SpiralMovementStrategy : IMovementStrategy
{
    private float speed;
    private float rotationRate;
    private Vector3 initialDirection;
    private float currentAngle = 0f;

    public SpiralMovementStrategy(Vector3 initialDir, float moveSpeed, float rotationRate)
    {
        this.speed = moveSpeed;
        this.rotationRate = rotationRate;
        this.initialDirection = initialDir.normalized;
    }
    public bool IsTargetValid() { return true; }
    public Vector3 GetTargetPosition() { return Vector3.zero; }
    public Transform GetTargetTransform() { return null; }

    public void Move(Transform transform, float deltaTime)
    {
        currentAngle += rotationRate * deltaTime;
        Vector3 forwardMovement = initialDirection * speed * deltaTime;

        Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
        Vector3 spiralMovement = rotation * forwardMovement;

        transform.position += spiralMovement;
    }
}