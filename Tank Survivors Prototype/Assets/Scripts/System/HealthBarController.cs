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
    float currentTime;

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

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime >= 0)
        {
            body.SetActive(true);
            Following();
        }
        else
            body.SetActive(false);
    }

    public void UpdateValue(float currentHealth)
    {
        if (currentHealth <= 0) return;
        currentTime = timeView;
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
