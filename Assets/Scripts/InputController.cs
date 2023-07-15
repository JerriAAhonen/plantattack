using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController>
{
	private Vector2 player1Input;
	private Vector2 player2Input;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W)) // Turn upwards
			player1Input = Vector2.up;
		else if (Input.GetKeyDown(KeyCode.A)) // Turn left
			player1Input = Vector2.left;
		else if (Input.GetKeyDown(KeyCode.S)) // Turn downwards
			player1Input = Vector2.down;
		else if (Input.GetKeyDown(KeyCode.D)) // Turn right
			player1Input = Vector2.right;

		if (Input.GetKeyDown(KeyCode.UpArrow)) // Turn upwards
			player2Input = Vector2.up;
		else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Turn left
			player2Input = Vector2.left;
		else if (Input.GetKeyDown(KeyCode.DownArrow)) // Turn downwards
			player2Input = Vector2.down;
		else if (Input.GetKeyDown(KeyCode.RightArrow)) // Turn right
			player2Input = Vector2.right;
	}

	public Vector2 GetInput(int playerIndex)
	{
		return playerIndex switch
		{
			0 => player1Input,
			_ => player2Input,
		};
	}
}
