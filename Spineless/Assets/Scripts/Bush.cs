using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public AudioClip bushSound;
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
        if (other.CompareTag("PlayerAttack") && coStarted == false)
        {
            animator.SetBool("isDestroyed", true);
            audioSource.PlayOneShot(bushSound, volume);
            StartCoroutine(BushCo());
            coStarted = true;
        }
    }
    private IEnumerator BushCo()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }
}
