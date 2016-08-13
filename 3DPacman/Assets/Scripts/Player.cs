using UnityEngine;
using System.Collections;

public enum MovementDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class Player : MonoBehaviour
{
    MovementDirection nextMove;
    Rigidbody rb;

    public float speed = 1.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nextMove = MovementDirection.None;
    }

    void Start()
    {
        applyRotationVelocity();
    }

    void Update()
    {
        if (Input.GetButtonDown("Up")) { nextMove = MovementDirection.Up; }
        if (Input.GetButtonDown("Down")) { nextMove = MovementDirection.Down; }
        if (Input.GetButtonDown("Left")) { nextMove = MovementDirection.Left; }
        if (Input.GetButtonDown("Right")) { nextMove = MovementDirection.Right; }
    }

    void FixedUpdate()
    {
        switch (nextMove)
        {
            case MovementDirection.Up:
                transform.Rotate(-90, 0, 0);
                nextMove = MovementDirection.None;
                applyRotationVelocity();
                break;
            case MovementDirection.Down:
                transform.Rotate(90, 0, 0);
                nextMove = MovementDirection.None;
                applyRotationVelocity();
                break;
            case MovementDirection.Left:
                transform.Rotate(0, -90, 0);
                nextMove = MovementDirection.None;
                applyRotationVelocity();
                break;
            case MovementDirection.Right:
                nextMove = MovementDirection.None;
                transform.Rotate(0, 90, 0);
                applyRotationVelocity();
                break;
            case MovementDirection.None: break;
        }
    }

    void applyRotationVelocity()
    {
        // Snap to grid
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),
                                         Mathf.RoundToInt(transform.position.y),
                                         Mathf.RoundToInt(transform.position.z));

        // Transform world to local, apply speed, and transform back again
        Vector3 locVel = transform.InverseTransformDirection(Vector3.zero);
        locVel.z = speed * Time.deltaTime;
        rb.velocity = transform.TransformDirection(locVel);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pellet")
        {
            GameController.instance.AddScore();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Ghost")
        {
            Debug.Log("You hit a ghost");
            //EXPLOSION
            //GAME OVER
            //SUCH LOSS
        }
    }
}
