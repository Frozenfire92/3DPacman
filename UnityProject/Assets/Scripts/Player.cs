using UnityEngine;
using System.Collections;

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class Player : MonoBehaviour
{
    Direction nextMove;
    Rigidbody rb;

    public float speed = 1.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        nextMove = Direction.None;
    }

    void Start()
    {
        applyRotationVelocity();
    }

    void Update()
    {
        if (Input.GetButtonDown("Up")) { nextMove = Direction.Up; }
        if (Input.GetButtonDown("Down")) { nextMove = Direction.Down; }
        if (Input.GetButtonDown("Left")) { nextMove = Direction.Left; }
        if (Input.GetButtonDown("Right")) { nextMove = Direction.Right; }
    }

    void FixedUpdate()
    {
        switch (nextMove)
        {
            case Direction.Up:
                transform.Rotate(-90, 0, 0);
                nextMove = Direction.None;
                applyRotationVelocity();
                break;
            case Direction.Down:
                transform.Rotate(90, 0, 0);
                nextMove = Direction.None;
                applyRotationVelocity();
                break;
            case Direction.Left:
                transform.Rotate(0, -90, 0);
                nextMove = Direction.None;
                applyRotationVelocity();
                break;
            case Direction.Right:
                nextMove = Direction.None;
                transform.Rotate(0, 90, 0);
                applyRotationVelocity();
                break;
            case Direction.None: break;
        }
    }

    void applyRotationVelocity()
    {
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
        else if (other.tag == "Enemy")
        {
            //EXPLOSION
            //GAME OVER
            //SUCH LOSS
        }
    }
}
