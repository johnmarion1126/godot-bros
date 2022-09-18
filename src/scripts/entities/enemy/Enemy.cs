using Godot;
using System;
public class Enemy : KinematicBody2D
{
  const float GRAVITY_SCALE =  Constants.GRAVITY_SCALE;
  const int ENEMY_WALK_SPEED = Constants.ENEMY_WALK_SPEED;
  const int SCREEN_WIDTH = Constants.SCREEN_WIDTH;

  const int PIXEL_LEFT_OFFSET = -50;
  const int PIXEL_RIGHT_OFFSET = 100;

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
    GD.Print(collision);
    if (area.Name == "Player" ) QueueFree(); 
  }

  public override void _PhysicsProcess(float delta)
  {
    if (anim.FlipH) velocity.x = -ENEMY_WALK_SPEED;
    else velocity.x = ENEMY_WALK_SPEED;

    velocity.y += delta * GRAVITY_SCALE;
    MoveAndSlide(velocity, new Vector2(0, -1));

    if (this.Position.x <= PIXEL_LEFT_OFFSET) anim.FlipH = false;
    else if (this.Position.x >= SCREEN_WIDTH - PIXEL_RIGHT_OFFSET) anim.FlipH = true;
  }
}