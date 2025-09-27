using UnityEngine;

public class Timer
{
    private readonly float duration; 
    private float endTime; 
    private bool running;
    public bool IsRunning => running;
    public float TimeLeft => Mathf.Max(0, endTime - Time.time);
    public bool IsFinished => !running && TimeLeft <= 0;

    public Timer(float seconds)
    {
        duration = seconds;
    }

    // Start or restart the timer
    public void Start()
    {
        endTime = Time.time + duration;
        running = true;

    }

    // Stop the timer early
    public void Stop() => running = false;
    

    // Call this once per frame if you want to auto-stop
    public void Update()
    {
        if (running && Time.time >= endTime)
        {
            Debug.Log("Timer finished");
            running = false;
        }
    }
}
