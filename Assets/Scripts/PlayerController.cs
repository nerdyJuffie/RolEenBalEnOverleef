using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    //variabelen om bal te laten rollen
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    //code nodig om bal terug te plaatsen in het veld als deze door een gat valt
    private Transform tr;
    private Vector3 startpositie;

    //variabelen voor punten en levens
    private int count;
    private int lives;

    //aantal gezonde paddestoelen
    RandomPosition randpos = new RandomPosition();
   // private int aantalGezondePaddestoelen = RandomPosition.getObjectToAdd;

    //variabelen om de tekst in op te slaan/ weer te geven
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI livesText;
    public GameObject loseTextObject;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //count en lives krijgen de initiele waarde waarmee ze het spel beginnen
        count = 0;
        lives = 5;
        SetCountText();
        SetLivesText();
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);

        //tr en startpositie krijgen hun beginwaarde
        tr = GetComponent<Transform>();
        startpositie = tr.position;

    }

    //code die er voor zorgt dat  waarde van beweging wordt vastgelegd
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        /***De volgende code kijkt of de bal verticaal gezien (y-as) beneden de waarde 3 komt. 
         Indien het geval wordt de bal terug in het spel gebracht op de laatst bekende positie ***/

        if (tr.position.y < -3.0f)
        {
            lives -= 1;
            tr.position = startpositie;
        }
    }

    
   /*** De volgende functie kijkt met welk object de bal (player) botst.
    Indien dit een giftige paddestoel is of een enemy (een spin of een golem), dan kost dit 1 leven.
   Indien het een groene, gezonde paddestoel is, dan krijgt de speler er een punt bij (count +=1) of
    indien hij nog maar 1 leven heeft een leven er bij. Als het object een paddestoel is, dan verdwijnt deze uit het
   spel. 
   ***/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gezond"))
        {
            if (lives < 2)
            {
                lives += 1;
                SetLivesText();
            }
            else
            {
                count += 1;
                SetCountText();
            }
            other.gameObject.SetActive(false);
            
        }
        else if (other.gameObject.CompareTag("giftig"))
        {
            lives -= 1;
            other.gameObject.SetActive(false);
            SetLivesText();
        }
        else if (other.gameObject.CompareTag("enemy"))
        {
            lives -= 1;
            SetLivesText();
        }

    }

    //Methode die er voor zorgt dat de tekst wordt weergegeven
    void SetCountText()
    {
        countText.text = "Punten: " + count.ToString();

        if (count >= 30)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Levens: " + lives.ToString();
        if (lives == 0)
        {
            loseTextObject.SetActive(true);
        }
    }
}
