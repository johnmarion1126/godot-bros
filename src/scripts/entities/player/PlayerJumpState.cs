using Godot;

class PlayerJumpState : PlayerBaseState {

  public PlayerJumpState(AnimatedSprite anim, Vector2 velocity, KinematicBody2D player, int hp): base(anim ,velocity, player, hp)
  {
  }

  public override void enter() 
  {
    velocity.y = JUMP_SPEED;
    anim.Animation = "Jump";
    isJumping = true;
  }

  public override void processCollision(Area2D area)
  {
    if (area.Name == "Ground" ) isJumping = false;
    if (area.Name == "Turtle" && !inInvincible) {
      if (area.Position.y - player.Position.y >= PLAYER_PIXEL_OFFSET) enter();
      else takeDamage();
    }
  }

  public override IState update(float delta) 
  {
    getInput();

    if (isPressingLeft)
    {
      velocity.x = -WALK_SPEED;
    }
    else if (isPressingRight)
    {
      velocity.x = WALK_SPEED;
    } 

    velocity.y += delta * GRAVITY_SCALE;

    if (!isJumping) return new PlayerBaseState(anim, velocity, player, hp);

    if (hp == 0) return new PlayerFaintState(anim, velocity, player, hp);

    return null;
  }
}