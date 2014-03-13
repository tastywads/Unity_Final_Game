using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour
{
	PhotonView myPhotonView;
	Vector3 realPosition;
	Quaternion realRotation;

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
			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			//This is our player. We need to send our actual position to the network
			Debug.Log("IS WRITING");
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			//This is someone else's player. We need to receive their position

			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
		}
	}
}
