using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FormationAI : MonoBehaviour {
    // Define all variables:
	[Tooltip("Your characters will form into positions depending on the positions of black pixels in your image. The recommended size is 10x10 px.")]
	public Texture2D[] formationImage;

    private int formationID;
    private float characterSpeed;
    private string characterAnimation = "";
    private bool forming;
    private GameObject emptyPrefab;

    List<List<GameObject>> destination;
	List<List<GameObject>> characters;

    private void Start()
    {
        // Initialize the 'destination' and 'characters' lists:
        destination = new List<List<GameObject>>();
        characters = new List<List<GameObject>>();

        emptyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("EmptyObject")[0]));
        formationID = 0;

        // For Testing: GameObject character = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("Character")[0]));
    }

    void Update(){
        if (forming) {
            for (int f = 0; f < characters.Count; f++)
            {
                for (int i = 0; i < characters[f].Count; i++)
                {
                    // If the characters have finished forming:
                    if (destination[f][i] != null)
                    {
                        // If the current destination's slot isn't empty:
                        if (characters[f][i].transform.position == destination[f][i].transform.position)
                        {
                            // Destroy the destination gameobject for all characters who have reached their destination:
                            Destroy(destination[f][i]);
                        }
                        else
                        {
                            // Make each character move to it's destination:
                            characters[f][i].transform.position = Vector3.MoveTowards(characters[f][i].transform.position, destination[f][i].transform.position, characterSpeed * Time.deltaTime);

                            // Play the character's walk animation if they have one:
                            if (characterAnimation != "")
                            {
                                if (characters[f][i].transform.position != destination[f][i].transform.position)
                                {
                                    characters[f][i].GetComponent<Animator>().SetBool(characterAnimation, true);
                                }
                                else
                                {
                                    characters[f][i].GetComponent<Animator>().SetBool(characterAnimation, false);
                                }
                            }
                        }
                    }
                }
            }
		}
	}

    /// <summary>
    ///  Create many characters around an area and make them form into a specified formation.
    /// </summary>
    /// <param name="characterPrefab">The prefab to spawn.</param>
    /// <param name="formationNumber">Which formation should the characters form into.</param>
    /// <param name="position">The position where you want the characters to form.</param>
    /// <param name="speed">How fast the characters should move.</param>
    /// <param name="spawnRange">How far around the position should the characters spawn.</param>
    /// <param name="cluster">How close should the characters be together.</param>
    /// <param name="size">How large should the characters and formation be.</param>
    public void Form(GameObject characterPrefab, int formationNumber, Vector3 position, float speed, float spawnRange, float cluster, float size)
    {
        // Initialize the speed variable:
        characterSpeed = speed;

        // Initialize the 'destination' and 'character' lists:
        List<GameObject> allDestinations = new List<GameObject>();
        List<GameObject> allCharacters = new List<GameObject>();

        // Create a parent for the destinations and positions and give it a custom ID:
        GameObject formParent = Instantiate(emptyPrefab);
        formParent.name = "FormationParent" + formationID;

        for (int y = 0; y < formationImage[formationNumber].height; y++)
        {
            for (int x = 0; x < formationImage[formationNumber].width; x++)
            {
                if (formationImage[formationNumber].GetPixel(x, y) == Color.black)
                {
                    // Create destinations in the formation of the black pixels that can be adjusted by the 'size' and 'cluster' variables:
                    allDestinations.Add(Instantiate(emptyPrefab, ((new Vector3(x, 0f, y) * size) / cluster) + (position / 2f), new Quaternion(0f, 0f, 0f, 0f), formParent.transform));

                    // Create characters in a range around the position variable:
                    allCharacters.Add(Instantiate(characterPrefab, new Vector3(Random.Range(-spawnRange, spawnRange) / 2f, 0f, Random.Range(-spawnRange, spawnRange)) + position , new Quaternion(0f, 0f, 0f, 0f), formParent.transform));
                }
            }
        }

        for (int i = 0; i < allDestinations.Count; i++)
        {
            // Adjust each character's size so that it can be adjusted by the 'size' variable:
            allCharacters[i].transform.localScale = new Vector3(allCharacters[i].transform.localScale.x * size, allCharacters[i].transform.localScale.y, allCharacters[i].transform.localScale.z * size);

            // Tag and name all destinations:
            allDestinations[i].tag = "FormationPos";
            allDestinations[i].name = "Pos";
        }

        destination.Add(allDestinations);
        characters.Add(allCharacters);

        // Start forming:
        forming = true;
    }

    /// <summary>
    ///  Make characters form into a specified formation.
    /// </summary>
    /// <param name="formationCharacters">The characters you want to form.</param>
    /// <param name="formationNumber">Which formation should the characters form into.</param>
    /// <param name="position">The position where you want the characters to form.</param>
    /// <param name="speed">How fast the characters should move.</param>
    /// <param name="cluster">How close should the characters be together.</param>
    /// <param name="size">How large should the characters and formation be.</param>
    public void Form(GameObject[] formationCharacters, int formationNumber, Vector3 position, float speed, float cluster, float size)
    {
        // Initialize the speed variable:
        characterSpeed = speed;

        // Create a variable to track all characters that haven't been worked with yet:
        int amountLeft = formationCharacters.Length;

        // Initialize the 'destination' and 'character' lists:
        List<GameObject> allDestinations = new List<GameObject>();
        List<GameObject> allCharacters = new List<GameObject>();

        if (formationCharacters.Length != 0)
        {
            // If the 'formationCharacters' field at least has one character:
            for (int y = 0; y < formationImage[formationNumber].height; y++)
            {
                for (int x = 0; x < formationImage[formationNumber].width; x++)
                {
                    if (formationImage[formationNumber].GetPixel(x, y) == Color.black)
                    {
                        // For every black pixel in the current formationImage:

                        // Create destinations in the formation of the black pixels that can be adjusted by the 'size' and 'cluster' variables:
                        allDestinations.Add(Instantiate(emptyPrefab, ((new Vector3(x, 0f, y) * size) / cluster) + (position / 2f), new Quaternion(0f, 0f, 0f, 0f)));

                        // Add all characters to the list:
                        if (amountLeft != 0)
                        {
                            // If their is at least one character left:
                            allCharacters.Add(formationCharacters[amountLeft - 1]);
                            amountLeft--;
                        }
                    }
                }
            }

            for (int i = 0; i < allDestinations.Count; i++)
            {
                // Adjust each character's size so that it can be adjusted by the 'size' variable:
                allDestinations[i].tag = "FormationPos";
                allDestinations[i].name = "Pos";
            }
        }

        destination.Add(allDestinations);
        characters.Add(allCharacters);

        // Start forming:
        forming = true;
    }

    /// <summary>
    ///  Create many characters around an area and make them form into a specified formation.
    /// </summary>
    /// <param name="characterPrefab">The prefab to spawn.</param>
    /// <param name="moveAnimationParameter">The animation parameter that should be set to true while each character is walking to their destinations.</param>
    /// <param name="formationNumber">Which formation should the characters form into.</param>
    /// <param name="position">The position where you want the characters to form.</param>
    /// <param name="speed">How fast the characters should move.</param>
    /// <param name="spawnRange">How far around the position should the characters spawn.</param>
    /// <param name="cluster">How close should the characters be together.</param>
    /// <param name="size">How large should the characters and formation be.</param>
    public void Form(GameObject characterPrefab, string moveAnimationParameter, int formationNumber, Vector3 position, float speed, float spawnRange, float cluster, float size)
    {
        // Initialize the speed variable:
        characterSpeed = speed;

        // Setup Animation:
        characterAnimation = moveAnimationParameter;

        // Initialize the 'destination' and 'character' lists:
        List<GameObject> allDestinations = new List<GameObject>();
        List<GameObject> allCharacters = new List<GameObject>();

        // Create a parent for the destinations and positions and give it a custom ID:
        GameObject formParent = Instantiate(emptyPrefab);
        formParent.name = "FormationParent" + formationID;

        for (int y = 0; y < formationImage[formationNumber].height; y++)
        {
            for (int x = 0; x < formationImage[formationNumber].width; x++)
            {
                if (formationImage[formationNumber].GetPixel(x, y) == Color.black)
                {
                    // Create destinations in the formation of the black pixels that can be adjusted by the 'size' and 'cluster' variables:
                    allDestinations.Add(Instantiate(emptyPrefab, ((new Vector3(x, 0f, y) * size) / cluster) + (position / 2f), new Quaternion(0f, 0f, 0f, 0f), formParent.transform));

                    // Create characters in a range around the manager:
                    allCharacters.Add(Instantiate(characterPrefab, new Vector3(Random.Range(-spawnRange, spawnRange) / 2f, 0f, Random.Range(-spawnRange, spawnRange)) + (position / 2f), new Quaternion(0f, 0f, 0f, 0f), formParent.transform));                  
                }
            }
        }

        for (int i = 0; i < allCharacters.Count; i++)
        {
            // Adjust each character's size so that it can be adjusted by the 'size' variable:
            allCharacters[i].transform.localScale = new Vector3(allCharacters[i].transform.localScale.x * size, allCharacters[i].transform.localScale.y, allCharacters[i].transform.localScale.z * size);

            // Tag and name all destinations:
            allDestinations[i].tag = "FormationPos";
            allDestinations[i].name = "Pos";
        }

        destination.Add(allDestinations);
        characters.Add(allCharacters);

        // Start forming:
        forming = true;
    }

    /// <summary>
    ///  Make characters form into a specified formation.
    /// </summary>
    /// <param name="formationCharacters">The characters you want to form.</param>
    /// <param name="moveAnimationParameter">The animation parameter that should be set to true while each character is walking to their destinations.</param>
    /// <param name="formationNumber">Which formation should the characters form into.</param>
    /// <param name="position">The position where you want the characters to form.</param>
    /// <param name="speed">How fast the characters should move.</param>
    /// <param name="cluster">How close should the characters be together.</param>
    /// <param name="size">How large should the characters and formation be.</param>
    public void Form(GameObject[] formationCharacters, string moveAnimationParameter, int formationNumber, Vector3 position, float speed, float cluster, float size)
    {
        // Initialize the speed variable:
        characterSpeed = speed;

        // Setup Animation:
        characterAnimation = moveAnimationParameter;

        // Create a variable to track all characters that haven't been worked with yet:
        int amountLeft = formationCharacters.Length;

        // Initialize the 'destination' and 'character' lists:
        List<GameObject> allDestinations = new List<GameObject>();
        List<GameObject> allCharacters = new List<GameObject>();

        if (formationCharacters.Length != 0)
        {
            // If the 'formationCharacters' field at least has one character:
            for (int y = 0; y < formationImage[formationNumber].height; y++)
            {
                for (int x = 0; x < formationImage[formationNumber].width; x++)
                {
                    if (formationImage[formationNumber].GetPixel(x, y) == Color.black)
                    {
                        // For every black pixel in the current formationImage:

                        // Create destinations in the formation of the black pixels that can be adjusted by the 'size' and 'cluster' variables:
                        allDestinations.Add(Instantiate(emptyPrefab, ((new Vector3(x, 0f, y) * size) / cluster) + (position / 2f), new Quaternion(0f, 0f, 0f, 0f)));

                        // Add all characters to the list:
                        if (amountLeft != 0)
                        {
                            // If their is at least one character left:
                            allCharacters.Add(formationCharacters[amountLeft - 1]);
                            amountLeft--;
                        }
                    }
                }
            }

            for (int i = 0; i < allDestinations.Count; i++)
            {
                // Adjust each character's size so that it can be adjusted by the 'size' variable:
                allDestinations[i].tag = "FormationPos";
                allDestinations[i].name = "Pos";
            }
        }

        destination.Add(allDestinations);
        characters.Add(allCharacters);
        
        // Start forming:
        forming = true;
    }
}