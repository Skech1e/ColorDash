using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum AnimType
{
    Position, Scale, Fade
}
public enum Axis
{
    Horizontal, Vertical, Both
}
public class Tweener : MonoBehaviour
{
    public AnimType anim;
    public Axis axis;
    public Ease easeType;

    public float startValue, endValue;
    [Range(0f, 10f)]
    public float duration;
    [Range(0, 20f)]
    public float delayAnim;

    public bool RunOnEnable;
    public GameObject ParentToDisable;

    private void OnEnable()
    {
        if (RunOnEnable)
            Animate();
    }
    private void OnDisable()
    {
    }

    private void OnValidate()
    {
        if (anim == AnimType.Fade)
        {
            Image img;
            if (transform.TryGetComponent(out img))
            {
                endValue = img.color.a;
            }
        }
    }

    public void Disable()
    {
        Animate(true);
    }

    public void Animate(bool reverse = false) => StartCoroutine(Tween(reverse));

    IEnumerator Tween(bool reverse)
    {
        float fromVal = startValue;
        float toVal = endValue;
        if (reverse)
        {
            fromVal = endValue;
            toVal = startValue;
        }
        if (anim == AnimType.Position)
        {
            RectTransform rectTransform = transform.GetComponent<RectTransform>();
            Vector2 pos = transform.localPosition;
            switch (axis)
            {
                case Axis.Horizontal:
                    pos.x = fromVal;
                    transform.localPosition = pos;
                    break;
                case Axis.Vertical:
                    pos.y = fromVal;
                    transform.localPosition = pos;
                    break;
            }
            yield return new WaitForSecondsRealtime(delayAnim);
            rectTransform.DOAnchorPosY(toVal, duration).SetEase(easeType).SetUpdate(true);
        }
        else if (anim == AnimType.Scale)
        {
            Vector2 scale = transform.localScale;
            switch (axis)
            {
                case Axis.Horizontal:
                    scale.x = fromVal;
                    transform.localScale = scale;
                    break;
                case Axis.Vertical:
                    scale.y = fromVal;
                    transform.localScale = scale;
                    break;
                case Axis.Both:
                    scale.x = scale.y = fromVal;
                    transform.localScale = scale;                    
                    break;
            }
            yield return new WaitForSecondsRealtime(delayAnim);
            transform.DOScale(toVal, duration).SetEase(easeType).SetUpdate(true);
        }
        else
        {
            Image img;
            if (transform.TryGetComponent(out img))
            {
                var colour = img.color;
                colour.a = fromVal;
                img.color = colour;
                img.DOFade(toVal, duration).SetEase(easeType).SetUpdate(true);
            }

        }

        if (reverse)
        {
            yield return new WaitForSeconds(delayAnim + duration);
            ParentToDisable.SetActive(false);
        }

    }
}
