using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{
    private AnimatedSprite anim;
    private bool isJumping = false;

    const float gravity = 800.0f;
    const int walkSpeed = 250;
    const int jumpSpeed = -450;

    Vector2 velocity = new Vector2();

    public override void _Ready() 
    {
        anim = GetNode<AnimatedSprite>("PlayerAnimation");
    }

    public void getInput()
    {
        bool isPressingUp = Input.IsActionJustPressed("up");
        bool isPressingLeft = Input.IsActionPressed("left");
        bool isPressingRight = Input.IsActionPressed("right");

        if (isPressingLeft)
        {
            velocity.x = -walkSpeed;
            anim.Animation = "Walk";
            anim.FlipH = true;
        }
        else if (isPressingRight)
        {
            velocity.x = walkSpeed;
            anim.Animation = "Walk";
            anim.FlipH = false;
        }
        else if (!isJumping)
        {
            anim.Animation = "Idle";
            velocity.x = 0;
        }

        if (isPressingUp && !isJumping)
        {
            velocity.y = jumpSpeed;
            isJumping = true;
        }

        if (isJumping) anim.Animation = "Jump";
    }

  public override void _PhysicsProcess(float delta)
  {
    velocity.y += delta * gravity;
    getInput();
    MoveAndSlide(velocity, new Vector2(0, -1));
  }

  public void onCollisionEnter(Area2D area) {
    if (area.Name == "Ground" ) isJumping = false;
  }
}
