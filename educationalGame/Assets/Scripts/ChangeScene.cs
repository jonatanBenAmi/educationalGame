using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	

	// Update is called once per frame
	public void ChangeToScene (string sceneToChangeTo) { //must be public so that we can reuse the function...
		Application.LoadLevel(sceneToChangeTo);  //sceneToChangeTo is the index of the level that should be changed to...
	
	}
}
