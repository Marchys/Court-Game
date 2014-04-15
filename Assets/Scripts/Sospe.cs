using UnityEngine;
using System.Collections;

public class Sospe : MonoBehaviour {

	// caracteristiques
	float velocitat = 5f;

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
		posicions_master=GameObject.FindWithTag("Posicions");
		GameObject sospitosos_master= GameObject.FindWithTag("Master");
		posi = sospitosos_master.GetComponent<Cre_des_sos>().li_posis;
		for(int i=1;i<=4;i++)
		{
			if(posi[i-1] == 0)
			{
				obj_pos=i+1;
				posi[i-1]=1;
				break;
			}
		}


		StartCoroutine(Next_position(false));
	}

	public void Seguent(bool ve)
	{

		if(saliendo==false)
		{
			if(primera)saliendo=true;
			StopCoroutine("Next_position");
			StartCoroutine(Next_position(ve));
		}

	}



	IEnumerator Next_position(bool master)
	{

		if(primera && master)
		{
			obj_pos=obj_pos-2;
			posi[3]=0;
			Vector3 temp_so=transform.localScale;;
			temp_so.x *= -1;
			transform.localScale = temp_so ;
			foreach (Transform fill in transform) {
				fill.renderer.sortingLayerName= "darrere";
			}
			} 
		else if(primera && master==false)
		{
			obj_pos--;
			posi[3]=0;

		}
		else obj_pos--;	
		if(obj_pos==1)primera=true;
		target = posicions_master.transform.Find("0"+obj_pos+"_pos");
		while(Vector2.Distance(transform.position,target.position)>0.1)		
		{
			float step = velocitat * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, target.position, step);
			yield return null;
		}
		if(obj_pos==0 || obj_pos==-1 )Destroy(gameObject);


	}





}
