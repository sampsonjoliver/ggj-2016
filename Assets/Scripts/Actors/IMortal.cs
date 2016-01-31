using UnityEngine;
using System.Collections;

public abstract class IMortal : MonoBehaviour {
	public abstract void OnImminentDeath();
    public abstract void OnDeath();
}
