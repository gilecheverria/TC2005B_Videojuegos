/*
Temporarily modify the colors of a UI image to make it blink
Used to signal a sound in the Simon game

Gilberto Echeverria
2024-02-27
*/

using UnityEngine;
using UnityEngine.UI;

public class SimonButton : MonoBehaviour
{
    [SerializeField] Color blinkColor;
    [SerializeField] float duration;
    [SerializeField] AudioSource audioSource;

    Color baseColor;
    Image image;
    float elapsed;
    float t;
    bool blinking;

    // Set up the button based on its id
    public void Init(int id, float hue)
    {
        // Initialize the required components
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        blinking = false;

        // Set the sounds for the buttons
        audioSource.clip = Resources.Load<AudioClip>("Sounds/" + (id % 9));
        // Change the color of the button
        image.color = Color.HSVToRGB(hue, 0.8f, 0.8f);
        baseColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(blinking) {
            if(elapsed <= duration) {
                elapsed += Time.deltaTime;
                t = elapsed / duration;
                image.color = Color.Lerp(baseColor, blinkColor, t);
            } else {
                // When time runs out, restore the color
                blinking = false;
                image.color = baseColor;
            }
        }
    }

    // Public method to activate the effect
    public void Blink()
    {
        audioSource.Play();
        elapsed = 0;
        blinking = true;
    }
}
