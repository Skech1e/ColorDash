using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }
    [SerializeField] GameEvents events;
    SpriteRenderer spr;
    public E_Colour colour;

    [SerializeField] private float speed, multiplier = 1f;
    Rigidbody2D rb2d;
    Animator anim;
    Vector2 startPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        StopPlayer();
    }

    private void OnEnable()
    {
        events.RestartGame += ResetPlayer;
    }
    private void OnDisable()
    {
        events.RestartGame -= ResetPlayer;
    }

    private void Update()
    {
        rb2d.position += speed * multiplier * Time.deltaTime * Vector2.right;
    }

    public void ColourShift(int index)
    {
        colour = (E_Colour)index;
        spr.color = ColorManager.GetColor(colour);
    }

    public void IncreaseSpeed()
    {
        multiplier += 0.125f;
        anim.speed = multiplier;
    }

    public void StopPlayer()
    {
        multiplier = 0f;
        anim.speed = multiplier;
    }

    void ResetPlayer()
    {
        transform.position = startPos;
        colour = E_Colour.White;
        spr.color = ColorManager.GetColor(colour);
        multiplier = 1f;
        anim.speed = multiplier;
    }
}
