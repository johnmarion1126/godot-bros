using Godot;
using System;

interface IState {
  void enter();
  // void exit();
  IState update(float delta);
}