using Godot;
using System;

// TODO: Slow down movement speed
// TODO: Move constants to one file
// TODO: Flip turtle sprite and direction after hitting wall

public class Enemy : KinematicBody2D
{
  const float GRAVITY_SCALE =  800.0f;
  int ENEMY_WALK_SPEED = 150;

  Vector2 velocity;
  private AnimatedSprite anim;

  public override void _Ready()
  {
    anim = GetNode<AnimatedSprite>("EnemyAnimation");
    velocity = new Vector2();
  }

  public override void _PhysicsProcess(float delta)
  {
    if (anim.FlipH) {
      GD.Print("TRUE");
      velocity.x = -ENEMY_WALK_SPEED;
    }
    else {
      GD.Print("FALSE");
      velocity.x = ENEMY_WALK_SPEED;
    }

    velocity.y += delta * GRAVITY_SCALE;
    MoveAndSlide(velocity, new Vector2(0, -1));

    float screenBorder = GetViewport().GetVisibleRect().Size.x;

    if (this.Position.x <= -50) {
      anim.FlipH = false;
    } else if (this.Position.x >= screenBorder - 100) {
      GD.Print("OUT");
      anim.FlipH = true;
    }
  }
}