using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController
{
	private readonly int[] scores;

	public ScoreController(int playerCount)
	{
		scores = new int[playerCount];
	}

	public int GetScore(int playerIndex) => scores[playerIndex];
	public void SetScore(int playerIndex, int score) => scores[playerIndex] = score;
	public void AddScore(int playerIndex) => scores[playerIndex] = scores[playerIndex] + 1;
	public void SubstractScore(int playerIndex) => scores[playerIndex] = scores[playerIndex] - 1;
}
