using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class makeMapObject : MonoBehaviour {

	public Image image;

	// Use this for initialization
	void Start () 
	{
		MiniMapController.RegisterMapObject(this.gameObject, image);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy()
	{
		MiniMapController.RemoveMapObject(this.gameObject);
	}
}
