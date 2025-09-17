using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Clip audio � jouer quand le joueur saute (assign� dans l�Inspector)
    [SerializeField] AudioClip sfxJump;
    // Composant AudioSource qui jouera les sons
    private AudioSource audioSource;

    // Valeur d�entr�e horizontale (?1 = gauche, 0 = immobile, 1 = droite)
    private float x;
    // Composant pour g�rer l�affichage du sprite (retourner � gauche/droite)
    private SpriteRenderer spriteRenderer;
    // Composant pour g�rer les animations du joueur
    private Animator animator;
    // Composant physique pour g�rer les forces (notamment le saut)
    private Rigidbody2D rb;

    // Indique si le joueur doit sauter � la prochaine frame physique
    private bool jump = false;


    void Awake()
    {
        // R�cup�re les composants n�cessaires attach�s au GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // M�thode appel�e au lancement, vide ici mais disponible pour init
    }

    // Update est appel� une fois par frame (logique li�e aux entr�es joueur)
    void Update()
    {
        // ---- D�placement horizontal ----
        x = Input.GetAxis("Horizontal"); // r�cup�re l�input clavier/fl�ches
        animator.SetFloat("x", Mathf.Abs(x)); // anime la marche selon vitesse
        transform.Translate(Vector2.right * 7f * Time.deltaTime * x); // d�place le joueur

        // ---- Orientation du sprite ----
        if (x > 0f) { spriteRenderer.flipX = false; } // regarde � droite
        if (x < 0f) { spriteRenderer.flipX = true; }  // regarde � gauche

        // ---- Gestion du saut ----
        // Ancienne version comment�e (force directe au moment de l�appui)
        // if (Input.GetKeyDown(KeyCode.UpArrow)) { rb.AddForce(Vector2.up * 900f); }

        // Nouvelle version : d�clenche un "flag" de saut
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true; // signal qu�il faut sauter dans FixedUpdate
            audioSource.PlayOneShot(sfxJump); // joue le son du saut
        }

        // ---- Animation d�attaque ----
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Attack", true); // lance l�animation
        }
        else
        {
            animator.SetBool("Attack", false); // arr�te l�animation
        }
    }

    // FixedUpdate est appel� � chaque frame physique (id�al pour Rigidbody)
    private void FixedUpdate()
    {
        // D�placement horizontal r�p�t� ici (? doublon avec Update)
        transform.Translate(Vector2.right * 5f * Time.deltaTime * x);

        // ---- Saut ----
        if (jump) // si le flag est actif
        {
            jump = false; // r�initialise pour �viter des sauts infinis

            audioSource.PlayOneShot(sfxJump); // rejoue le son du saut (? doublon aussi)

            rb.AddForce(Vector2.up * 900f); // applique une force vers le haut
        }
    }
}
