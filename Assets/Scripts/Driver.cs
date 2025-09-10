using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 0.3f;
    [SerializeField] float moveSpeed = 0.03f;

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float moveAmount = moveSpeed * input.y * Time.deltaTime;
        //Need to invert input's sign, otherwise A would turn right.
        float steerAmount = steerSpeed * -input.x * Time.deltaTime;

        transform.Rotate(0, 0, steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
}
