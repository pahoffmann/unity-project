using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    // player shit
    [SerializeField] private float speed = 15f;
    [SerializeField] private int _lives = 3;
    private MaterialPropertyBlock _mpb;
    private Color _orange = new Color(1f, 0.5f, 0f);
    private Color _red = new Color(1f, 0f, 0f);
    private int shootPhase = 0;

    // vaccine shit
    [SerializeField] private GameObject _vaccinePrefab;
    [SerializeField] private float _vaccinationRate = 0.3f;
    private float _nextVaccination = 0f;

    // wavemanager needs to know, when this dude is dead.
    [SerializeField] private WaveManager _waveManager;
    
    // UV light shit
    [SerializeField] private GameObject _UVLightPrefab;
    [SerializeField] private bool _useUVLight = false;
    
    // UIManager shit
    [SerializeField] private UIManager _UIManager;
    
    //DamageSound
    public AudioClip notGood;

    // Start is called before the first frame update
    void Start()
    {
        // instantiate MaterialPropertyBlock
        if (_mpb == null)
        {
            _mpb = new MaterialPropertyBlock();
            _mpb.Clear();
            this.GetComponent<Renderer>().GetPropertyBlock(_mpb);
        }
        
        
        // spawn player
        transform.position = new Vector3(0, -2, 0);
        
        _UIManager.setLives(_lives);
    }
    
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        RotatePlayer();
        Vaccine();
    }
    
    /// <summary>
    /// player rotation
    /// </summary>
    void RotatePlayer()
    {
        // rotates the player instantly to a fixed angle whilst moving
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = new Quaternion(0f, 1f, 0f, 4f);
        } else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            transform.rotation = Quaternion.identity;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = new Quaternion(0f, -1f, 0f, 4f);
        } else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            transform.rotation = Quaternion.identity;
        } 
    }
    
    /// <summary>
    /// player movement and boundary handling
    /// </summary>
    void PlayerMovement()
    {
        // move the player around
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis(("Vertical"));
        float baseDirectionalTranslate = 1 * Time.deltaTime * speed; // precalculate this for better efficiency
        Vector3 playerTranslate = new Vector3(baseDirectionalTranslate * horizontalInput,
            baseDirectionalTranslate * verticalInput, 0f);
        transform.Translate(playerTranslate, Space.World);

        // get transform.position once to avoid redundancy
        Vector3 pos = transform.position;
        
        // use values from constants class instead of hardcoding it.
        float ceiling   = Constants.Dimensions.BorderTop, 
              floor     = Constants.Dimensions.BorderBottom,
              leftWall  = Constants.Dimensions.BorderLeft,
              rightWall = Constants.Dimensions.BorderRight;
        
        // player boundaries
        // top, bottom
        if (pos.y > ceiling)
        {
            transform.position = new Vector3(pos.x, ceiling, 0);
        } else if (pos.y < floor)
        {
            transform.position = new Vector3(pos.x, floor, 0);
        }

        // left, right
        if (pos.x > rightWall)
        {
            transform.position = new Vector3(leftWall, pos.y, 0);
        } else if (pos.x < leftWall)
        {
            transform.position = new Vector3(rightWall, pos.y, 0);
        }
    }

    /// <summary>
    /// spawns Vaccines and UV Lights
    /// </summary>
    void Vaccine()
    {
        // instantiate vaccine prefab on spacebar button press
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextVaccination)
        {
            _nextVaccination = Time.time + _vaccinationRate;

            if (_useUVLight)
            {
                Instantiate(_UVLightPrefab, transform.position + new Vector3(0f, -2f, 0f),
                    Quaternion.identity);
            }
            else
            {
                switch (shootPhase)
                {
                    case 0:
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            Quaternion.identity);
                        break;
                    case 1:
                        // shoot two vaccines
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(-0.2f, 0.7f, 0f),
                            Quaternion.identity);
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0.2f, 0.7f, 0f),
                            Quaternion.identity);
                        
                        break;
                    case 2:
                        // shoot 3 vaccines at an angle
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            Quaternion.identity);
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            new Quaternion(0, 0f, -0.2f, 1));
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            new Quaternion(0, 0f, 0.2f, 1));
                        break;
                    case 3:
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            new Quaternion(0, 0f, -0.2f, 1));
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            new Quaternion(0, 0f, -0.1f, 1));
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            new Quaternion(0, 0f, 0.1f, 1));
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            new Quaternion(0, 0f, 0.2f, 1));
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            Quaternion.identity);
                        break;
                    default:
                        Instantiate(_vaccinePrefab, transform.position + new Vector3(0f, 0.7f, 0f),
                            Quaternion.identity);
                        break;
                }
            }
        }
    }

   //gets called by the HeartPowerUp script and adds a life:D
    public void OneMoreLife()
    {
        _lives++;
        _UIManager.setLives(_lives);
    }
    
    /// <summary>
    /// damages the player if hit by Corona
    /// </summary>
    public void Damage()
    {
        // reduce amount of lives
        _lives--;
        //sound effect
        AudioSource.PlayClipAtPoint(notGood, transform.position);
        _UIManager.setLives(_lives);

        switch (_lives)
        {
            case 0:
                
                //maybe destroy leftover viruses (stop spawning)

                _UIManager.GameOver(true);
                
                // change da world. My final message, Goodbye.
                Destroy(this.gameObject);
                
                _waveManager.onPlayerDeath();
                
                break;
            case 1:
                // change the color when damaged
                // _mpb.SetColor("_Color", _red);
                // this.GetComponent<Renderer>().SetPropertyBlock(_mpb);
                break;
            case 2:
                // change the color when damaged
                // _mpb.SetColor("_Color", _orange);
                // this.GetComponent<Renderer>().SetPropertyBlock(_mpb);
                break;
        }
    }

    /// <summary>
    /// activates the UV light for a given time by starting coroutine
    /// </summary>
    /// <param name="duration"></param>
    public void ActivateUVLight(float duration)
    {
        StartCoroutine(ActivateUVLightCoroutine(duration));
    }

    /// <summary>
    /// activates the UV light for a given time
    /// </summary>
    /// <param name="duration"></param>
    /// <returns>IEnumerator for starting the Coroutine</returns>
    IEnumerator ActivateUVLightCoroutine(float duration)
    {
        _useUVLight = true;
        _vaccinationRate = 0.1f;
        yield return new WaitForSeconds(duration);
        _useUVLight = false;
        _vaccinationRate = 0.3f;
    }

    public void UpgradeShot()
    {
        if (shootPhase <= 2)
        {
            _vaccinationRate -= 0.05f;
            shootPhase++;
        }
            
    }
    
}
