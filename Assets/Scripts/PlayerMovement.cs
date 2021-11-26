using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private Vector3 movement;

    private void GetInput()
    {
        var h = (Vector3.right + Vector3.back) * Input.GetAxisRaw("Horizontal");
        var v = (Vector3.right + Vector3.forward) * Input.GetAxisRaw("Vertical");

        movement = (h + v).normalized;
    }

    
    public float acceleration = 100;
    public float counterAcceleration = 10;
    
    private void Move()
    {
        var velocity = rb.velocity;
        velocity.y = 0;
        rb.AddForce(movement * acceleration + -velocity * counterAcceleration);
    }
}
