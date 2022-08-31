using Godot;
using System;

class PlayerWalkState : PlayerBaseState {

  public PlayerWalkState(AnimatedSprite anim, Vector2 velocity): base(anim ,velocity)
  {
  }

  public override void enter() 
  {
    anim.Animation = "Walk";
  }

  public override PlayerBaseState update(float delta)
  {
    bool isPressingUp = Input.IsActionPressed("up");
    bool isPressingLeft = Input.IsActionPressed("left");
    bool isPressingRight = Input.IsActionPressed("right");

    if (isPressingLeft)
    {
      velocity.x = -walkSpeed;
      anim.FlipH = true;
    }
    else if (isPressingRight)
    {
      velocity.x = walkSpeed;
      anim.FlipH = false;
    }
    else if (isPressingUp)
    {
      return new PlayerJumpState(anim, velocity);
    }
    else
    {
      return new PlayerBaseState(anim, velocity);
    }

    velocity.y += delta * gravity;
    return null;
  }
}