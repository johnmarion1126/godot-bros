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

  protected bool fainting = false;

  protected const int PLAYER_PIXEL_OFFSET = 40;

  protected Vector2 velocity;
  protected AnimatedSprite anim;
  protected KinematicBody2D player;
  protected int hp;

  public PlayerBaseState(AnimatedSprite anim, Vector2 velocity, KinematicBody2D player, int hp)
  {
    this.velocity =  velocity;
    this.anim = anim; 
    this.player = player;
    this.hp = hp;
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

  private void faint()
  {
    velocity.y = JUMP_SPEED;
    anim.Animation = "Faint";
    player.SetCollisionMaskBit(0, false);
  }

  public async void takeDamage()
  {
    hp -= 1;
    if (hp == 0) faint();

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

  public virtual void processCollision(Area2D area)
  {
    if (area.Name == "Ground" ) isJumping = false;
    if (area.Name == "Turtle" && !inInvincible) takeDamage();
  }

  public virtual IState update(float delta) 
  {
    velocity.y += delta * GRAVITY_SCALE;
    getInput();

    if (isPressingLeft || isPressingRight) return new PlayerWalkState(this.anim, this.velocity, this.player, this.hp);
    if (isPressingUp) return new PlayerJumpState(this.anim, this.velocity, this.player, this.hp);

    if (hp == 0) return new PlayerFaintState(this.anim, this.velocity, this.player, this.hp);

    return null;
  }
}