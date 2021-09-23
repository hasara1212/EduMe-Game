﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public static class ButtonExtension
{
	public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener (delegate() {
			OnClick (param);
		});
	}
}

public class Demo : MonoBehaviour
{
	[Serializable]
	public struct Game
	{
		public string username;
		public string score;
		public string sname;
		public Sprite Icon;
		public string IconUrl;
	}

	Game[] allGames;
	[SerializeField] Sprite defaultIcon;

	void Start ()
	{
		//fetch data from Json
		StartCoroutine (GetGames ());
	}

	void DrawUI ()
	{
		GameObject buttonTemplate = transform.GetChild (0).gameObject;
		GameObject g;

		int N = allGames.Length;

		for (int i = 0; i < N; i++) {
			g = Instantiate (buttonTemplate, transform);
			g.transform.GetChild (0).GetComponent <Image> ().sprite = allGames [i].Icon;
			g.transform.GetChild (1).GetComponent <Text> ().text = allGames [i].username;
			g.transform.GetChild (2).GetComponent <Text> ().text = allGames [i].score;
			g.transform.GetChild (3).GetComponent <Text> ().text = allGames [i].sname;

			g.GetComponent <Button> ().AddEventListener (i, ItemClicked);
		}

		Destroy (buttonTemplate);
	}

	void ItemClicked (int itemIndex)
	{
		Debug.Log ("name " + allGames [itemIndex].username);
	}

	//***************************************************
	IEnumerator GetGames ()
	{
		WWWForm form= new WWWForm();
        form.AddField("name", DBManager.tusername);
        form.AddField("classname", DBManager.classname);
		
		UnityWebRequest request = UnityWebRequest.Post("https://edumeuwu.000webhostapp.com/getdatabyteacher.php",form);
		yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        else {
            allGames = JsonHelper.GetArray<Game> (request.downloadHandler.text);
				StartCoroutine (GetGamesIcones ());
        }

		
	}

	IEnumerator GetGamesIcones ()
	{
		for (int i = 0; i < allGames.Length; i++) {
			WWW w = new WWW (allGames [i].IconUrl);
			yield return w;

			if (w.error != null) {
				//error
				//show default image
				allGames [i].Icon = defaultIcon;
			} else {
				if (w.isDone) {
					Texture2D tx = w.texture;
					allGames [i].Icon = Sprite.Create (tx, new Rect (0f, 0f, tx.width, tx.height), Vector2.zero, 10f);
				}
			}
		}

		DrawUI ();	
	}

	public void retrydemo()
	{
      UnityEngine.SceneManagement.SceneManager.LoadScene(8);
	}

}
