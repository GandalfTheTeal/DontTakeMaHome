using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
	public Image[] HealthBars;

	public Color[] colors;

	public GameObject[] UIElements;


	private void Start()
	{
		for (int i = 0;  i <=GameManager.numPlayers-1; i++)
		{
			if(GameManager.players != null)
			{
				GameManager.players[i].GetComponentInChildren<HealthManager>().healthBar = HealthBars[i];
				GameManager.players[i].GetComponentInChildren<CharacterAnimator>().scarf.color = colors[i];
				UIElements[i].SetActive(true);
			}
		}
	}




}
