using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoiceControl : MonoBehaviour {
    public AudioSource aud;

    // spectrum data
    float[] spectrum = new float[256];

    float threshold = -11.0f;

    void Start() {
        if (!aud)
            aud = GetComponent<AudioSource>();

        aud.clip = Microphone.Start(null, true, 1, 44100);
        aud.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        aud.Play();
    }

    void Update() {
        // getting spectrum data
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        //for (int i = 1; i < spectrum.Length - 1; i++) {
        //for (int i = 1; i < 20; i++) {
            //Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            //Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        //}
        //Debug.Log(GetAverageActiveLevel());
    }

    public float GetAverageActiveLevel() {
        float averageValue = 0.0f;
        float valueSum = 0.0f;
        int minFreq = 0;
        int maxFreq = 20;
        for (int i = minFreq; i < maxFreq; i++) {
            valueSum += spectrum[i];
        }
        averageValue = valueSum / (maxFreq - minFreq);
        return Mathf.Log(valueSum);
    }

    public float GetJumpValue() {
        float value = GetAverageActiveLevel();
        if(value > threshold) {
            return Mathf.Abs(value - threshold);
        }
        return 0.0f;
    }
}
