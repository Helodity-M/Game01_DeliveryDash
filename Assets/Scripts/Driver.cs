using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 0.3f;
    [SerializeField] float moveSpeed = 0.03f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float move = 0;
        float steer = 0;


        if (Keyboard.current.wKey.isPressed)
        {
            move = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            move = -1;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            steer = 1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            steer = -1;
        }


        transform.Rotate(0, 0, steerSpeed * steer);
        transform.Translate(0, moveSpeed * move, 0);
    }
}
