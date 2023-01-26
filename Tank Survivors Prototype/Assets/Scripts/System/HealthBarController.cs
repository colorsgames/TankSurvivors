using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private float timeView;
    [SerializeField] private float followSpeed;
    [SerializeField] private Image image;
    [SerializeField] private GameObject body;

    [SerializeField] Vector2 offset;

    float oldHealth;

    Camera cam;
    Player player;
    Transform target;

    private void Start()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>();
        oldHealth = player.Health;
        target = player.transform;
    }

    private void FixedUpdate()
    {
        Following();
    }

    public void UpdateValue(float currentHealth)
    {
        if (currentHealth <= 0) return;
        if (!body.activeInHierarchy)
            StartCoroutine(Show());
        image.fillAmount = currentHealth / oldHealth;
    }

    void Following()
    {
        Vector2 srcteenPos = cam.WorldToScreenPoint(target.position);
        Vector2 lerpPos = Vector2.Lerp(transform.position, srcteenPos + offset, followSpeed * Time.deltaTime);
        transform.position = lerpPos;
    }

    IEnumerator Show()
    {
        body.SetActive(true);
        yield return new WaitForSeconds(timeView);
        body.SetActive(false);
    }
}
