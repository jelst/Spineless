using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound;
    public float volume;
    AudioSource audioSource;
    public bool coStarted = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && coStarted == false)
        {
            animator.SetBool("isCollected", true);
            audioSource.PlayOneShot(coinSound, volume);
            StartCoroutine(CoinCo());
            coStarted = true;
        }
    }
    private IEnumerator CoinCo()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }
}
