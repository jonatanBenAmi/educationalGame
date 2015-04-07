using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroundImage : MonoBehaviour {
	public GameObject Obj;
	public Sprite img1, img2, img3;
	public int ChangeBackground;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().sprite = img1;
	}
	
	// Update is called once per frame
	void Update () {
		ChangeBackground = GameObject.Find ("InputField").GetComponent<InputField>().Background;//establish connection to InputField script, and the variable "Background"
		if(ChangeBackground == 1){
			gameObject.GetComponent<SpriteRenderer>().sprite = img2;
		}
		if(ChangeBackground == 2){
			gameObject.GetComponent<SpriteRenderer>().sprite = img2;
		}
	}
}
