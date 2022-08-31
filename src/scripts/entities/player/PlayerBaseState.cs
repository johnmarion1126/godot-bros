using Godot;
using System;

class PlayerBaseState : KinematicBody2D {
  protected bool isJumping = false;
  protected float gravity = 800.0f;
  protected int walkSpeed = 250;
  protected int jumpSpeed = -450;

  protected Vector2 velocity;
  protected AnimatedSprite anim;

  public PlayerBaseState(AnimatedSprite anim, Vector2 velocity)
  {
    this.velocity =  velocity;
    this.anim = anim; 
  }

  public virtual void enter() 
  {
    anim.Animation = "Idle";
    velocity.x = 0;
  }

  public Vector2 getVelocity()
  {
    return velocity;
  }

  public virtual PlayerBaseState update(float delta) 
  {
    velocity.y += delta * gravity;

    bool isPressingUp = Input.IsActionPressed("up");
    bool isPressingLeft = Input.IsActionPressed("left");
    bool isPressingRight = Input.IsActionPressed("right");

    if (isPressingLeft || isPressingRight) return new PlayerWalkState(this.anim, this.velocity);
    if (isPressingUp) return new PlayerJumpState(this.anim, this.velocity);
    return null;
  }

  public void onGroundCollision()
  {
    isJumping = false;
  }

}