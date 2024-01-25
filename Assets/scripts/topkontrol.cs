using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topkontrol : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text zaman, can, durum;
    Rigidbody rg;
    public float Hiz = 10.5f;
    float zamansayaci = 25;
    int cansayaci = 5;
    bool oyunDevam = true;
    bool oyuntamam = false;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyuntamam)
        {
            zamansayaci -= Time.deltaTime;
            zaman.text = (int)zamansayaci + "";
        }
        else if(! oyuntamam)
        {
            durum.text = "Oyun Tamamlanamadı!";
            btn.gameObject.SetActive(true);
        }
        if(zamansayaci < 0)
        {
            oyunDevam = false;
        }
    }
    private void FixedUpdate()
    {
        if (oyunDevam && !oyuntamam )
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(yatay, 0, dikey);
            rg.AddForce(kuvvet * Hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        string objname = collision.gameObject.name;
        if (objname.Equals("finish"))
        {
            //  print("Oyun tamamlandı"); //
            oyuntamam = true;
            durum.text = "Oyun Tamamlandı";
            btn.gameObject.SetActive(true);
        }
        else if (!objname.Equals("Plane") && !objname.Equals("zemin"))
        {

            cansayaci -= 1;
            can.text = cansayaci + " ";
            if (cansayaci == 0)
            {
                oyunDevam = false;
            }
        }
    }

}