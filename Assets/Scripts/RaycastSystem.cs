using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSystem : MonoBehaviour
{
    public static RaycastSystem instance;
    [SerializeField] private LayerMask katman;
    [SerializeField] private float distance;
    [SerializeField] private GameObject karakter;
    public bool move;
    [SerializeField] private AudioSource sound;
    private bool SesAcikMi;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        transform.position -= new Vector3(0, transform.position.y, 0);
        RaycastAt();
    }

    public void RaycastAt()
    {
        if (!move)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, katman))
            {
                Debug.Log("Ray");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);

                if (hit.collider.gameObject.CompareTag("Collectables"))
                {
                    StartCoroutine(Ilerle());
                  //  StartCoroutine(SoundOperationn());
                    move = true;
                }
                if (hit.collider.gameObject.CompareTag("Untagged"))
                {
                    StartCoroutine(Ilerle());
                    move = true;
                }
                if (hit.collider.gameObject.CompareTag("Bridge"))
                {
                    StartCoroutine(Ilerle());
                  //  StartCoroutine(SoundOperationn());
                    move = true;
                }
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    sound.Stop();
                }
            }
            else
            {
                sound.Stop();
            }
        }

    }

    IEnumerator Ilerle()
    {
        yield return new WaitForSeconds(.005f);
        karakter.transform.position += karakter.transform.TransformDirection(Vector3.forward * 1.05f);
        move = false;
    }
    IEnumerator SoundOperationn()
    {
        if (!SesAcikMi)
        {
            if (!sound.isPlaying)
            {
                sound.Play();
                for(float f=1; f<=2; f += Time.deltaTime)
                {
                    sound.pitch = f;
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                yield return new WaitForSeconds(2);
                sound.Stop();
                SesAcikMi = false;
                sound.pitch = 1;
            }

        }
    }

}
