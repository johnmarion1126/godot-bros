using Godot;

class PlayerFaintState : PlayerBaseState {

  public PlayerFaintState(AnimatedSprite anim, Vector2 velocity, KinematicBody2D player, int hp): base(anim ,velocity, player, hp)
  {
  }

  public override void enter() 
  {
  }

  public override IState update(float delta) 
  {
    velocity.y += delta * GRAVITY_SCALE;
    return new PlayerFaintState(anim, velocity, player, hp);
  }
}
