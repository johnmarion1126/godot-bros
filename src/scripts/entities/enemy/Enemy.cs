using Godot;
using System;

public class Enemy : KinematicBody2D
{
  const float GRAVITY_SCALE =  800.0f;
  const int WALK_SPEED = 250;

  Vector2 velocity;

  public override void _Ready()
  {
    velocity = new Vector2();
  }

  public override void _PhysicsProcess(float delta)
  {
    velocity.x = -WALK_SPEED;
    velocity.y += delta * GRAVITY_SCALE;
    MoveAndSlide(velocity, new Vector2(0, -1));
  }
}