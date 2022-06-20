using UnityEngine;
using UnityEngine.UI;
public class ShowText : MonoBehaviour
{
    public string textValue;
    public Text rally;
    public Text bestrally;
    public GameObject tutorial;
    public GameObject tint;

    private int bestRally;

    [HideInInspector] public PlayerController control;
    

    void Start() { control = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController; }

    
    void Update()
    {
        rally.text = "Rally: " + control.rally;

        if (control.rally > bestRally)
        {
            bestRally = control.rally;
        }

        bestrally.text = "Best Rally: " + bestRally;

        if (control.showTutorial == true)
        {
            tutorial.SetActive(true);
            tint.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            tutorial.SetActive(false);
            tint.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
