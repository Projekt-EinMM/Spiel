using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class PlayerMovment : MonoBehaviour
{
    SerialPort sp = new SerialPort("/dev/ttyACM0", 9600); //Erstellt eine Verbindung über den USB-Port mit der jeweiligen Frequenz (Baud)
    bool alive = true; //Überprüft den "am Leben" Status
    public float speed = 5; //Geschwindigkeit
    public Rigidbody rb = new Rigidbody(); //Rigidbody ist die Spielfigur/Modell
    float horizontalInput = 0; //Der Bewegungsinput, um die Figur zu steuern
    public float horizontalMultiplier = 2;
    float read;

    void Start()
    {
        sp.Open(); //Öffnet den Verbindungsport
        sp.ReadTimeout = 0; //Lies-Intervall
    }

    private void FixedUpdate()
    {
        if (!alive)
            return; //Solange man am Leben ist, kann man die Figur steuern
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove =
            transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove); //Die Figur wird bewegt
    }

    /* "Update()" ist eine Methode die jeden Frame aufgerufen wird */
    void Update()
    {
        if (sp.IsOpen)
        { //Wenn der Verbindungsport offen ist, dann wird gelesen was der Mikrokontroller ausgibt
            try
            { //und je nach Input wird die Figur auf der horizontalen Ebene anders gesteuert
                if (sp.ReadByte() == 0)
                {
                    print(sp.ReadByte());
                    horizontalInput = 0; //Neutrprint(horizontalInput);
        if (Input.GetAxis("Horizontal")!=0){                //Wenn bei der Tastatur eine Eingabe passiert, dann wird der Mikrokontroller wert überschrieben
            horizontalInput = Input.GetAxis("Horizontal");  //und man steuert mit der Tastatur
        }

        if (transform.position.y < -5){             //Wenn man runterfällt, dann Stirbt man
            Die();
        }
                    print(sp.ReadByte());
                    read = horizontalInput + -0.2f;
                    horizontalInput = read; //Negativer Input = nach Links gehen
                }
                if (sp.ReadByte() == 2)
                {
                    print(sp.ReadByte());
                    read = horizontalInput + 0.2f;
                    horizontalInput = read; //Positiver Input = nach Rechts gehen
                }
            }
            catch (System.Exception) { }
            /**print(horizontalInput);
        if (Input.GetAxis("Horizontal")!=0){                //Wenn bei der Tastatur eine Eingabe passiert, dann wird der Mikrokontroller wert überschrieben
            horizontalInput = Input.GetAxis("Horizontal");  //und man steuert mit der Tastatur
        }

        if (transform.position.y < -5){             //Wenn man runterfällt, dann Stirbt man
            Die();
        }
        if (transform.position.y < -5){             //Wenn man runterfällt, dann Stirbt man
            Die();
        }
        **/
        }
        print(horizontalInput);
        if (Input.GetAxis("Horizontal") != 0)
        { //Wenn bei der Tastatur eine Eingabe passiert, dann wird der Mikrokontroller wert überschrieben
            horizontalInput = Input.GetAxis("Horizontal"); //und man steuert mit der Tastatur
        }

        if (transform.position.y < -5)
        { //Wenn man runterfällt, dann Stirbt man
            Die();
        }
    }

    public void Die()
    {
        alive = false;
        Invoke("Restart", 1); //Wenn der Spieler Stirbt, wird "Restart" aufgerufen
    }

    void Restart()
    {
        sp.Close();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Die "Scene" also das Spiel wird neugestartet
    }
}
