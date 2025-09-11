using Unity.Cinemachine;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float steerSpeed = 0.3f;
    [SerializeField] float moveSpeed = 0.03f;
    [SerializeField] float moveSteerRatio = -5f;

    [Header("Acceleration")]
    [SerializeField] AnimationCurve accelerationCurve;
    [SerializeField] AnimationCurve reverseCurve;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float decelerationSpeed;
    float accelerationTime;

    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float targetTime = input.y;
        bool tryingToSlowdown = Mathf.Abs(targetTime - accelerationTime) > Mathf.Abs(targetTime);

        //Use decelerationSpeed if we are currently trying to slow down
        float accelerationChange = (tryingToSlowdown ? decelerationSpeed : accelerationSpeed) * Time.deltaTime;
        accelerationChange = Mathf.Min(accelerationChange, Mathf.Abs(targetTime - accelerationTime));
        if (targetTime < accelerationTime)
        {
            //Moving in reverse
            accelerationChange *= -1;
        }
        accelerationTime += accelerationChange;

        float moveAmount = getMovementCurve() * moveSpeed * Time.fixedDeltaTime;
        //Need to invert input's sign, otherwise A would turn right.
        float steerAmount = Mathf.Abs(getMovementCurve()) * (steerSpeed + (Mathf.Abs(getMovementCurve()) * moveSteerRatio)) * Time.fixedDeltaTime * -input.x;

        rb2d.MovePositionAndRotation(rb2d.position + ((Vector2)transform.up * moveAmount), rb2d.rotation + steerAmount);
    }

    float getMovementCurve()
    {
        float curveVal = 0;
        if (accelerationTime > 0)
        {
            curveVal = accelerationCurve.Evaluate(accelerationTime);
        } 
        else
        {
            curveVal = -reverseCurve.Evaluate(-accelerationTime);
        }

        return curveVal;
    }

}
