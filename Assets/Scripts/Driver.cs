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
        if (Keyboard.current.wKey.isPressed)
        {
            Debug.Log("Forwards is being pressed.");
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            Debug.Log("Backwards is being pressed.");
        }

        if (Keyboard.current.aKey.isPressed)
        {
            Debug.Log("Left is being pressed.");
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            Debug.Log("Right is being pressed.");
        }


        transform.Rotate(0, 0, steerSpeed);
        transform.Translate(0, moveSpeed, 0);
    }
}
