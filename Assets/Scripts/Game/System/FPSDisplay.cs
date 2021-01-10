using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
	[Header("Resolution Target")]
	public bool hasTargetResolution;
	public int width;
	public int height;

	[Header("FPS Target")]
	public bool showFPS;
	public bool hasTargetFPS;
	public int targetFPS;

	[Header("Font Settings")]
	[Range(0, 10)]
	public int fontSize;
	public Color cor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public Font font;
	public TextAnchor textPosition;

	float deltaTime = 0.0f;

	void Start()
	{
		if (hasTargetFPS)
			Application.targetFrameRate = targetFPS;

		if (hasTargetResolution)
			Screen.SetResolution(width, height, true);
	}

	void Update()
	{
		if(showFPS)
			deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

		if (Input.GetKeyDown(KeyCode.KeypadPlus))
			Time.timeScale += 0.5f;
		else if (Input.GetKeyDown(KeyCode.KeypadMinus))
			Time.timeScale -= 0.5f;
		else if (Input.GetKeyDown(KeyCode.F1))
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
	}

	void OnGUI()
	{
		if (showFPS)
		{
			int w = Screen.width, h = Screen.height;

			GUIStyle style = new GUIStyle();

			Rect rect = new Rect(0, 0, w, h * 2 / 100);
			style.alignment = textPosition;
			style.fontSize = (h * 2 / 100) * fontSize;
			style.normal.textColor = cor;
			style.font = font;
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
			GUI.Label(rect, text, style);
		}
	}
}

//HD 720p	(1280 × 720)
//Full HD	(1920 x 1080)
//4K UHD	(3840 x 2160)
