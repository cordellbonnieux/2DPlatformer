using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
	public Transform[] backgrounds; //background and foreground array for parallaxing.
	private float[] parallaxScales; // the proportion of the camera's movement to move the backgrounds by.
	public float smoothing = 1f; // how smooth the parallaxing will be. This must have a value greater than 0
	
	private Transform cam; //reference to the main camera transform
	private Vector3 previousCamPos; // store the position of the camera in the previous frame.
	
	// Is called before start, but after the game objects are set up. Great for references.
	void Awake() {
		// set up the camera reference
		cam = Camera.main.transform;
	}
	// Start is called before the first frame update
	void Start()
	{
        	// The previous frame had the current frame's camera position
		previousCamPos = cam.position;
	
		parallaxScales = new float[backgrounds.Length]; //get the number of backgrounds

		// assigning corresponding parallaxScales
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}

	// Update is called once per frame
	void Update()
	{
        	for (int i = 0; i < backgrounds.Length; i++){
			// the parallax is the opposite of the camera movement, because the previous frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// create a target position, which is the background's current position with it's target x position.
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current position and the target position using what is called lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		// set the previousCamPos to the camera's position at the end of the frame
		previousCamPos = cam.position;
	}
}
