using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScanObject : MonoBehaviour {
	
	private List<int> items = new List<int>();
	
	public int result;
	public int itemsToScan = 2;
	public int scenario;
	string operatorSign = " + ";
	
	public Text displayText;  //will directly associate the reference in the inspector when we drag in our text object.
	public Text equationText; //will directly associate the reference in the inspector when we drag in our text object.
	
	public AudioClip bipSound;
	private AudioSource source;
	
	// Use this for initialization
	public void Start () {
		source = GetComponent<AudioSource>();
	}
	
	public void Update () {
		switch (scenario)
		{
		case 0:
			operatorSign = " + ";
			break;
		case 1:
			operatorSign = " * ";
			break;
		case 2:
			operatorSign = " - ";
			break;
		default:
			break;
		}
	}
	
	public void OnTriggerExit(Collider other)//Collider function
	{ 
		if(other.tag == "item"){
			if(items.Count <= itemsToScan){
				items.Add(other.GetComponent<Item>().price);
			}
			setDisplayText (other.GetComponent<Item>().price , other.GetComponent<Item>().name);
			source.PlayOneShot(bipSound,1F);

			setEquationText();

		}              
	}
	
	public int runGame(){
		switch (scenario)
		{
		case 0:
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 1:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 2:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 3:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		default:
			break;
		}
		return result;
	}
	
	
	public void setDisplayText(int value, string name){
		displayText.text = "item: " + name + " kroner: " + value;
	}
	
	public void setEquationText(){
		equationText.text = "ligning: ";
		for (int i = 0 ; i < items.Count; i++){
			if(i != 0 && i <= itemsToScan-1 ){
				equationText.text += operatorSign;
			}
			equationText.text +=  items[i];
		}
		if(items.Count == itemsToScan){
			equationText.text += " =" ;
		}
	}
	
	public void calculateResultPlus(){

		result = 0;
		for (int i = 0 ; i < items.Count ; i++){
			result += items[i];
		}
	}
	public void calculateResultMultiply(){
		operatorSign = " * ";
		result = items[0];
		for (int i = 1 ; i < items.Count ; i++){
			result *= items[i];
		}
	}
	public void calculateResultMinus(){
		operatorSign = " - ";
		result = items[0];
		for (int i = 1 ; i < items.Count ; i++){
			result -= items[i];
		}
	}
	
}