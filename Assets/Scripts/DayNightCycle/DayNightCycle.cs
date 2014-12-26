using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public Light sunLight;
    public Color daytimeColor;
    public Color duskColor;

    public int Hour
    {
        get {
            return hour;
        }
    }

    public int Minute
    {
        get {
            return minute;
        }
    }

    // Last day before retirement, from 9:00 to 21:00,
    // in game otherwise, 12 minutes
    float time = 9f;
    const float endTime = 21f;

    float yRotate;

    int hour;
    int minute;

    void Start()
    {
        if (sunLight == null) {
            Debug.LogError("Fail to find light.");
            return;
        }

        yRotate = sunLight.transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        time += Time.deltaTime / 60;
        hour = (int)time;
        minute = (int)((time - hour) * 60);

        // Light rotate from -45 to 45, speed: Time.deltaTime * 90 / (12 * 60)
        yRotate += Time.deltaTime / 8;
        sunLight.transform.rotation = Quaternion.Euler(40f, yRotate, 0f);

        if (hour >= 17) {
            // Light color change to dusk Flame after 5 pm
            sunLight.color = Color.Lerp(daytimeColor, duskColor, (time - 17) / 4);
            // Light intensity decreases from 0.6 to 0.2, speed: Time.deltaTime * 0.4 / (4 * 60)
            sunLight.intensity -= Time.deltaTime / 600;
        } else if (hour > 21) {
            Debug.Log("TDBR End");
        }
    }
}
