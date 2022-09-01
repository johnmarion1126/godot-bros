using Godot;

class PlayerJumpState : PlayerBaseState {

  public PlayerJumpState(AnimatedSprite anim, Vector2 velocity): base(anim ,velocity)
  {
  }

  public override void enter() 
  {
    velocity.y = JUMP_SPEED;
    anim.Animation = "Jump";
    isJumping = true;
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

    if (!isJumping) return new PlayerBaseState(this.anim, this.velocity);
    return null;
  }
}