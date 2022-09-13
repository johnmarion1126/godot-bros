using Godot;
using System;
public class Enemy : KinematicBody2D
{
  const float GRAVITY_SCALE =  Constants.GRAVITY_SCALE;
  const int ENEMY_WALK_SPEED = Constants.ENEMY_WALK_SPEED;

  Vector2 velocity;
  private AnimatedSprite anim;

  public override void _Ready()
  {
    anim = GetNode<AnimatedSprite>("EnemyAnimation");
    velocity = new Vector2();
  }

  public override void _PhysicsProcess(float delta)
  {
    if (anim.FlipH) velocity.x = -ENEMY_WALK_SPEED;
    else velocity.x = ENEMY_WALK_SPEED;

    velocity.y += delta * GRAVITY_SCALE;
    MoveAndSlide(velocity, new Vector2(0, -1));

    float screenBorder = GetViewport().GetVisibleRect().Size.x;

    if (this.Position.x <= -50) anim.FlipH = false;
    else if (this.Position.x >= screenBorder - 100) anim.FlipH = true;
  }
}