using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

	void Update () {
		Vector3 p = Camera.main.transform.position;
		p.y = transform.position.y;
		transform.LookAt (p);
        transform.Rotate(new Vector3(0f,180f,0f));
	}
}