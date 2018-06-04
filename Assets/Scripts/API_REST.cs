using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.IO;

namespace APIMethods
{
	public static class APIHelper
	{
		public enum State {started, finished, error, aborted, successful};

		public static State requestState;

		private const string urlBase = "http://h2744356.stratoserver.net/sapiens/robotsGameApi/public/index.php/";

		public static JSONResponse response = null;

		public static IEnumerator GetRequest(string route, string parameters) 
		{
			requestState = State.started;
			UnityWebRequest request  = UnityWebRequest.Get(
				urlBase + route + "?" + parameters
			);

			request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

			if(PlayerPrefs.GetString("data") != null || PlayerPrefs.GetString("data") != "")
			{
				string token = PlayerPrefs.GetString ("token");

				if (token != null || token != "") {
					request.SetRequestHeader ("Authorization", token);
				}
				else 
				{
					token = PlayerPrefs.GetString ("data");
					request.SetRequestHeader ("Authorization", token);
				}
			}

			yield return request.Send();

			if(request .isNetworkError) 
			{
				requestState = State.error;
				Debug.Log(request.error);
			}
			else 
			{
				response = JSONHelper.JSONToObject (request.downloadHandler.text);

				JSONDataUser dataUser = null;

				try
				{
					dataUser = JSONHelper.JSONToDataUser (response.data);
				}
				catch
				{
					//requestState = State.aborted;
					Debug.Log ("No hay datos de usuario en la respuesta");
				}
				finally
				{
					if (response.code == 200) 
					{
						Debug.Log ("ok");
						PlayerPrefs.SetString ("data", response.data);

						if (dataUser != null && dataUser.token != null)
						{
							PlayerPrefs.SetString ("token", dataUser.token);

							if(dataUser.urlPhoto != null && dataUser.urlPhoto != ""){
								PlayerPrefs.SetString ("urlPhoto", dataUser.urlPhoto);
							}

						}
						else 
						{
							if(response.data.Contains("ey")){
								Debug.Log ("data:" + PlayerPrefs.GetString ("data"));
								PlayerPrefs.SetString ("token", response.data);
							}

						}
						requestState = State.successful;
					}
					else
					{
						requestState = State.aborted;
					}
				}
			}
		}

		public static IEnumerator LoginRequest(string route, string parameters) 
		{
			requestState = State.started;
			UnityWebRequest request  = UnityWebRequest.Get(
				urlBase + route + "?" + parameters
			);

			request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

			if(PlayerPrefs.GetString("data") != null || PlayerPrefs.GetString("data") != "")
			{
				string token = PlayerPrefs.GetString ("token");

				if(token != null || token != "")
				{
					request.SetRequestHeader("Authorization", token);
				}
			}

			yield return request.Send();

			if(request .isNetworkError) 
			{
				requestState = State.error;
				Debug.Log(request.error);
			}
			else 
			{
				response = JSONHelper.JSONToObject (request.downloadHandler.text);

				JSONDataUser dataUser = null;

				try
				{
					dataUser = JSONHelper.JSONToDataUser (response.data);
				}
				catch
				{
					//requestState = State.aborted;
					Debug.Log ("No hay datos de usuario en la respuesta");
				}
				finally
				{
					if (response.code == 200) 
					{
						PlayerPrefs.SetString ("data", response.data);

						if (dataUser != null && dataUser.token != null)
						{
							PlayerPrefs.SetString ("token", dataUser.token);

							if(dataUser.urlPhoto != null && dataUser.urlPhoto != ""){
								PlayerPrefs.SetString ("urlPhoto", dataUser.urlPhoto);
							}

						}

						requestState = State.successful;
					}
					else
					{
						requestState = State.aborted;
					}
				}
			}
		}

		public static IEnumerator PostRequest(string route, string parameters) 
		{
			requestState = State.started;
			string url = urlBase + route;

			UnityWebRequest request = new UnityWebRequest(url, "POST");

			byte[] bodyRaw = Encoding.UTF8.GetBytes(parameters);
			request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

			if(PlayerPrefs.GetString("data") != null || PlayerPrefs.GetString("data") != "")
			{
				string token = PlayerPrefs.GetString ("token");

				if(token != null || token != "")
				{
					request.SetRequestHeader("Authorization", token);
				}
			}

			yield return request.Send();

			if (request.error != null) 
			{
				Debug.Log("request error: " + request.error);
				requestState = State.error;
			}
			else 
			{
				response = JSONHelper.JSONToObject (request.downloadHandler.text);

				if (response.code == 200) 
				{
					Debug.Log ("ok");
					PlayerPrefs.SetString ("data", response.data);
					requestState = State.successful;
				}
				else
				{
					requestState = State.aborted;
				}
			}
		}

		public static IEnumerator PostPhoto(string route, string parameters) 
		{
			requestState = State.started;
			string url = urlBase + route;

			UnityWebRequest request = new UnityWebRequest(url, "POST");

			byte[] bodyRaw = Encoding.UTF8.GetBytes(parameters);
			request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

			//string downloadedPhoto = Application.persistentDataPath + "/userPicture.png";
			//string downloadedPhoto = Application.dataPath+ "/userPicture.png";
			//Debug.Log(downloadedPhoto);
			//File.WriteAllBytes (downloadedPhoto, request.b);
		
			//WWW www = new WWW (url);
			//yield return www;
			//File.WriteAllBytes (downloadedPhoto, bodyRaw);

			if(PlayerPrefs.GetString("data") != null || PlayerPrefs.GetString("data") != "")
			{
				string token = PlayerPrefs.GetString ("token");

				if(token != null || token != "")
				{
					request.SetRequestHeader("Authorization", token);
				}
			}

			yield return request.Send();

			if (request.error != null) 
			{
				Debug.Log("request error: " + request.error);
				requestState = State.error;
			}
			else 
			{
				response = JSONHelper.JSONToObject (request.downloadHandler.text);

				if (response.code == 200) 
				{
					PlayerPrefs.SetString ("urlPhoto", response.data);
					Debug.Log(response.data);
					/*WWW www = new WWW (response.data);
					yield return www;
					File.WriteAllBytes (downloadedPhoto, www.bytes);*/
					requestState = State.successful;
				}
				else
				{
					requestState = State.aborted;
				}
			}
		}
	}

	public static class JSONHelper
	{
		public static JSONResponse JSONToObject (string json)
		{
			JSONResponse response = new JSONResponse ();
			response = JsonUtility.FromJson<JSONResponse>(json);
			return response;
		}

		public static JSONDataUser JSONToDataUser (string json)
		{
			JSONDataUser response = new JSONDataUser ();
			response = JsonUtility.FromJson<JSONDataUser>(json);
			return response;
		}
	}
}