using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

public static class Goodies
{
    #region Timers

    public static Tweener Timer(TweenCallback action, float delay)
    {
        var timer = DOTween.To(_ => { }, 0.0f, 1.0f, delay);
        timer.OnComplete(action);
        timer.SetEase(Ease.Linear);
        return timer;
    }

    public static Tweener OverAction(DOSetter<float> action, float delay = 1.0f)
    {
        var timer = DOTween.To(action, 0.0f, 1.0f, delay);
        timer.SetEase(Ease.Linear);
        return timer;
    }

    public static Tweener Loop(TweenCallback action, float delay)
    {
        var timer = DOTween.To(_ => { }, 0.0f, 1.0f, delay);
        timer.OnStepComplete(action);
        timer.SetEase(Ease.Linear).SetLoops(-1);
        return timer;
    }

    #endregion


    #region String

    public static string ReplaceChar(this string str, string chr, int index)
    {
        str = str.Remove(index, chr.Length);
        str = str.Insert(index, chr);
        return str;
    }

    public static string ToShort(this float f)
    {
        return f switch
        {
            > 1000000000 => (f / 1000000000).ToString("0.0") + "B",
            > 1000000 => (f / 1000000).ToString("0.0") + "M",
            > 1000 => (f / 1000).ToString("0.0") + "K",
            _ => f.ToString("0.0")
        };
    }

    #endregion


    #region Animator

    public static bool HasParameter(this Animator animator, string name)
    {
        return animator.parameters.Any(parameter => parameter.name == name);
    }

    #endregion


    #region Rigidbody

    public static void Reset(this Rigidbody rigidbody)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public static void SetDynamic(this Rigidbody rigidbody, Collision collision, float multiplier = 1.0f)
    {
        rigidbody.SetDynamic(collision.relativeVelocity * multiplier, collision.rigidbody);
    }

    public static void SetDynamic(this Rigidbody rigidbody, Vector3 relativeVelocity, Rigidbody causeRigidbody = null)
    {
        rigidbody.isKinematic = false;

        rigidbody.AddForce(relativeVelocity, ForceMode.Force);
        if (causeRigidbody != null) causeRigidbody.AddForce(relativeVelocity, ForceMode.Force);
    }

    public static bool IsMoving(this Rigidbody rigidbody, float detectionStep = 0.1f)
    {
        var velocity = rigidbody.velocity;
        return velocity.x > detectionStep || velocity.y > detectionStep || velocity.z > detectionStep;
    }

    public static bool IsRotating(this Rigidbody rigidbody, float detectionStep = 10.0f)
    {
        var velocity = rigidbody.angularVelocity;
        return velocity.x > detectionStep || velocity.y > detectionStep || velocity.z > detectionStep;
    }

    public static void OnPhysicsSleep(Action action, float delay = 1.0f)
    {
        var rigidbodies = Object.FindObjectsOfType<Rigidbody>();
        OnPhysicsSleep(rigidbodies, action, delay);
    }

    public static void OnPhysicsSleep(this IEnumerable<Rigidbody> rigidbodies, Action action, float delay = 1.0f)
    {
        bool IsMovingOrRotating(Rigidbody rigidbody)
        {
            return rigidbody != null && (IsMoving(rigidbody) || IsRotating(rigidbody));
        }

        Loop(() =>
        {
            if (!rigidbodies.Any(IsMovingOrRotating)) action?.Invoke();
        }, delay);
    }

    #endregion


    #region Vector3

    public static Vector3 Rotate(this Vector3 vector, Vector3 angles)
    {
        return Quaternion.Euler(angles) * vector;
    }

    public static Vector3 RotateAround(this Vector3 vector, Vector3 point, Vector3 angles)
    {
        var dir = vector - point;
        dir = Quaternion.Euler(angles) * dir;
        return dir + point;
    }

    public static float Max(this Vector3 vector)
    {
        return Mathf.Max(vector.x, vector.y, vector.z);
    }

    public static float Min(this Vector3 vector)
    {
        return Mathf.Min(vector.x, vector.y, vector.z);
    }

    public static Vector3 Multiply(this Vector3 vectorA, Vector3 vectorB)
    {
        vectorA.x *= vectorB.x;
        vectorA.y *= vectorB.y;
        vectorA.z *= vectorB.z;
        return vectorA;
    }

    public static Vector3 Divide(this Vector3 vectorA, Vector3 vectorB)
    {
        vectorA.x /= vectorB.x;
        vectorA.y /= vectorB.y;
        vectorA.z /= vectorB.z;
        return vectorA;
    }

    public static Vector3 AddFloat(this Vector3 vector, float f)
    {
        vector.x += f;
        vector.y += f;
        vector.z += f;
        return vector;
    }

    public static Vector3 OneMinus(this Vector3 vector)
    {
        vector.x = 1.0f - vector.x;
        vector.y = 1.0f - vector.y;
        vector.z = 1.0f - vector.z;
        return vector;
    }

    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

    public static float Sum(this Vector3 vector)
    {
        return vector.x + vector.y + vector.z;
    }

    public static Vector3 Clamp(this Vector3 vector, float min, float max)
    {
        vector.x = Mathf.Clamp(vector.x, min, max);
        vector.y = Mathf.Clamp(vector.y, min, max);
        vector.z = Mathf.Clamp(vector.z, min, max);
        return vector;
    }

    #endregion


    #region Texture

    public static Vector2 Size(this Texture texture)
    {
        return new Vector2(texture.width, texture.height);
    }

    public static Texture2D Copy(this Texture texture)
    {
        var sourceTexture = texture as Texture2D;
        if (sourceTexture == null) return new Texture2D(2048, 2048);

        var copiedTexture = new Texture2D(sourceTexture.width, sourceTexture.height);
        copiedTexture.SetPixels(sourceTexture.GetPixels());
        return copiedTexture;
    }

    #endregion


    #region Color

    public static float Sum(this Color color)
    {
        return color.r + color.g + color.b + color.a;
    }

    #endregion


    #region Component

    public static Component CopyTo(this Component original, GameObject target)
    {
        var type = original.GetType();
        var copy = target.AddComponent(type);
        var fields = type.GetFields();
        foreach (var field in fields) field.SetValue(copy, field.GetValue(original));
        return copy;
    }

    #endregion

    #region Touch

    public static bool IsOverUI(this Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

    #endregion
}