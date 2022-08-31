using Godot;
using System;

class Player : KinematicBody2D {
  private PlayerBaseState currentState;

  public Vector2 velocity;
  public AnimatedSprite anim;

  public override void _Ready()
  {
    anim = GetNode<AnimatedSprite>("PlayerAnimation");
    velocity = new Vector2();

    currentState = new PlayerBaseState(anim, velocity);
    currentState.enter();
  }

  public void changeState(PlayerBaseState newState) 
  {
    currentState = newState;
    currentState.enter();
  }

  public override void _PhysicsProcess(float delta) 
  {
    PlayerBaseState newState = currentState.update(delta);
    if (newState != null) changeState(newState);
    velocity = currentState.getVelocity();
    MoveAndSlide(velocity, new Vector2(0, -1));
  }

  public void onCollisionEnter(Area2D area) {
    if (area.Name == "Ground" ) currentState.onGroundCollision();
  }
}