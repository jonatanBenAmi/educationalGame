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
			operatorSign = " + "; //you can write * or - depending on the type of equation
			break;
		case 2:
			operatorSign = " + ";
			break;
		case 3:
			operatorSign = " + ";
			break;
		case 4:
			operatorSign = " + ";
			break;
		case 5:
			operatorSign = " + ";
			break;
		case 6:
			operatorSign = " + ";
			break;
		case 7:
			operatorSign = " + ";
			break;
		case 8:
			operatorSign = " - ";
			break;
		case 9:
			operatorSign = " - ";
			break;
		case 10:
			operatorSign = " - ";
			break;
		case 11:
			operatorSign = " - ";
			break;
		case 12:
			operatorSign = " - ";
			break;
		case 13:
			operatorSign = " - ";
			break;
		case 14:
			operatorSign = " - ";
			break;
		case 15:
			operatorSign = " - ";
			break;
		case 16:
			operatorSign = " - ";
			break;
		case 17:
			operatorSign = " * ";
			break;
		case 18:
			operatorSign = " * ";
			break;
		case 19:
			operatorSign = " * ";
			break;
		case 20:
			operatorSign = " * ";
			break;
		case 22:
			operatorSign = " * ";
			break;
		case 23:
			operatorSign = " * ";
			break;
		case 24:
			operatorSign = " * ";
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
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 2:
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 3:
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 4:
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 5:
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 6:
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 7:
			calculateResultPlus();
			equationText.text = "";
			items.Clear();
			break;
		case 8:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 9:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 10:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 11:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 12:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 13:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 14:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 15:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 16:
			calculateResultMinus();
			equationText.text = "";
			items.Clear();
			break;
		case 17:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 18:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 19:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 20:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 21:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 22:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 23:
			calculateResultMultiply();
			equationText.text = "";
			items.Clear();
			break;
		case 24:
			calculateResultMultiply();
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
		if(items.Count<=itemsToScan){  //we want no more than two items displayed in our equation.
		equationText.text = "ligning: ";
		for (int i = 0 ; i < items.Count; i++){ //ass long as my i is less than my item count, update my equationtext.
			if(i != 0 && i <= itemsToScan -1){ //if i is less not 0 and less than 2. write plus
				equationText.text += operatorSign;
			}
			equationText.text +=  items[i];
		}
		if(items.Count == itemsToScan){
			equationText.text += " =" ;
		}
		}
	}
	
	public void calculateResultPlus(){

		result = 0;
		for (int i = 0 ; i < itemsToScan ; i++){ //determines how many items should be included in the calculation
			result += items[i];
		}
	}
	public void calculateResultMultiply(){
		operatorSign = " * ";
		result = items[0];
		for (int i = 1 ; i < itemsToScan ; i++){
			result *= items[i];
		}
	}
	public void calculateResultMinus(){
		operatorSign = " - ";
		result = items[0];
		for (int i = 1 ; i < itemsToScan ; i++){
			result -= items[i];
		}
	}
	
}