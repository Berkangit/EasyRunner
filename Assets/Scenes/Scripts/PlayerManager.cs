using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Transform player;
    private Vector3 startMousePos, startPLayerPos;
    [SerializeField]private bool moveThePlayer;
    [SerializeField]private float maxSpeed ,pathSpeed, velocity;
    public Transform path;
    private Color currentColor;
    private GameManager gameManager;
    public Text toptext;



    private void Awake()
    {
        gameManager = GameObject.Find("MenuManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        player = transform;
        maxSpeed = 0.5f;
        gameManager.gameState = true;
        toptext.text = "You Have To Be Red";
        
    }

    private void Update()
    {
        

        if (Input.GetMouseButtonDown(0) && GameManager.gameManagerInstance.gameState)
        {
            moveThePlayer = true;
            

            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(newPlan.Raycast(ray,out var distance))
            {
                startMousePos = ray.GetPoint(distance);
                startPLayerPos = player.position;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            moveThePlayer = false;

        }
        if(moveThePlayer)
        {
            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (newPlan.Raycast(ray, out var distance))
            {
                Vector3 mouseNewPos = ray.GetPoint(distance);
                Vector3 MouseNewPos = mouseNewPos - startPLayerPos;
                Vector3 DesirePlayerPos = mouseNewPos + startPLayerPos;


                DesirePlayerPos.x = Mathf.Clamp(DesirePlayerPos.x, -0.75f, 0.75f);

                player.position = new Vector3(Mathf.SmoothDamp(player.position.x, DesirePlayerPos.x, ref velocity, maxSpeed)
                 ,player.position.y,player.position.z);
            }

        }
        if (GameManager.gameManagerInstance.gameState)
        {

            var pathNewPos = path.position;
            path.position = new Vector3(path.position.x, path.position.y, Mathf.MoveTowards(pathNewPos.z, -100f, pathSpeed * Time.deltaTime));


        }



    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("zamazingo") && collision.gameObject.GetComponent<MeshRenderer>().material.color == currentColor)
        {
            collision.gameObject.GetComponent<BoxCollider>().isTrigger = true;

        }
        else
        {
            //Death();

        }

        var playerRenderer = player.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

        if (collision.gameObject.CompareTag("Pickable"))
        {

            playerRenderer.material.SetColor("_Color", collision.gameObject.GetComponent<MeshRenderer>().material.color);
            currentColor = playerRenderer.material.color;
            //Debug.Log("Aldin");
            Destroy(collision.gameObject);
            // currentColor.material.color = playerRenderer.material.color;

            //collision.gameObject.GetComponent<MeshRenderer>().material.color =
            //    gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color;

        }

      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("dalgabir"))
        {
            toptext.text = "You Have To Be Blue";

        }
        if (other.gameObject.CompareTag("dalga2"))
        {
            toptext.text = "You Have To Be Yellow";

        }

    }


    public void Death()
    {
        gameManager.gameState = false;

        //if (gameManager.gameState == false)
        //{
        //    Time.timeScale = 0;
        //}

    }



}



