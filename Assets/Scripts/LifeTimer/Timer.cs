using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public IEnumerator ExpireThrough(float time)
	{
		var wait = new WaitForSeconds(time);
		yield return wait;
	}
}