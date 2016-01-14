using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {
    [SerializeField]
    private float lifeTime = 1;

	void Start () {
        //starts with the destroy function when it is initiated.
        StartCoroutine(Destroy());
	}
    
	IEnumerator Destroy()
    {
        //waits for lifetime seconds
        yield return new WaitForSeconds(lifeTime);

        //destroys itself
        Destroy(this.gameObject);
    }
}
