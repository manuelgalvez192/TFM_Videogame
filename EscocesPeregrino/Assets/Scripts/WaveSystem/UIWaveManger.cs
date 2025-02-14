using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveManger : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Text killsText;
    int totalKills;
    float currentTime;
    IEnumerator behavior;
    void Start()
    {
        behavior = Count();
        StartCoroutine(behavior);
        timeText.text = FloatToTime(0);
        killsText.text = totalKills.ToString();

    }
    IEnumerator Count()
    {
        while(true)
        {
            timeText.text = FloatToTime(currentTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    public void OnKillEnemy()
    {
        totalKills++;
        killsText.text = totalKills.ToString();
    }
    public void OnPlayerDie()
    {
        StopCoroutine(behavior);
    }
    public void OnPlayerResucite()
    {
        StartCoroutine(behavior);
    }
    string FloatToTime(float time)
    {
        int segundos = (int)time;
        int centesimas = (int)((time - segundos) * 100);
        int milisegundos = (int)(((time - segundos) * 100 - centesimas) * 1000);

        int horas = segundos / 3600;
        int minutos = (segundos % 3600) / 60;
        segundos = segundos % 60;

        return $"{horas:D2}:{minutos:D2}:{segundos:D2}";

        //return horas + ":" + minutos + ":" + segundos;
    }
}
