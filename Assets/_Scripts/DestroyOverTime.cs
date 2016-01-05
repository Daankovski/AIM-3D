using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {
    [SerializeField]
    private float lifeTime = 1;
	void Start () {
        StartCoroutine(Destroy());
	}
	IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
