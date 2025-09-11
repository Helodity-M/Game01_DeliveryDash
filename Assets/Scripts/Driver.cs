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

    // Update is called once per frame
    void Update()
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

        float moveAmount = getMovementCurve() * moveSpeed * Time.deltaTime;
        //Need to invert input's sign, otherwise A would turn right.
        float steerAmount = Mathf.Abs(getMovementCurve()) * (steerSpeed + (Mathf.Abs(getMovementCurve()) * moveSteerRatio)) * Time.deltaTime * -input.x;

        transform.Rotate(0, 0, steerAmount);
        transform.Translate(0, moveAmount, 0);
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
