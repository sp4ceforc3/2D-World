using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Speed of the character
    [SerializeField] float speed = 3f;
    // speed modificators, toggle
    int[] speedMod = new int[] {1, 2, 4};
    int indexSpeedMod = 0;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // from https://gamedev.stackexchange.com/questions/171565/accesing-child-gameobjects-of-gameobject
        // All GameObjects have a transform component built-in
        foreach(Transform child in this.transform) {
            GameObject character = child.gameObject;
            character.SetActive(false);
        }

        // First Charcter shall be the active one
        // index 0 ist the camera
        this.transform.GetChild(0).gameObject.SetActive(true);
        
        // Second child is first character and so one
        player = this.transform.GetChild(1).gameObject;
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = Vector3.zero;
        
        // Get input and save state in moveVector
        // taken from exercise, added speedMod
        if (Input.GetKeyDown(KeyCode.LeftShift))
            indexSpeedMod = (indexSpeedMod + 1) % 3;
        if (Input.GetKey(KeyCode.W)) moveVector.y = 1;
        if (Input.GetKey(KeyCode.A)) moveVector.x = -1;
        if (Input.GetKey(KeyCode.S)) moveVector.y = -1;
        if (Input.GetKey(KeyCode.D)) moveVector.x = 1;
        
        // change character component colors by pressing F
        if (Input.GetKeyDown(KeyCode.F)) {
            SpriteRenderer [] childSpriteRenderer = player.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sr in childSpriteRenderer) {
                Color color = Random.ColorHSV();
                sr.color = color;
            }
        }

        // change character by pressing 1,2,3 ...
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            changePlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            changePlayer(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            changePlayer(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            changePlayer(4);
        }
        
        
        // taken from exercise
        // Normalize vector, so that magnitude for diagonal movement is also 1
        moveVector.Normalize();

        moveVector = moveVector * speedMod[indexSpeedMod];
         // Frame rate independent movement
        transform.position += Time.deltaTime * speed * moveVector;
    }

    private void changePlayer(int layer) {
        player.SetActive(false);
        player = this.transform.GetChild(layer).gameObject;
        player.SetActive(true);
    }
}
