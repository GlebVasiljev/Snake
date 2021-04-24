using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SnakeBody
{
    public List<Vector2> HeadPos = new List<Vector2>();
    public List<float> HeadAng = new List<float>();
    public Transform BodyTransform;
    public float LastDistance;

}
public class SnakeControll : MonoBehaviour
{
    public float speed = 3f;
    public float DeltaSpeed;
    public float fps;
    public static bool IsApple;
    public Transform Head;
    public static int result = 0;
    float DefRotate = 0f;
    public Text Score;
    public Text FPStext;
    public float BodyDelta = 1f;
    public GameObject SnakeBody;
    public List<SnakeBody> BodyList = new List<SnakeBody>();
    public GameObject GameOverMenu;
    public Animator SnakeEat;
    public GameObject SnakeDeath;
    public GameObject MovementController;
    public GameObject Pause;
    const float ChangeSpeed = 4;
    bool DeadSnake = false;
    bool IsRotation = false;
    bool MobileUp;
    bool MobileDown;
    bool MobileLeft;
    bool MobileRight;
    bool PressUp => MobileUp || Input.GetKeyDown(KeyCode.UpArrow);
    bool PressDown => MobileDown || Input.GetKeyDown(KeyCode.DownArrow);
    bool PressLeft => MobileLeft || Input.GetKeyDown(KeyCode.LeftArrow);
    bool PressRight => MobileRight || Input.GetKeyDown(KeyCode.RightArrow);
    private Light2D SnakeLight;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Score.text = "Score: " + result;
        GameOverMenu.SetActive(false);
        StartCoroutine(SnakeMove());
    }

    void Update()
    {
        IsRotation = false;
        if (PressUp)
        {
            if (DefRotate == 270f)
            {
                Dead();
                return;
            }
            DefRotate = 90f;
            IsRotation = true;

        }
        if (PressLeft)
        {
            if (DefRotate == 0f)
            {
                Dead();
                return;
            }
            DefRotate = 180f;
            IsRotation = true;
        }
        if (PressDown)
        {
            if (DefRotate == 90f)
            {
                Dead();
                return;
            }
            DefRotate = 270f;
            IsRotation = true;
        }
        if (PressRight)
        {
            if (DefRotate == 180f)
            {
                Dead();
                return;
            }
            DefRotate = 0f;
            IsRotation = true;
        }

        /* Vector3 Rotation = Head.eulerAngles;


        Rotation.z = DefRotate;
        Head.eulerAngles = Rotation;
        Head.Translate(Vector2.right * speed * Time.deltaTime);
        foreach (SnakeBody body in BodyList)
        {


            if (IsRotation)
            {
                body.HeadAng.Add(DefRotate);
                body.HeadPos.Add(Head.position);
            }
            body.BodyTransform.Translate(Vector2.right * speed * Time.deltaTime);
            if (body.HeadAng.Count > 0)
            {
                // body.BodyTransform.position = Vector2.MoveTowards(body.BodyTransform.position, body.HeadPos[0], speed * Time.deltaTime);
                //body.BodyTransform.Translate(Vector2.right * speed * Time.deltaTime);
                float distance = (body.HeadPos[0] - (Vector2)body.BodyTransform.position).magnitude;
                // if (  Vector2.Distance(body.HeadPos[0], body.BodyTransform.position) < 0.005f)
                if (distance >= body.LastDistance)
                {
                    body.BodyTransform.position = body.HeadPos[0];
                    body.BodyTransform.eulerAngles = new Vector3(0, 0, body.HeadAng[0]);
                    body.HeadPos.RemoveAt(0);
                    body.HeadAng.RemoveAt(0);
                    body.LastDistance = Mathf.Infinity;
                }
                else
                {
                    body.LastDistance = distance;
                }
            }
            else
            {

                // body.BodyTransform.position = Vector2.MoveTowards(body.BodyTransform.position, Head.position, speed * Time.deltaTime);
                // body.BodyTransform.Translate(Vector2.right * speed * Time.deltaTime);
            }

        }*/
        fps = 1 / Time.unscaledDeltaTime;
        FPStext.text = "" + fps.ToString("00.00");
        FPStext.text = "" + fps.ToString("00.00");
    }

    IEnumerator SnakeMove()
    {

        while (true)
        {
            
            // float Speed = 1 / speed;
            float Delta = 1 / DeltaSpeed;
            yield return null;
            if (Pause.activeInHierarchy)
                continue;
            if (DeadSnake)
                continue;
            if (IsRotation)
            {
                Vector3 Rotation = Head.eulerAngles;
                Rotation.z = DefRotate;
                Head.eulerAngles = Rotation;
            }
            Head.Translate(Vector2.right * Delta);
            foreach (SnakeBody body in BodyList)
            {
                if (IsRotation)
                {
                    body.HeadAng.Add(DefRotate);
                    body.HeadPos.Add(Head.position);

                }
                body.BodyTransform.Translate(Vector2.right * Delta);
                if (body.HeadAng.Count > 0)
                {
                    float Distance = Vector2.Distance(body.BodyTransform.position, body.HeadPos[0]);
                    if (Distance >= body.LastDistance)
                    {
                        body.BodyTransform.position = body.HeadPos[0];
                        body.BodyTransform.eulerAngles = new Vector3(0, 0, body.HeadAng[0]);
                        body.HeadPos.RemoveAt(0);
                        body.HeadAng.RemoveAt(0);
                        body.LastDistance = Mathf.Infinity;
                    }
                    else
                    {
                        body.LastDistance = Distance;
                    }
                }
            }
            // Head.Translate(Vector2.right * Delta);
            IsRotation = false;

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Apple")
        {

            GoldenApple apple = collision.gameObject.GetComponentInParent<GoldenApple>();
            switch (apple.Type)
            {
                case AppleType.Golden:
                    DeltaSpeed -= ChangeSpeed;
                    Invoke(nameof(Speedtime), 5);
                    break;

                case AppleType.Blue:
                    Light2D SnakeLight = GetComponentInChildren<Light2D>();
                    Invoke(nameof(SnakeVisionRadius), 10);
                    SnakeLight.pointLightOuterRadius += 9;
                    break;
            }

            GameObject.Destroy(collision.gameObject, 0.15f);
            result++;

            Score.text = "Score: " + result;
            IsApple = true;
            AppleController.CreateApple();

            BodyCreate();
            SnakeEat.SetTrigger("Eat");
            return;
        }
        if (collision.tag == "Spikes" || collision.tag == "body" || collision.tag == "Enemy")
        {
            Dead();
        }
    }

    private void LateUpdate()
    {
        IsApple = false;
        MobileDown = false;
        MobileUp = false;
        MobileRight = false;
        MobileLeft = false;
    }

    public void PressMObileButton(int Dir)
    {
        switch (Dir)
        {
            case 0:
                MobileDown = true;
                break;
            case 1:
                MobileUp = true;
                break;
            case 2:
                MobileLeft = true;
                break;
            case 3:
                MobileRight = true;
                break;
        }
    }
    void SnakeVisionRadius()
    {
        Light2D SnakeLight = GetComponentInChildren<Light2D>();
        SnakeLight.pointLightOuterRadius -= 9;
    }
    void Speedtime()
    {
        DeltaSpeed += ChangeSpeed;
    }
    void Dead()
    {
        DeadSnake = true;
        Debug.Log("Lose");
        SnakeDeath.transform.position = Head.position;
        SnakeDeath.SetActive(true);
        gameObject.SetActive(false);
        Invoke(nameof(OpenMenu), 2.5f);


    }
    void OpenMenu()
    {
        MovementController.SetActive(false);
        GameOverMenu.SetActive(true);
    }

    public void SceneRestart()
    {
        SceneManager.LoadScene("Level1");
        DayController.Day = 1f;
    }

    void BodyCreate()
    {

        GameObject obj = Instantiate(SnakeBody, transform);
        SnakeBody body = new SnakeBody();
        body.LastDistance = Mathf.Infinity;
        body.BodyTransform = obj.transform;
        Vector3 Rotation = body.BodyTransform.eulerAngles;
        if (BodyList.Count == 0)
        {
            Rotation.z = DefRotate;
        }
        else
        {
            Rotation.z = BodyList[BodyList.Count - 1].BodyTransform.eulerAngles.z;
            body.HeadAng.AddRange(BodyList[BodyList.Count - 1].HeadAng);
            body.HeadPos.AddRange(BodyList[BodyList.Count - 1].HeadPos);
        }
        body.BodyTransform.eulerAngles = Rotation;
        body.BodyTransform.position = BodyList.Count == 0 ? Head.position : BodyList[BodyList.Count - 1].BodyTransform.position;
        body.BodyTransform.Translate(Vector2.left * BodyDelta);


        BodyList.Add(body);

    }

    public void PauseController()
    {
        Invoke(nameof(PauseDelay), 0.5f);
    }

    void PauseDelay()
    {
        Pause.gameObject.SetActive(false);
    }
}
