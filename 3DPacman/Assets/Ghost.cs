using UnityEngine;

public enum Direction3D
{
    Forward,
    Back,
    Up,
    Down,
    Left,
    Right
}

public class Ghost : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;
    float speed;

    public Direction3D currentDirection;
    public float minSpeed = 150f;
    public float maxSpeed = 300f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(minSpeed, maxSpeed);
        currentDirection = (Direction3D)Random.Range(0, 6);
        FlipDirection();
    }

    void Update()
    {
        //Decide if we want to change direction
        if (Random.Range(0f,1f) >= 0.975) { currentDirection = (Direction3D)Random.Range(0, 6); }

        //Check for boundaries
        if (transform.position.x >= GameController.instance.maxPellet.x ||
            transform.position.y >= GameController.instance.maxPellet.y ||
            transform.position.z >= GameController.instance.maxPellet.z ||
            transform.position.x <= GameController.instance.minPellet.x ||
            transform.position.y <= GameController.instance.minPellet.y ||
            transform.position.z <= GameController.instance.minPellet.z) { FlipDirection(); }
    }

    void FlipDirection()
    {
        switch (currentDirection)
        {
            case Direction3D.Forward: currentDirection = Direction3D.Back; movement = Vector3.back; break;
            case Direction3D.Back: currentDirection = Direction3D.Forward; movement = Vector3.forward; break;
            case Direction3D.Up: currentDirection = Direction3D.Down; movement = Vector3.down; break;
            case Direction3D.Down: currentDirection = Direction3D.Up; movement = Vector3.up; break;
            case Direction3D.Left: currentDirection = Direction3D.Right; movement = Vector3.right; break;
            case Direction3D.Right: currentDirection = Direction3D.Left; movement = Vector3.left; break;
        }
        rb.velocity = movement * speed * Time.deltaTime;
    }
}
