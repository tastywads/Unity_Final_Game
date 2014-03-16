using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour
{
	public Transform playerTransform;

	private PhotonView myPhotonView;
	private Vector3 realPosition;
	private Quaternion realRotation;
	private bool gotFirstUpdate;

	void Awake()
	{
		myPhotonView = photonView;
		realPosition = Vector3.zero;
		realRotation = Quaternion.identity;
	}

	void Update()
	{
		Debug.Log (myPhotonView.isMine);
		if(!(myPhotonView.isMine))
		{
			playerTransform.position = Vector3.Lerp (playerTransform.position, realPosition, 0.1f);
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, realRotation, 0.1f);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			//This is our player. We need to send our actual position to the network
			Debug.Log("IS WRITING");
			stream.SendNext(playerTransform.position);
			stream.SendNext(playerTransform.rotation);
		}
		else
		{
			//This is someone else's player. We need to receive their position

			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();

			if(!gotFirstUpdate)
			{
				playerTransform.position = realPosition;
				playerTransform.rotation = realRotation;
				gotFirstUpdate=true;
			}
		}
	}
}
