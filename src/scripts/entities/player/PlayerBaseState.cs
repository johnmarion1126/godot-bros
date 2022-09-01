using Godot;

class PlayerBaseState : KinematicBody2D, IState {
  protected float GRAVITY_SCALE = 800.0f;
  protected int WALK_SPEED = 250;
  protected int JUMP_SPEED = -450;

  protected bool isJumping = false;
  protected bool isPressingLeft;
  protected bool isPressingRight;
  protected bool isPressingUp;

  protected Vector2 velocity;
  protected AnimatedSprite anim;

  public PlayerBaseState(AnimatedSprite anim, Vector2 velocity)
  {
    this.velocity =  velocity;
    this.anim = anim; 
  }

  public virtual void enter() 
  {
    anim.Animation = "Idle";
    velocity.x = 0;
  }

  public Vector2 getVelocity()
  {
    return velocity;
  }

  protected void getInput()
  {
    isPressingUp = Input.IsActionPressed("up");
    isPressingLeft = Input.IsActionPressed("left");
    isPressingRight = Input.IsActionPressed("right");
  }

  public void processCollision(Area2D area)
  {
    if (area.Name == "Ground" ) isJumping = false;
  }

  public virtual IState update(float delta) 
  {
    velocity.y += delta * GRAVITY_SCALE;
    getInput();

    if (isPressingLeft || isPressingRight) return new PlayerWalkState(this.anim, this.velocity);
    if (isPressingUp) return new PlayerJumpState(this.anim, this.velocity);
    return null;
  }


}