using Godot;

class Player : KinematicBody2D {
  private IState currentState;

  public Vector2 velocity;
  public AnimatedSprite anim;
  public CollisionShape2D collision;

  public override void _Ready()
  {
    anim = GetNode<AnimatedSprite>("PlayerAnimation");
    velocity = new Vector2();
    collision = GetNode<CollisionShape2D>("Area2D/PlayerCollision");

    currentState = new PlayerBaseState(anim, velocity, collision);
    currentState.enter();
  }

  public void changeState(IState newState) 
  {
    currentState = newState;
    currentState.enter();
  }

  public void onCollisionEnter(Area2D area) {
    currentState.processCollision(area);
  }
  
  public override void _PhysicsProcess(float delta) 
  {
    IState newState = currentState.update(delta);
    if (newState != null) changeState(newState);

    velocity = currentState.getVelocity();
    MoveAndSlide(velocity, new Vector2(0, -1));
  }

}