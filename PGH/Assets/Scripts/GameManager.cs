using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour 
{
	private PlayerController playerController;
	private Rigidbody2D playerbody;
	private bool gameEnded;
	private bool levelCompleted;
	private GameObject gameOverText;
	private GameObject levelFinishedText;

	// Use this for initialization
	void Start () 
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		playerbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		gameOverText = GameObject.FindGameObjectWithTag("GameOver");
		levelFinishedText = GameObject.FindGameObjectWithTag("LevelFinished");
		gameEnded = false;
		levelCompleted = false;
		gameOverText.gameObject.SetActive(false);
		levelFinishedText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameEnded)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Scene level = SceneManager.GetActiveScene();
				SceneManager.LoadScene(level.name);
			}
		}
		else if (levelCompleted)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
	}
	public void EndGame ()
	{
		gameEnded = true;
		playerController.enabled = false;
		gameOverText.gameObject.SetActive(true);
	}
	public void FinishLevel ()
	{
		levelCompleted = true;
		playerController.enabled = false;
		levelFinishedText.gameObject.SetActive(true);
		StartCoroutine("StopPlayerMovement");
	}
	IEnumerator StopPlayerMovement ()
	{
		while (Mathf.Abs(playerbody.velocity.x) > 0)
		{
			playerbody.velocity = new Vector2(playerbody.velocity.x - Time.deltaTime, playerbody.velocity.y - Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
}
