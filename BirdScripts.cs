using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScripts : MonoBehaviour {

    public static BirdScripts instance;

    [SerializeField]
    public Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 3f;
    private float bounceSpeed = 4f;

    private bool didFlap;
    public bool isAlive;

    private Button flapButton;
    
	public int score;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        isAlive = true;
		score = 0;
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => flapTheBird());

        CameraX();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (didFlap)
            {
                didFlap = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                anim.SetTrigger("Flap");
            }

            if(myRigidBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } else {
                float angle = 0;
                angle = Mathf.Lerp(0, -70, -myRigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
	}

    void CameraX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    public float GetPositionX() {
        return transform.position.x;
    }

    public void flapTheBird()
    {
        didFlap = true;
    }

	void OnCollisionEnter2D(Collision2D target) {
		if (target.gameObject.tag == "Pipe" || target.gameObject.tag == "Ground" ) {
			if (isAlive) {

				isAlive = false;
				anim.SetTrigger ("BirdDied");
				GamePlayController.instance.playerDiedShowScore (score);
			}
		} else if (target.gameObject.tag == "Flag") {
			if (isAlive) {

				isAlive = false;
				GamePlayController.instance.finishGame();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "PipeHolder") {
			score++;
			GamePlayController.instance.setScore(score);
		}
	}

}












































