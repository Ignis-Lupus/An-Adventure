using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugFormation : MonoBehaviour {
	public GameObject character;
	public GameObject posPrefab;
	public GameObject formParentPrefab;
	[Tooltip("How fast your character moves.")]
	public float speed = 2.5f;
	[Tooltip("How large the characters should be and the area that they spawn.")]
	public float size = 1f;
	[Tooltip("The higher this is, the closer the characters will move to each other.")]
	public float closeness = 5f;
	[Tooltip("The higher this is, the farther characters will spawn from the center.")]
	public float spawnRange = 7f;
	public bool hasMoveAnimation;
	[Tooltip("Your characters will form into positions depending on the positions of black pixels in your image. The recommended size is 10x10 px.")]
	public Texture2D[] formationImage;
	private int formation;
	private int formationID;
	private bool forming;

	List<GameObject> positions;
	List<GameObject> characters;

	void Update(){
		if (forming) {
			for (int i = 0; i < characters.Count; i++) {
				//If the characters have finished forming:
				if (positions [i] != null) {
					if (characters [i].transform.position == positions [i].transform.position) {
						Destroy (positions [i]);
					} else {
						characters [i].transform.position = Vector3.MoveTowards (characters [i].transform.position, positions [i].transform.position, speed * Time.deltaTime);
						if (hasMoveAnimation) {
							if (characters [i].transform.position != positions [i].transform.position) {
								characters [i].GetComponent<Animator> ().SetBool ("Walk", true);
							} else {
								characters [i].GetComponent<Animator> ().SetBool ("Walk", false);
							}
						}
					}
				}
			}
		}
	}

	public void SetFormationID(int newID){
		formationID = newID;
	}

	public void Reset(Text box){
		Destroy(GameObject.Find("FormationParent" + formationID));

		if(formationImage.Length != 0){
			int num;
			if (int.TryParse (box.text, out num)) {
				forming = true;
				if (num <= formationImage.Length - 1 && num > -1) {
					Form (character, num, speed, spawnRange, closeness, size);
				} else {
					Debug.LogError ("Their is not a formation with that number, please choose one from 0-" + (formationImage.Length - 1).ToString() + ".");
				}
			} else {
				Debug.LogError ("Please enter a number in the text box.");
			}
		} else {
			Debug.LogError("Their are no formations. Please read the documentation to find out how to add your own.");
		}
	}

	public void Form(GameObject characterObject, int formationNumber, float characterSpeed, float characterSpawnRange, float characterCloseness, float spawnSize){
		
		positions = new List<GameObject>();
		characters = new List<GameObject>();

		GameObject formParent = Instantiate(formParentPrefab);
		formParent.name = "FormationParent" + formationID;

		for (int y = 0; y < formationImage[formationNumber].height; y++)
		{
			for (int x = 0; x < formationImage[formationNumber].width; x++)
			{
				if(formationImage[formationNumber].GetPixel(x, y) == Color.black)
				{
					//Debug.Log ("Pixel Read: " + x + "," + y);
					// Add positions in the formation of the black pixels that can be adjusted by the 'size' and 'spread' variables:
					positions.Add(Instantiate(posPrefab, ((new Vector3(x, 0f, y) * spawnSize) / characterCloseness) + (transform.position / 2f), new Quaternion(0f, 0f, 0f, 0f), formParent.transform));
					// Adds your characters in a range around the manager:
					characters.Add(Instantiate(characterObject, new Vector3(Random.Range(-spawnRange, spawnRange) / 2f, 0f, Random.Range(-characterSpawnRange, characterSpawnRange)), new Quaternion(0f, 0f, 0f, 0f), formParent.transform));
				}
			}
		}

		for (int i = 0; i < characters.Count; i++)
		{
			// Adjust each character's size so that it can be adjusted by the 'size' variable:
			characters[i].transform.localScale = new Vector3(characters[i].transform.localScale.x * spawnSize, characters[i].transform.localScale.y, characters[i].transform.localScale.z * spawnSize);
			positions[i].tag = "FormationPos";
		}
	}
}