using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GateData data;

    [SerializeField] 
    SpriteRenderer spr;
    Collider2D col2d;
    [SerializeField] GameEvents events;

    private void Awake()
    {
        SetGateColor();
    }

    public void SetGateColor()
    {
        E_Colour colour = (E_Colour)Random.Range(0, 3);
        Color c = ColorManager.GetColor(colour);
        c.a = 0.8f;
        spr.color = c;
        data = new(colour);
    }
        
    private IEnumerator FadeOut()
    {
        float time = 0;
        Color c = spr.color;
        while(time < 1f)
        {
            c.a = Mathf.Lerp(c.a, 0, time);
            spr.color = c;
            time += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        col2d = collision;
        if(col2d.TryGetComponent<Player>(out var player))
        {
            if(player.colour == data.colour)
            {
                data.IsPassed();
                events.RaisePlayerAtGate();
                player.IncreaseSpeed();
            }
            else
            {
                player.StopPlayer();
                events.RaisePlayerNoEntry();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == col2d && data.isPassed)
        {
            events.RaisePlayerPassed();
            StartCoroutine(FadeOut());
            col2d = null;
            data.Reset();
        }
    }
}
