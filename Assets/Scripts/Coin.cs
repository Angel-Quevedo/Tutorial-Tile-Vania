using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int points = 100;
    [SerializeField] AudioClip SFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(SFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(points);
        Destroy(gameObject);
    }
}
