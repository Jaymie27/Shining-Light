using Godot;
using System;

public class optionsMenu : Control
{

	public override void _Ready()
	{
		GetNode<Button>("ColorRect/VBoxContainer/BackButton").GrabFocus();
	}

	private void _on_BackButton_pressed()
	{
		GetNode<Button>("../ColorRect/VBoxContainer/StartButton").GrabFocus();
		this.QueueFree();
	}
}



