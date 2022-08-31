using Godot;
using System;

// TODO: Create an entity parent class
// TODO: Pass player object (entity) to state

class PlayerJumpState : PlayerBaseState {

  public PlayerJumpState(AnimatedSprite anim, Vector2 velocity): base(anim ,velocity)
  {
  }

  public override void enter() 
  {
    velocity.y = jumpSpeed;
    anim.Animation = "Jump";
    isJumping = true;
  }

  public override IState update(float delta) 
  {
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

    velocity.y += delta * gravity;

    if (!isJumping) return new PlayerBaseState(this.anim, this.velocity);
    return null;
  }
}