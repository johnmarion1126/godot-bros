using Godot;
using System;
public class Enemy : KinematicBody2D
{
  const int ENEMY_WALK_SPEED = Constants.ENEMY_WALK_SPEED;
  const int SCREEN_WIDTH = Constants.SCREEN_WIDTH;
  const float GRAVITY_SCALE =  Constants.GRAVITY_SCALE;
  const float JUMP_SPEED = Constants.JUMP_SPEED;

  const int PIXEL_LEFT_OFFSET = -40;
  const int PIXEL_RIGHT_OFFSET = 100;
  const int PIXEL_TOP_OFFSET = 43;

  bool fainted = false;

  Vector2 velocity;
  private AnimatedSprite anim;
  private CollisionShape2D collision;

  public override void _Ready()
  {
    anim = GetNode<AnimatedSprite>("EnemyAnimation");
    collision = GetNode<CollisionShape2D>("EnemyCollisionShape2D");
    velocity = new Vector2();
  }

  public void onCollisionEnter(Area2D area) {
    if (fainted) return;

    if (area.Position.y <= this.Position.y - PIXEL_TOP_OFFSET) {
      this.SetCollisionMaskBit(0, false);
      this.velocity.y = JUMP_SPEED;
      anim.FlipV = true;
      fainted = true;
    }
  }

  public override void _PhysicsProcess(float delta)
  {
    if (anim.FlipH) velocity.x = -ENEMY_WALK_SPEED;
    else velocity.x = ENEMY_WALK_SPEED;

    velocity.y += delta * GRAVITY_SCALE;
    MoveAndSlide(velocity, new Vector2(0, -1));

    if (this.Position.x <= PIXEL_LEFT_OFFSET && fainted == false) anim.FlipH = false;
    else if (this.Position.x >= SCREEN_WIDTH - PIXEL_RIGHT_OFFSET && fainted == false) anim.FlipH = true;
  }
}