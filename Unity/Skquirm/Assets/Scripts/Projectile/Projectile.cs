using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected PlayerController origin;
	protected GameObject target;

	public void SetInfo(GameObject targetObj, PlayerController controller){
		target = targetObj;
		origin = controller;
	}
}
