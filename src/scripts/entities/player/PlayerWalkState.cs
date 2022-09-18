using Godot;

class PlayerWalkState : PlayerBaseState {

  public PlayerWalkState(AnimatedSprite anim, Vector2 velocity, CollisionShape2D collision): base(anim ,velocity, collision)
  {
  }

  public override void enter() 
  {
    anim.Animation = "Walk";
  }

  public override IState update(float delta)
  {
    getInput();

    if (isPressingLeft)
    {
      velocity.x = -WALK_SPEED;
      anim.FlipH = true;
    }
    else if (isPressingRight)
    {
      velocity.x = WALK_SPEED;
      anim.FlipH = false;
    }
    
    if (isPressingUp)
    {
      return new PlayerJumpState(anim, velocity, collision);
    }
    
    if (!isPressingLeft && !isPressingRight && !isPressingUp)
    {
      return new PlayerBaseState(anim, velocity, collision);
    }

    velocity.y += delta * GRAVITY_SCALE;
    return null;
  }
}