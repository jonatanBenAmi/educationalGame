﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public class InputField : MonoBehaviour {
	
	public AudioClip Song1, Song2, Song3;//Here we add the audio-tracks for the background music
	public int Background = 0; //This is used to change between the background image files. 
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
	//points for correct answers
	public int credit = 0;
	//registering wrong answers
	public int punish = 0;
	//variable used to pimp my font
	public GUIStyle fontStyle= null;
	//texture for gui (the credit system) this will store the thumbs up icon.
	public Texture thumbsUpGui;
	//the text used to tell if the answer given is correct or wrong
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
		gameItems = new string[8] {"Orange" , "Lollipop" , "Apple", "Milk", "Banana", "GiftBox","CerealBox","Redbull"}; //this is our game object from the resources that we can spawn
		source.PlayOneShot(Song1,15f);
		//font size for screen width = 1920 is equal 30, then calculate new font size (for the credit gui)
			fontStyle.fontSize = (int)(30.0f * (float)(Screen.width) / 1920.0f);

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
			credit+=50;
			scenario+=1;
			reset();
		}
		else{
			
			wrongOrRight.text="forkert!";
			credit-=50;
			punish+=1;
			source.PlayOneShot(buzz,15F);//play our sound source
			reset();
		}
	}

	//gui displaying the credit system and giving the user stats related to their effort
	public void OnGUI(){
		string creditText;
		 

		GUI.TextField(new Rect(Screen.width - 160,80,160,110),"point: " + credit.ToString() +"\nforkerte svar: "+ punish.ToString (), fontStyle); //the two first value controls the length and height of the box, while the last two controls the location on screen.

		//GUI.DrawTexture(new Rect(Screen.width - 130,95,90,90),thumbsUpGui);
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
			newItems = new int[2]{ 2 , 1};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==2){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 2};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==3){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==4){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 4};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==5){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 1 , 4};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==6){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==7){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 6};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==8){
			Background += 1;//Change background image
			source.PlayOneShot(Song2,15F);//Change bacground song
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 7};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==9){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 1 , 7};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==10){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 1 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==11){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 6};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==12){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==13){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 4 , 5};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==14){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 1 , 7};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==15){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 4 , 7};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==16){
			Background += 1;//Change background image
			source.PlayOneShot(Song3,15F);//Change bacground song
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 0 , 1};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==17){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==18){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 4};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==19){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 3 , 4};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==20){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 1 , 3};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==21){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 3 , 6};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==22){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 6};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==23){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 5 , 2};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}else if(scenario==24){
			numberOfItemsToScanNext = 2;
			temp.scenario = scenario;
			newItems = new int[2]{ 2 , 7};
			makeNewItems(newItems,numberOfItemsToScanNext);
		}
		// Set the variables in
		temp.scenario = scenario;
		temp.itemsToScan = numberOfItemsToScanNext;// increase if more items are to be scaned
	}
}