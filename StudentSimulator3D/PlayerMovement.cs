using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Объект управляет сверхспособностями
    /// </summary>
    public PowerUpController PUController;
    /// <summary>
    /// Объект управляет анимациями персонажа
    /// </summary>
    public Animator ac;
    /// <summary>
    /// Объект управляет логикой игры
    /// </summary>
    public GameManager GM;
    CapsuleCollider selfCollider;

    /// <summary>
    /// Способствует реализации физических свойств персонажа таких как масса, графитация, коллизии
    /// </summary>
    Rigidbody rb;

    public delegate void OnPowerupUse(PowerUpController.PowerUp.Type type);
    public static event OnPowerupUse PowerUpUseEvent;

    /// <summary>
    /// Высота прыжка
    /// </summary>
    public float jumpSpeed = 12;

    int laneNumber = 1,
        lanesCount = 2;

    public float FirstLanePos,
                 LaneDistance,
                 SideSpeed;
    bool WannaJump = false,
        IsImortal = false;
    // Start is called before the first frame update
    void Start()
    {

        selfCollider = GetComponent<CapsuleCollider>();
        ac = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();


    }

    private void FixedUpdate()
    {


        if (WannaJump)
        {
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Force);
            ac.SetTrigger("Jump");
            WannaJump = false;

        }

    }



    // Update is called once per frame
    void Update()
    {


        rb.AddForce(new Vector3(0, Physics.gravity.y * 4, 0), ForceMode.Acceleration);
        //if (IsGrounded())
        //{
        if (GM.CanPlay)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                WannaJump = true;
            }
        }
        //}




        checkInput();


        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, FirstLanePos + (laneNumber * LaneDistance), Time.deltaTime * SideSpeed);
        transform.position = newPos;


    }

    /// <summary>
    /// Проверяет ввод пользователя для управления
    /// </summary>
    void checkInput()
    {
        int sign = 0;

        if (!GM.CanPlay)
            return;

        if (Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.LeftArrow))
            sign = -1;
        else if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
            sign = 1;
        else
            return;


        laneNumber += sign;
        laneNumber = Mathf.Clamp(laneNumber, 0, lanesCount);

    }

    /// <summary>
    /// Метод реализует подбор монет
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PointMultiplier":
                PowerUpUseEvent(PowerUpController.PowerUp.Type.MUILTIPLIER);
                Destroy(other.gameObject);
                break;

            case "ImmertialTag":
                PowerUpUseEvent(PowerUpController.PowerUp.Type.IMMORTALITY);
                Destroy(other.gameObject);
                break;

            case "CoinTag":
                GM.AddCoins(1);
                Destroy(other.gameObject);
                break;
            default: return;



        }



    }

    /// <summary>
    /// При столкновении с препятствием игрок умирает
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("trap"))
            return;
        if (IsImortal)
        {
            collision.collider.isTrigger = true;
            return;
        }

        StartCoroutine(Death());
    }
    /// <summary>
    /// когда игрок умирает, включается экран GameOver(Game Manager) 
    /// </summary>
    /// <returns></returns>
    IEnumerator Death()
    {
        GM.CanPlay = false;
        PUController.ResetAllPowerUps();


        ac.SetTrigger("Death");
        yield return new WaitForSeconds(2);

        GM.Smert();

    }
    /// <summary>
    /// Включает бессмертие
    /// </summary>
    public void ImmortalityOn()
    {
        IsImortal = true;
    }
    /// <summary>
    /// Выключает бессмерти 
    /// </summary>
    public void ImmortalityOff()
    {
        IsImortal = false;
    }
}

