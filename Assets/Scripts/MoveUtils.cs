using System;
using System.Collections;
using UnityEngine;

public class MoveUtils
{
    public IEnumerator MoveToPosition(Transform obj, Vector3 targetPos, Quaternion targetRot, float moveSpeed, Action onComplete = null, bool rotate = false)
    {
        float elapsedTime = 0f;
        Vector3 startPos = obj.position;
        Quaternion startRot = obj.rotation;

        while (elapsedTime < 1f)
        {
            obj.position = Vector3.Lerp(startPos, targetPos, elapsedTime);
            if (rotate) obj.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        obj.position = targetPos;
        if (rotate) obj.rotation = targetRot;

        onComplete?.Invoke();
    }
}
