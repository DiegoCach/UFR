using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : NetworkManager {

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

		if (GameObject.Find ("Player1") != null) {
			
			player.name = "Player2";
			NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);
		} else {

			player.name = "Player1";

			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}

	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		if (Network.isServer)
			Debug.Log("Local server connection disconnected");
		else
			if (info == NetworkDisconnection.LostConnection)
				Debug.Log("Lost connection to the server");
			else
				Debug.Log("Successfully diconnected from the server");
	}
}
