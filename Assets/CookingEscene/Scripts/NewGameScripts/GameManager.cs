using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Clientes (desactivados al inicio)")]
    public GameObject[] customers;

    [Header("UI")]
    public GameObject playButton;
    public Text timerText;
    public Text scoreText;

    [Header("Reglas")]
    public float gameTime = 60f;
    public int pointsPerCoffee = 100;
    public float delayBetweenCustomers = 1.5f;

    float timeLeft;
    int score;
    int customerIndex;
    bool playing;
    bool waitingCustomer;

    public bool CanDeliver { get { return playing && waitingCustomer; } }

    void Awake() { I = this; }

    void Start()
    {
        foreach (GameObject c in customers) c.SetActive(false);
        playButton.SetActive(true);
        timerText.text = Mathf.CeilToInt(gameTime).ToString();
        scoreText.text = "PUNTOS: 0";
    }

    // Conéctalo al OnClick del botón
    public void StartGame()
    {
        if (playing) return;

        StopAllCoroutines();
        playButton.SetActive(false);
        score = 0;
        customerIndex = 0;
        timeLeft = gameTime;
        playing = true;
        waitingCustomer = false;
        scoreText.text = "PUNTOS: 0";

        StartCoroutine(NextCustomer(0.5f));
    }

    IEnumerator NextCustomer(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!playing) yield break;

        if (customerIndex >= customers.Length)
        {
            EndGame("¡TURNO COMPLETADO!");
            yield break;
        }

        customers[customerIndex].SetActive(true);
        waitingCustomer = true;
    }

    public void DeliverCoffee()
    {
        if (!CanDeliver) return;

        waitingCustomer = false;
        score += pointsPerCoffee;
        scoreText.text = "PUNTOS: " + score;

        customers[customerIndex].SetActive(false);
        customerIndex++;

        StartCoroutine(NextCustomer(delayBetweenCustomers));
    }

    void Update()
    {
        if (!playing) return;

        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(Mathf.Max(timeLeft, 0f)).ToString();

        if (timeLeft <= 0f) EndGame("¡SE ACABÓ EL TIEMPO!");
    }

    void EndGame(string message)
    {
        playing = false;
        waitingCustomer = false;
        StopAllCoroutines();

        foreach (GameObject c in customers) c.SetActive(false);

        timerText.text = message;                    // el reloj muestra el mensaje final
        scoreText.text = "PUNTOS: " + score;
        playButton.SetActive(true);                  // el botón sirve para repetir
    }
}