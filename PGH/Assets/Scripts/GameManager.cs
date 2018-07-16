using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour 
{
	private PlayerController playerController;
	private bool gameEnded;
	private GameObject gameOverText;

	// Use this for initialization
	void Start () {
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		gameOverText = GameObject.FindGameObjectWithTag("GameOver");
		gameEnded = false;
		gameOverText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameEnded)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Debug.Log("RRRR");
				Scene level = SceneManager.GetActiveScene();
				SceneManager.LoadScene(level.name);
			}
		}
	}
	public void EndGame()
	{
		gameEnded = true;
		playerController.enabled = false;
		gameOverText.gameObject.SetActive(true);
	}
}
