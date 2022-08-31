using Godot;
using System;

interface IState {
  void enter();
  // void exit();
  IState update(float delta);

  Vector2 getVelocity();
  void processCollision(Area2D area);
}