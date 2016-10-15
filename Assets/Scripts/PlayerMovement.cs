using UnityEngine;
using System;
using System.Collections;

enum Direction { Up, Down, Left, Right, };

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid;

    Vector3 movement;
    Vector3 nextSpace;
    Direction nextDirection;

    public float speed = 5f;
    public bool moving = false;

    float startTime;
    float journeyLength;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
    }

    void Move(float h, float v)
    {
        if (!moving && h != 0)
        {
            moving = true;
            if (h > 0)
                nextDirection = Direction.Right;
            else
                nextDirection = Direction.Left;
            nextSpace = CenterVector(new Vector3(tPosition.x + h, tPosition.y, tPosition.z));
            startTime = Time.time;
            journeyLength = Vector3.Distance(tPosition, nextSpace);
        }
        else if (!moving && v != 0)
        {
            moving = true;
            if (v > 0)
                nextDirection = Direction.Up;
            else
                nextDirection = Direction.Down;
            nextSpace = CenterVector(new Vector3(tPosition.x, tPosition.y, tPosition.z + v));
            startTime = Time.time;
            journeyLength = Vector3.Distance(tPosition, nextSpace);
        }

        if (moving)
        {
            switch (nextDirection)
            {
                case Direction.Up:
                    movement.Set(0f, 0f, 1);
                    break;
                case Direction.Down:
                    movement.Set(0f, 0f, -1);
                    break;
                case Direction.Left:
                    movement.Set(-1, 0f, 0f);
                    break;
                case Direction.Right:
                    movement.Set(1, 0f, 0f);
                    break;
            }
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(tPosition, nextSpace, fracJourney);
        }
        if (tPosition == nextSpace)
            moving = false;
    }

    Vector3 CenterVector(Vector3 vec)
    {
        return new Vector3(RoundToNearestHalf(vec.x), vec.y, RoundToNearestHalf(vec.z));
    }

    public static float RoundToNearestHalf(float num)
    {
        return Mathf.Round(num * 2f) / 2f;
    }

    public Vector3 tPosition
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public Vector3 rPosition
    {
        get
        {
            return rigid.position;
        }
        set
        {
            rigid.position = value;
        }
    }
}
