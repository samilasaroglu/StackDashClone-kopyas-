using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectables = new List<GameObject>();
    [SerializeField] private GameObject StackObject;
    [SerializeField] private GameObject karakter;
    [SerializeField] private Material planeMaterial;
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectables"))
        {
            other.tag = "Untagged";
            sound.PlayOneShot(clip);
            sound.pitch += .1f;
            StackEkle(other.gameObject, collectables.Count);
        }
        if (other.CompareTag("Bridge"))
        {
            sound.PlayOneShot(clip);
            sound.pitch -= .2f;
            other.tag = "Untagged";
            other.GetComponent<MeshRenderer>().material = planeMaterial;
            StackCikar();
        }
    }

    public void StackEkle(GameObject other,int index)
    {

        other.transform.SetParent(StackObject.transform);
        //Karakteri .11 arttır collectable yi sıfırla sonra localY sini -index*1.1f yap
        karakter.transform.position += new Vector3(0, .11f, 0);
        Vector3 pos = Vector3.zero - new Vector3(0,index*1.1f,0);
        other.transform.localPosition = pos;
        collectables.Add(other);


    }

    public void StackCikar()
    {
        collectables[collectables.Count - 1].transform.SetParent(null);
        collectables[collectables.Count - 1].SetActive(false);
        collectables.RemoveAt(collectables.Count - 1);
        karakter.transform.position -= new Vector3(0, .11f, 0); 
    }
}
