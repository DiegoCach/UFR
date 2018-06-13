using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class TrampaBombilla : NetworkBehaviour
{
    [SyncVar]
    public bool activado = false;
    public bool activadoS = false;

    // Update is called once per frame
    void Update()
    {
        if (activado == true)
        {
            CmdActivateLightning();
        }
        if (activadoS == true)
        {
            CmdActivateWater();
        }

    }

    [Command]
    void CmdActivateLightning()
    {
        RpcActiveLigth();
    }

    [Command]
    void CmdActivateWater()
    {
        RpcActiveWater();
    }

    [ClientRpc]
    void RpcActiveLigth()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        activado = false;
    }
    [ClientRpc]
    void RpcActiveWater()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        activadoS = false;
    }

}
