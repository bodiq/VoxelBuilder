using System.Linq;
using DG.Tweening;
using UnityEngine;

public static class BaseUtility
{
    #region Timers

    public static Tweener Timer(TweenCallback action, float delay)
    {
        var timer = DOTween.To(_ => { }, 0.0f, 1.0f, delay);
        timer.OnComplete(action);
        timer.SetEase(Ease.Linear);
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

    public static void Reset(this Rigidbody2D rigidbody)
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0.0f;
    }

    #endregion


    #region Collision

    public static void SetDynamic(this Collision collision)
    {
        var rigidbody = collision.rigidbody;
        rigidbody.isKinematic = false;
        rigidbody.AddForce(collision.relativeVelocity, ForceMode.Force);
    }

    #endregion


    #region Float

    public static float Lerp3(float a, float b, float c, float t)
    {
        return Mathf.LerpUnclamped(b, Mathf.LerpUnclamped(a, c, t), Mathf.Abs(t - 0.5f) * 2.0f);
    }

    #endregion


    #region Vector2

    public static Vector2 Lerp3(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        return new Vector2(Lerp3(a.x, b.x, c.x, t), Lerp3(a.y, b.y, c.y, t));
    }

    #endregion


    #region Vector3

    public static Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return new Vector3(Lerp3(a.x, b.x, c.x, t), Lerp3(a.y, b.y, c.y, t), Lerp3(a.z, b.z, c.z, t));
    }

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
}