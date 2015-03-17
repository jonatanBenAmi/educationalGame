using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public class InputField : MonoBehaviour {
	
	
	public GameObject Obj; //we will have to establish a connection to the scanner object
	//will be used to store the "wrong" sound
	public AudioClip buzz;
	//will be used to spawn new objects for a new equation
	public Transform spawnPoint1;
	//audio clip used to store the "right" sound
	public AudioClip applaus;
	//integer that will be used to determine which scenario/equation to load
	public int scenario = 0;
	//public AudioClip applaus;
	public int numberGiven;
	//we will a reference to the AnswerGiven
	public string text2String;
	
	public Text wrongOrRight;
	//array to store items to calculate with;
	public string[] gameItems;
	public List<GameObject> spawnedItems = new List<GameObject>();
	
	public Text answerGiven; // public so that we can see it in the inspector. // a reference to the AnswerGiven
	// Use this for initialization
	private Text placeholderText; // a reference to the placeholder for our input
	
	private AudioSource source;
	//private AudioSource source2;
	void Start () {
		answerGiven = GameObject.Find("AnswerGiven").GetComponent<Text>();
		placeholderText = GameObject.Find("Placeholder").GetComponent<Text>();
		Obj = GameObject.Find ("scanner"); //creating the scanner reference
		source = GetComponent<AudioSource>();
		gameItems = new string[4] {"Banana" , "Apple" , "milk", "pear"}; //this is our game object from the resources that we can spawn

		reset();
	}
	
	//will spawn new items and delete the former items
	void makeNewItems(int[] newItems , int size){
		GameObject cm = GameObject.Find("itemSpawnPoint1"); //reference point to where the new objects will spawn.
		if(spawnedItems.Count != 0){
			for(int i = 0; i < spawnedItems.Count ; i++){
				Destroy(spawnedItems[i]); //delete the former items
			}
			spawnedItems.Clear();
		}
		
		for(int i = 0; i < size ; i++){
			
			GameObject tmp = (GameObject)Object.Instantiate(Resources.Load(gameItems[newItems[i]])); //instantiate(spawn) from resources the item numbers from the list game Items
			tmp.transform.position = new Vector3((cm.transform.position.x)+(3*(i-size/2)) ,cm.transform.position.y,cm.transform.position.z); //spawn the new objects at this position
			spawnedItems.Add(tmp);//spawn the items
		} 
	}
	
	//evaluate and present a graphical representation if it is right or wrong
	
	// take text written in input and assign it to the reference
	public void AnswerInput(string inputFieldString){
		answerGiven.text = inputFieldString;
		
	}
	public void ConfirmInput(){ //func. that checks if we have enough digits in the answer and evaluates if it correspons to the correct result

		ScanObject temp = Obj.GetComponent<ScanObject>(); //establishing connection to the scanners script
		if(answerGiven.text.Length >= 1){
			Debug.Log ("enough digits");
			PlayerPrefs.SetString("result Given",answerGiven.text);
			print (PlayerPrefs.GetString("result Given"));
			//text2String =  PlayerPrefs.GetString("result Given");
			numberGiven = int.Parse(answerGiven.text); //typecast our string to an int, so that we can evaluate it.
		}
		else{
			Debug.Log ("not enough digits");
			
		}
		int newResult = temp.runGame();
		Debug.Log (numberGiven + " " + newResult);
		//check if the given number by the user is equal to the result
		if(numberGiven == newResult){ //evaluating from the result
			Debug.Log ("correct");
			wrongOrRight.text = "rigtigt!";
			source.PlayOneShot(applaus,15F);
			scenario+=1;
			reset();
		}
		else{
			
			wrongOrRight.text="forkert!";
			source.PlayOneShot(buzz,15F);//play our sound source
			reset();
		}
	}

	public void reset(){
		ScanObject temp = Obj.GetComponent<ScanObject>(); //establishing connection to the scanners script
		// Make new Items
		int numberOfItemsToScanNext = 2;
		int[] newItems;
		//spawn new scenario
		if(scenario==0){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 1};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==1){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 1};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==2){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==3){
			numberOfItemsToScanNext = 3;
			temp.scenario = scenario;
			newItems = new int[3]{ 0 , 2 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}
		// Set the variables in
		temp.scenario = scenario;
		temp.itemsToScan = numberOfItemsToScanNext;// increase if more items are to be scaned
	}
}