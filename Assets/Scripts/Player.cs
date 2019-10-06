using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, INotificationListener
{
    // PROPERTIES
    // -------------------------------------------------------------------------

    [SerializeField] [Range(0.1f, 0.9f)] private float m_moveSpeed = 0.5f;
    [SerializeField] private LayerMask m_wallsLayer;
    [SerializeField] private GameObject m_sprites;
    [SerializeField] private SpriteRenderer m_bodySpriteRenderer;
    [SerializeField] private SpriteRenderer m_faceSpriteRenderer;
    [SerializeField] private bool m_isFacingRight = true;

    [Header("Face Sprites")]
    
    [SerializeField] private bool m_hasFixedFace = false;
    [SerializeField] private Sprite m_spriteFace0;
    [SerializeField] private Sprite m_spriteFace25;
    [SerializeField] private Sprite m_spriteFace50;
    [SerializeField] private Sprite m_spriteFace75;
    [SerializeField] private Sprite m_spriteFace100;
    [SerializeField] private Sprite m_spriteFaceRejected;
    [SerializeField] private Sprite m_spriteFaceSad;
    
    [Header("Face Sprites")]
    
    [SerializeField] private Sprite m_spriteBody0;
    [SerializeField] private Sprite m_spriteBody25;
    [SerializeField] private Sprite m_spriteBody50;
    [SerializeField] private Sprite m_spriteBody75;
    [SerializeField] private Sprite m_spriteBody100;
    
    private float m_fillPercent;
    private bool m_isMoving = false;
    private Vector3 m_velocity;

    // ACCESSORS
    // -------------------------------------------------------------------------

    public float fillPercent => m_fillPercent;

    // PUBLIC METHODS
    // -------------------------------------------------------------------------
    
    public void OnNotification(Notification notification)
    {
        switch (notification.type) {
            case Notification.Type.POTION_BLOCKED:
                m_faceSpriteRenderer.sprite = m_spriteFaceSad;
                break;
        }
    }
    
    // PRIVATE METHODS
    // -------------------------------------------------------------------------

    private void Start()
    {
        GameController.instance.notifications.Subscribe(Notification.Type.POTION_BLOCKED, this);

        if (!m_hasFixedFace) {
            SetSprites();
        }
    }

    private void Update()
    {
        CheckInput();
    }

    private void OnDestroy()
    {
        GameController.instance.notifications.Unsubscribe(Notification.Type.POTION_BLOCKED, this);
    }

    private void CheckInput()
    {
        if (m_isMoving || GameController.instance.isPaused) {
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            MoveToward(Vector2.right);
            SetFacingRight(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            MoveToward(Vector2.left);
            SetFacingRight(false);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            MoveToward(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            MoveToward(Vector2.down);
        }
    }

    private void MoveToward(Vector3 offset)
    {
        Vector3 destination = transform.position + offset;

        // Check if there is a wall there.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, offset.magnitude, m_wallsLayer);
        if (!hit.collider) {
            StartCoroutine(MoveTo(destination));
            return;
        }
        
        if (hit.collider.CompareTag("Exit")) {
            Exit exit = hit.collider.GetComponent<Exit>();
            if (m_fillPercent >= exit.requiredLevel) {
                GameController.instance.notifications.Notify(new LevelClearedNotification());
                StartCoroutine(MoveTo(destination));
            }
            else {
                GameController.instance.sounds.PlayRejected();
            }

            return;
        }

        if (hit.collider.CompareTag("Rock")) {
            Rock rock = hit.collider.GetComponent<Rock>();
            if (rock.Push(offset, gameObject)) {
                StartCoroutine(MoveTo(destination));
            }
        }
    }

    private IEnumerator MoveTo(Vector3 destination)
    {
        m_isMoving = true;
        float distance;
        Vector3 direction = (destination - transform.position).normalized;

        do {
            Vector3 currentPosition = transform.position;
            distance = Vector3.Distance(currentPosition, destination);
            float moveDistance = Mathf.Lerp(0, distance, m_moveSpeed);
            transform.position = currentPosition + (direction * moveDistance);
            yield return null;
        } while (distance > 0.15f);

        transform.position = destination;
        m_isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Potion potion = other.GetComponent<Potion>();
        if (potion && !potion.isEmpty) {
            m_fillPercent += potion.value;
            potion.Empty();
            SetSprites();
            GameController.instance.sounds.PlayValueIncrement();
        }
    }

    private void SetSprites()
    {
        if (m_fillPercent < 0.25f) {
            m_bodySpriteRenderer.sprite = m_spriteBody0;
            m_faceSpriteRenderer.sprite = m_spriteFace0;
        }
        else if (m_fillPercent < 0.50f) {
            m_bodySpriteRenderer.sprite = m_spriteBody25;
            m_faceSpriteRenderer.sprite = m_spriteFace25;
        }
        else if (m_fillPercent < 0.75f) {
            m_bodySpriteRenderer.sprite = m_spriteBody50;
            m_faceSpriteRenderer.sprite = m_spriteFace50;
        }
        else if (m_fillPercent < 1.0f) {
            m_bodySpriteRenderer.sprite = m_spriteBody75;
            m_faceSpriteRenderer.sprite = m_spriteFace75;
        }
        else {
            m_bodySpriteRenderer.sprite = m_spriteBody100;
            m_faceSpriteRenderer.sprite = m_spriteFace100;
        }
    }

    private void SetFacingRight(bool isFacingRight)
    {
        if (m_isFacingRight == isFacingRight) {
            return;
        }

        m_isFacingRight = isFacingRight;

        Vector3 scale = m_sprites.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (m_isFacingRight ? 1f : -1f);
        m_sprites.transform.localScale = scale;
    }
}