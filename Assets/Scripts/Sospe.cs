using UnityEngine;
using System.Collections;

public class Sospe : MonoBehaviour {

	// caracteristiques
	float velocitat = 5f;
	public int sospi_num;
	Cre_des_sos script_master;
	public int po_num = 5;
    public bool last_cre = false;
	public int coss;
	public bool culpable = false;
	Animator anim_cos;
	Animator anim_cap;

	// posicio actual
    bool primera = false;

	// posicions ocupades
	int[] posi = new int[4];

	// posicio objectiu
	Transform target;
	int obj_pos=0;

	// posis origen master
	GameObject posicions_master;

	// espera/refresh
	bool saliendo=false;

	// Use this for initialization
	void Start ()
	{

		anim_cos = transform.GetChild(4).GetComponent<Animator>();
		anim_cap = GetComponent<Animator>();
		anim_cos.SetInteger("cos", coss);
		anim_cap.SetInteger("Movimentes",50);	
		script_master =transform.parent.gameObject.GetComponent<Cre_des_sos>();	
		posicions_master=GameObject.FindWithTag("Posicions");
		if (!last_cre) {
						GameObject sospitosos_master = GameObject.FindWithTag ("Master");
						posi = sospitosos_master.GetComponent<Cre_des_sos> ().li_posis;
						for (int i=1; i<=4; i++) {

								if (posi [i - 1] == 0) {
										obj_pos = i + 1;
										posi [i - 1] = 1;
										break;
								}
						
						}
						StartCoroutine (Next_position (false, true));
				} else obj_pos = po_num;
		       
	
		
	}
	
	public void Seguent(bool ve, bool po)
	{
		if(!po && !ve)po_num--;
		if(saliendo==false)
		{
			if(po)if(primera)saliendo=true;
			StopCoroutine("Next_position");
			StartCoroutine(Next_position(ve,po));
		}

	}



	IEnumerator Next_position(bool master, bool pocs)
	{

		bool gir = false;
		if(primera && master)
		{
			if(pocs)obj_pos=obj_pos-2;
			else 
			{
			obj_pos--;
			if(obj_pos==0)
			{
		    obj_pos=po_num;
			primera=false;
			}

			}				
			posi[3]=0;
			canviar_capa("Darrere");
			gir = true;
		} 
		else if(primera && master==false)
		{

			obj_pos--;
			posi[3]=0;
			script_master.array_total_sos[sospi_num]=0;
			if(!pocs)saliendo=true;
							

		}
		else obj_pos--;	
		if(obj_pos==1)primera=true;
		target = posicions_master.transform.Find("0"+obj_pos+"_pos");
		anim_cos.SetInteger("Moviment",1);
		anim_cap.SetInteger("Movimentes",2);
		while(Vector2.Distance(transform.position,target.position)>0.1)		
		{
			float step = velocitat * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, target.position, step);
			yield return null;
		}
		anim_cos.SetInteger("Moviment",0);
        anim_cap.SetInteger("Movimentes",1);
		if(!pocs && gir) canviar_capa("Davant");
		
		if(obj_pos==0 || obj_pos==-1 )Destroy(gameObject);

	}


	void canviar_capa(string pos)
	{
		Vector3 temp_so=transform.localScale;
		temp_so.x *= -1;
		transform.localScale = temp_so ;
		foreach (Transform fill in transform) 
		{
			fill.renderer.sortingLayerName= pos;
		}
	}


}
