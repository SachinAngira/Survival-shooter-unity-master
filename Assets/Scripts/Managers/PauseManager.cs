using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {
	
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	public GameObject OnLoadErrorObj;
	Canvas canvas;
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
        if (GameManager.OnGameLoad != Pause)
        {
			GameManager.OnGameLoad += Pause;
		}
		if (GameManager.OnErrorGameLoad != OnLoadError)
		{
			GameManager.OnErrorGameLoad += OnLoadError;
		}

	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}
	
	public void Pause()
	{
		canvas.enabled = !canvas.enabled;
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();
		
	}
	
	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}
		
		else
			
		{
			unpaused.TransitionTo(.01f);
		}
	}


	void OnLoadError()
	{
		OnLoadErrorObj.SetActive(true);
	}
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
