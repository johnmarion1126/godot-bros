using Godot;
using System.Threading.Tasks;

class PlayerBaseState : KinematicBody2D, IState {
  protected float GRAVITY_SCALE = Constants.GRAVITY_SCALE;
  protected int WALK_SPEED = Constants.WALK_SPEED;
  protected int JUMP_SPEED = Constants.JUMP_SPEED;

  protected bool inInvincible = false;
  protected bool isJumping = false;
  protected bool isPressingLeft;
  protected bool isPressingRight;
  protected bool isPressingUp;

  protected Vector2 velocity;
  protected AnimatedSprite anim;
  protected CollisionShape2D collision;

  public PlayerBaseState(AnimatedSprite anim, Vector2 velocity, CollisionShape2D collision)
  {
    this.velocity =  velocity;
    this.anim = anim; 
    this.collision = collision;
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

  public async void takeDamage()
  {
    inInvincible = true;
    for (int i = 0; i < 3; i += 1)
    {
      anim.Modulate = new Color(0, 0, 0, 0);
      await Task.Delay(150);
      anim.Modulate = new Color(1, 1, 1, 1);
      await Task.Delay(150);
    }
    inInvincible = false;
  }

  public void processCollision(Area2D area)
  {
    if (area.Name == "Ground" ) isJumping = false;
    if (area.Name == "Turtle" && !inInvincible) takeDamage();
  }

  public virtual IState update(float delta) 
  {
    velocity.y += delta * GRAVITY_SCALE;
    getInput();

    if (isPressingLeft || isPressingRight) return new PlayerWalkState(this.anim, this.velocity, this.collision);
    if (isPressingUp) return new PlayerJumpState(this.anim, this.velocity, this.collision);
    return null;
  }
}