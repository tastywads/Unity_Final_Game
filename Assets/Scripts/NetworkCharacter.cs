using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour
{
	public Transform myTransform;

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
		if(myPhotonView.isMine)
		{
			Debug.Log ("MINE");
		}

		if(!(myPhotonView.isMine))
		{
			Debug.Log("NOT MINE");
			myTransform.position = Vector3.Lerp (myTransform.position, realPosition, 0.1f);
			myTransform.rotation = Quaternion.Lerp (myTransform.rotation, realRotation, 0.1f);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		Debug.Log ("IN SERIALIZE");
		if(stream.isWriting)
		{
			//This is our player. We need to send our actual position to the network
			Debug.Log ("WRITING");
			stream.SendNext(myTransform.position);
			stream.SendNext(myTransform.rotation);
		}
		else
		{
			//This is someone else's player. We need to receive their position
			Debug.Log ("SENDING");
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
		}
	}
}
