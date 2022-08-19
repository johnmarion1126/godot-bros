using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{
    const float gravity = 800.0f;
    const int walkSpeed = 250;
    const int jumpSpeed = -450;

    Vector2 velocity = new Vector2();

    public void getInput()
    {
        bool isPressingUp = Input.IsActionJustPressed("up");
        bool isPressingLeft = Input.IsActionPressed("left");
        bool isPressingRight = Input.IsActionPressed("right");

        if (isPressingLeft)
        {
            velocity.x = -walkSpeed;
        }
        else if (isPressingRight)
        {
            velocity.x = walkSpeed;
        }
        else
        {
            velocity.x = 0;
        }

        if (isPressingUp)
        {
            velocity.y = jumpSpeed;
        }
    }

  public override void _PhysicsProcess(float delta)
  {
    velocity.y += delta * gravity;
    getInput();
    MoveAndSlide(velocity, new Vector2(0, -1));
  }
}
