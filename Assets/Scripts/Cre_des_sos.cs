using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]
public class Cre_des_sos : MonoBehaviour 
{

	#region variables

	public GameObject p_base;
	public Transform or_sortida;
	//sons
	public AudioClip[] sons; 
	public GUIText text_fin;
	public GUITexture check_but;
	public GUIText[] pistes;
	//sprites sospitosos
	public Sprite[] caps_spirites;
	public Sprite[] ulls_spirites;
	public Sprite[] boques_spirites;
	public Sprite[] testa_spirites;
	public Sprite[] cossos_spirites;

	//llistes
	List<Sprite[]> Sprite_base = new List<Sprite[]>();

	List<int> llista_1 = new List<int>();
	List<int> llista_2 = new List<int>();
	List<int> llista_3 = new List<int>();

	List<int> llista_1_ex = new List<int>();
	List<int> llista_2_ex = new List<int>();
	List<int> llista_3_ex = new List<int>();

	List<int> llista_2_01 = new List<int>();
	List<int> llista_2_02 = new List<int>(); 
	List<int> llista_2_03 = new List<int>();

	List<string> car_totals_pos = new List<string>();
	List<string> car_totals_neg = new List<string>();
	List<string> resultat_carac = new List<string>();

	int l_2_1;
	int l_2_2;
	int l_2_3;

	//contador_llistes
	int ll_1;
	int ll_2;
	int ll_3;
	
	int ll_1_ex;
	int ll_2_ex;
	int ll_3_ex;

	//altres
	int avis=0;
	// sospitosos actuals
	[HideInInspector]
	public int[] li_posis = new int[4] {0, 0, 0, 0};
	// tots sospitosos
	[HideInInspector]
	public int[] array_total_sos = new int[25];
    int fila_sos = 0;
	int temp_num_sospe=0;
	//caracteristiques sospitosos
	int[] car_sos_b = new int[2] {5, 5};
	int[] car_sos_c = new int[3] {0, 0, 0};
	string[] car_sos_c_s = new string[3] {"", "", ""};
	List<int> carac_resta = new List<int>();
	List<int> num_parts= new List<int>();
	//culpable
	int culpable;

	// nombre de parts
	int num_caps;
	int num_ulls;
	int num_boques;
	int num_super;
	int num_cos;

	//textures gui
	public GUITexture dret;
	public GUITexture esquerre;

	//refresh/espera
	bool gou = true;
	#endregion

	#region principi
	void Start () 
	{	
	//GameObject sospi = Instantiate(p_base,transform.position,Quaternion.identity) as GameObject;
	//sospi.transform.parent = GameObject.Find("Sospitosos").transform;
	Screen.orientation = ScreenOrientation.LandscapeLeft;
	pistes[3].text="        Loading...";
	check_but.guiTexture.enabled = false;		   
	    Sprite_base.Add(ulls_spirites);
	    Sprite_base.Add(boques_spirites);
		Sprite_base.Add(testa_spirites);
		Sprite_base.Add(cossos_spirites);
		carac_resta.Add(0);
		carac_resta.Add(1);
		carac_resta.Add(2);
		carac_resta.Add(3);
		carac_resta.Add(4);
	StartCoroutine(Proces_joc());	
	}

	IEnumerator Proces_joc(){
	yield return StartCoroutine(Generar_car());		
	yield return StartCoroutine(Pistes());
	yield return StartCoroutine(Iniciar_joc());
    yield return StartCoroutine(Base_joc());
	yield return StartCoroutine(Comp_en());
	Debug.Log("fi");
	}
	#endregion

	#region Generar_car
	IEnumerator Generar_car()
	{
		int contc = 0;
		while(contc<2)
		{
			int temp_num_gen = Random.Range(0,5);
			if(!car_sos_b.Contains(temp_num_gen))
			{
				car_sos_c[contc]=Random.Range(0,2);
				car_sos_b[contc] = temp_num_gen;
				contc++;			   
			}
		}	
		car_sos_c[2]=Random.Range(0,2);
		for (int i=0; i<3; i++) {
			car_sos_c_s[i]=car_sos_c[i].ToString();	
		}
		int[] ordenat = car_sos_b.OrderBy(i => i).ToArray();
			   
		for (int i=1; i>=0;i--)
		{
			carac_resta[ordenat[i]]=8;
			if(ordenat[i]==3) avis+=3;
			else if(ordenat[i]==4) avis+=2;
			else Sprite_base.RemoveAt(ordenat[i]);
		}

		switch (avis) 
		{
		case 0:
			for (int i=0; i<2;i++)
			{
				int cont=0;
				string temp_se;
				string temp_cs=car_sos_c_s[2]+car_sos_c_s[1];
				if(car_sos_c[2]==1)temp_se="0";
				else temp_se="1";
				if(car_sos_c_s[1]==car_sos_c_s[2]){
					temp_cs=temp_se+temp_se;
				}
				foreach(Sprite sas in Sprite_base[i])
				{
					switch(i)
					{
					case 0:
						if(sas.name==car_sos_c_s[0])llista_1.Add(cont);
						else llista_1_ex.Add(cont);
						break;
					case 1:
						if(sas.name==car_sos_c_s[1]+car_sos_c_s[2])llista_2.Add(cont);
						else llista_2_ex.Add(cont);
						if(sas.name==temp_cs)llista_2_01.Add(cont);
						else if(sas.name==temp_se+car_sos_c_s[1])llista_2_02.Add(cont);
						else llista_2_03.Add(cont);
						break;					
					}					
					cont++;
				}
			}
			    break;		
		case 3:
			for (int i=0; i<3;i++)
			{
				int cont=0;
				foreach(Sprite sas in Sprite_base[i])
				{

					switch(i)
					{
					case 0:
						if(sas.name==car_sos_c_s[0])llista_1.Add(cont);
						else llista_1_ex.Add(cont);
						break;
					case 1:
						if(sas.name==car_sos_c_s[1])llista_2.Add(cont);
						else llista_2_ex.Add(cont);
						break;					
					case 2:
						if(sas.name=="0"+car_sos_c_s[2] || sas.name=="1"+car_sos_c_s[2])llista_3.Add(cont);
						else llista_3_ex.Add(cont);
						break;
					}
					cont++;
				}
			}
				break;
		case 2:
			for (int i=0; i<3;i++)
			{
				int cont=0;
				foreach(Sprite sas in Sprite_base[i])
				{
					switch(i)
					{
					case 0:
						if(sas.name==car_sos_c_s[0])llista_1.Add(cont);
						else llista_1_ex.Add(cont);
						break;
					case 1:
						if(sas.name==car_sos_c_s[1])llista_2.Add(cont);
						else llista_2_ex.Add(cont);
						break;						
					case 2:
						if(sas.name==car_sos_c_s[2]+"0" || sas.name==car_sos_c_s[2]+"1")llista_3.Add(cont);
						else llista_3_ex.Add(cont);
						break;
					}
					cont++;
				}
			}

				break;
		case 5:
			for (int i=0; i<3;i++)
			{
				int cont=0;
				foreach(Sprite sas in Sprite_base[i])
				{
					switch(i)
					{
					case 0:
						if(sas.name==car_sos_c_s[0])llista_1.Add(cont);
						else llista_1_ex.Add(cont);
						break;
					case 1:
						if(sas.name==car_sos_c_s[1])llista_2.Add(cont);
						else llista_2_ex.Add(cont);
						break;						
					case 2:
						if(sas.name==car_sos_c_s[2])llista_3.Add(cont);
						else llista_3_ex.Add(cont);
						break;
					}
					cont++;
				}
			}


				break;
		default:	
			Debug.Log("Error");
			    break;
		}


		ll_1=llista_1.Count;
		ll_2=llista_2.Count;
		ll_3=llista_3.Count;

		ll_1_ex=llista_1_ex.Count;
		ll_2_ex=llista_2_ex.Count;
		ll_3_ex=llista_3_ex.Count;

		l_2_1=llista_2_01.Count;
		l_2_2=llista_2_02.Count;
		l_2_3=llista_2_03.Count;

		num_caps = caps_spirites.Length;
		num_ulls = ulls_spirites.Length;
		num_boques = boques_spirites.Length;
		num_super = testa_spirites.Length;
		num_cos = cossos_spirites.Length;
		num_parts.Add(num_ulls);
		num_parts.Add(num_boques);
		num_parts.Add(num_super);
		num_parts.Add(num_cos);


		while(temp_num_sospe<25)
		{

		int[]new_sos=proces(temp_num_sospe);
		int phantom = 1;
		int temp_cap = Random.Range(0,num_caps);
		int temp_ull = new_sos[0];
		int temp_boca= new_sos[1];
		int temp_sup = new_sos[2];
		int temp_cos = new_sos[3];
		int culpable = 0;
		if(temp_num_sospe==0)culpable = 1;
		else culpable = 0;
		//Debug.Log(new_sos[0]+""+new_sos[1]+""+new_sos[2]+""+new_sos[3]);
		
			int temp_num_gen = int.Parse(phantom.ToString()+temp_cap.ToString() + temp_ull.ToString() + temp_boca.ToString() + temp_sup.ToString()+ temp_cos.ToString()+culpable.ToString());
		
			if(!array_total_sos.Contains(temp_num_gen))
			{
			array_total_sos[temp_num_sospe] = temp_num_gen;
			temp_num_sospe++;			   
			}
		
		}

		for (int t = 0; t < array_total_sos.Length; t++ )
			
		{
			
			int tmp = array_total_sos[t];
			
			int r = Random.Range(t, array_total_sos.Length);
			
			array_total_sos[t] = array_total_sos[r];
			
			array_total_sos[r] = tmp;
			
		}
		yield return null;
	}
	#endregion

	#region proces
	int[] proces(int temp_n_sos)
	{
		int canvi=0;

		List<int> copy_1 = new List<int>(llista_1);
		List<int> copy_2 = new List<int>(llista_2);
		List<int> copy_3 = new List<int>(llista_3);

		int l_copy_1=ll_1;
		int l_copy_2=ll_2;
		int l_copy_3=ll_3;

		int[]temp_sos_c=new int[4] {0, 0, 0,0};
		if(temp_n_sos>=1 && temp_n_sos<=6)
		{
		int temp = Random.Range(0,3);
		if(temp==0)
		    {
			copy_1 = new List<int>(llista_1_ex);
			l_copy_1 = ll_1_ex;
			}
		if(temp==1)
			{
			copy_2 = new List<int>(llista_2_ex);
			l_copy_2 = ll_2_ex;
			canvi+=2;
			} 
		if(temp==2)
			{
			copy_3 = new List<int>(llista_3_ex);
			l_copy_3 = ll_3_ex;
			canvi+=3;
			} 		
		}


		if (temp_n_sos > 6 && temp_n_sos <= 18) 
		{		
						int temp = Random.Range (0, 3);
						int new_te = temp;
						for (int i=1; i>=0; i--) 
			            {
								if (temp == 0) {
										copy_1 = new List<int> (llista_1_ex);
										l_copy_1 = ll_1_ex;
								}
								if (temp == 1) {
										copy_2 = new List<int> (llista_2_ex);
										l_copy_2 = ll_2_ex;
										canvi += 2;
								} 
								if (temp == 2) {
										copy_3 = new List<int> (llista_3_ex);
										l_copy_3 = ll_3_ex;
										canvi += 3;
								}
								do {
										temp = Random.Range (0, 3);
								} while(new_te==temp);

						}
		}

		if(temp_n_sos>18)
		{
			copy_1 = new List<int>(llista_1_ex);
			l_copy_1 = ll_1_ex;
			copy_2 = new List<int>(llista_2_ex);
			l_copy_2 = ll_2_ex;
			copy_3 = new List<int>(llista_3_ex);
			l_copy_3 = ll_3_ex;	
		}


		
	    switch(avis)
		{
		case 0:

			for(int soc=0; soc<=3;soc++)
			{

				if(carac_resta[soc]==8)temp_sos_c[soc]=Random.Range(0,num_parts[soc]);
			    else if(soc==3)
			    {				
				switch(canvi)
				{
						case 0:
						temp_sos_c[soc]=copy_2[Random.Range(0,l_copy_2)];
						break;
				        case 2:
						temp_sos_c[soc]=llista_2_02[Random.Range(0,l_2_2)];
					    break;
					     case 3:
						temp_sos_c[soc]=llista_2_03[Random.Range(0,l_2_3)];
					    break;
					     case 5:
						temp_sos_c[soc]=llista_2_01[Random.Range(0,l_2_1)];
					     break;	
					default:
						Debug.Log("Error!!");
						break;
				
				}				
			    }else{				
				temp_sos_c[soc]=copy_1[Random.Range(0,l_copy_1)];
			    }
			}
			break;
		case 3:
			bool patato=true;
			int conts=0;
			for(int soc=0; soc<4;soc++)
			{
				if(carac_resta[soc]==8 && patato )
				{
					temp_sos_c[soc]=Random.Range(0,num_parts[soc]);
					patato=false;
				}
				else{				
					switch(conts)
					{
					case 0:
						temp_sos_c[soc]=copy_1[Random.Range(0,l_copy_1)];
						conts++;
						break;
					case 1:
						temp_sos_c[soc]=copy_2[Random.Range(0,l_copy_2)];
						conts++;
						break;
					case 2:		
						temp_sos_c[soc]=copy_3[Random.Range(0,l_copy_3)];
						conts++;
						break;				
					default:
						Debug.Log("Error!!");
						break;
						
					}				
				}
			}
			break;
		case 2:
			bool patatis=true;
			int contis=0;
			for(int soc=0; soc<4;soc++)
			{
				if(carac_resta[soc]==8 && patatis )
				{
					temp_sos_c[soc]=Random.Range(0,num_parts[soc]);
					patatis=false;
				}
				else{				
					switch(contis)
					{
					case 0:
						temp_sos_c[soc]=copy_1[Random.Range(0,l_copy_1)];
						contis++;
						break;
					case 1:
						temp_sos_c[soc]=copy_2[Random.Range(0,l_copy_2)];
						contis++;
						break;
					case 2:		
						temp_sos_c[soc]=copy_3[Random.Range(0,l_copy_3)];
						contis++;
						break;				
					default:
						Debug.Log("Error!!");
						break;
						
					}				
				}
			}
			break;
		case 5:
			bool fars=true;
			int contos=0;
			for(int soc=0; soc<4;soc++)
			{
				if(carac_resta[soc]==8 && fars )
				{
					temp_sos_c[soc]=Random.Range(0,num_parts[soc]);
					fars=false;
				}
				else{				
					switch(contos)
					{
					case 0:
						temp_sos_c[soc]=copy_1[Random.Range(0,l_copy_1)];
						contos++;
						break;
					case 1:
						temp_sos_c[soc]=copy_2[Random.Range(0,l_copy_2)];
						contos++;
						break;
					case 2:		
						temp_sos_c[soc]=copy_3[Random.Range(0,l_copy_3)];
						contos++;
						break;				
					default:
						Debug.Log("Error!!");
						break;
						
					}				
				}
			}
			break;
		default:
			Debug.Log("falten!!");
			break;
		}
		return temp_sos_c;
	}
	#endregion

	#region Pistes
	IEnumerator Pistes()
	{


		car_totals_pos.Add("Con gafis");
		car_totals_pos.Add("Con barba");
		car_totals_pos.Add("Cabeza Cubierta");
		car_totals_pos.Add("Con guantes");
		car_totals_pos.Add("Pantalones largos");


		car_totals_neg.Add("Sin gafis");
		car_totals_neg.Add("Sin barba");
		car_totals_neg.Add("Cabeza descubierta");
		car_totals_neg.Add("Sin guantes");
		car_totals_neg.Add("Pantalones cortos");

		int cos=0;
		for(int i=0; i<=4;i++)
		{
			if(carac_resta[i]!=8)
			 {
				int temp_car=carac_resta[i];
				int temp_car_o=car_sos_c[cos];
				if(temp_car_o==1)
				{
					resultat_carac.Add(car_totals_pos[temp_car]);
					pistes[cos].text=(cos+1)+"."+car_totals_pos[temp_car];
				}
				else
				{
					resultat_carac.Add(car_totals_neg[temp_car]);
					pistes[cos].text=(cos+1)+"."+car_totals_neg[temp_car];
				}
				cos++;

			 }
		}

		pistes[3].text="Pistes";
		check_but.guiTexture.enabled = true;

		bool espera=true;
		while(espera)
		{
			if(Input.GetMouseButtonDown(0)&& check_but.HitTest(Input.mousePosition)){
			AudioSource.PlayClipAtPoint(sons[0], Camera.main.transform.position);
			espera=false;
			}
			yield return null;
		}

		Camera.main.cullingMask = 1 | 1<<10;
		check_but.guiTexture.enabled = false;
		yield return null;
	}
    #endregion

	#region Crear_sospitos 
	void Crear_sospitos(int fila_sos,bool last){

		GameObject sospi = Instantiate(p_base,or_sortida.position,Quaternion.identity) as GameObject;
		sospi.transform.parent = GameObject.Find("Sospitosos").transform;
		int sos_info = array_total_sos[fila_sos];

		int cap_n = (sos_info / 100000) % 10;
		int ull_n = (sos_info / 10000) % 10;
		int boca_n = (sos_info/ 1000) % 10;
		int super_n = (sos_info / 100) % 10;
		int cos_n = (sos_info / 10) % 10;
		int cul = sos_info % 10;

		sospi.name ="sospi_"+fila_sos;
		if(last)sospi.GetComponent<Sospe>().last_cre = true;
		Sospe script_sos =sospi.GetComponent<Sospe>();
		script_sos.sospi_num=fila_sos;
		script_sos.coss=cos_n;
		if(cul==0)script_sos.culpable=false;
		else script_sos.culpable=true;

		Transform cap_or = sospi.transform.GetChild(0);
		Transform ull_or = sospi.transform.GetChild(1);
		Transform boca_or = sospi.transform.GetChild(2);
		Transform super_or = sospi.transform.GetChild(3);
		Transform cos_or = sospi.transform.GetChild(4);
		
		cap_or.GetComponent<SpriteRenderer>().sprite = caps_spirites[cap_n];
		ull_or.GetComponent<SpriteRenderer>().sprite = ulls_spirites[ull_n];
		boca_or.GetComponent<SpriteRenderer>().sprite = boques_spirites[boca_n];
		super_or.GetComponent<SpriteRenderer>().sprite = testa_spirites[super_n];
		cos_or.GetComponent<SpriteRenderer>().sprite = cossos_spirites[cos_n];
	}
	#endregion

    #region Iniciar_joc
	IEnumerator Iniciar_joc()
	{		
		float temps=1f;
		while(fila_sos<4)
		{
			Crear_sospitos(fila_sos,false);
			fila_sos++;
			temps=temps-0.1f;   
			yield return new WaitForSeconds(temps);
		}

	}
   #endregion

	#region Base_joc
	IEnumerator Base_joc()
	{
		bool joc = true;
		bool molts_sos = true;
		bool una_veg = true;
		Component[] fills_Sospi;
		while(joc)
		{

			if (Input.GetMouseButtonDown(0) && dret.HitTest(Input.mousePosition) && gou==true)
			{
			AudioSource.PlayClipAtPoint(sons[1], Camera.main.transform.position);
			StartCoroutine(Refresh());
			//Debug.Log("dret");
			fills_Sospi=GetComponentsInChildren<Sospe>();
			foreach(Sospe fill_sospi in fills_Sospi) fill_sospi.Seguent(true,molts_sos);
			if(molts_sos)Crear_sospitos(fila_sos,false);			
			fila_sos++;			
			if(fila_sos>24)
				{
				    fila_sos=0;
				    while(array_total_sos[fila_sos]==0)fila_sos++;	
				}else
				{
					while(array_total_sos[fila_sos]==0)
					{						
						fila_sos++;
						if(fila_sos>24)fila_sos=0;
					}	
				} 
			//Debug.Log(fila_sos);
						
			}
			else if(Input.GetMouseButtonDown(0) && esquerre.HitTest(Input.mousePosition) && gou==true)
			{
			AudioSource.PlayClipAtPoint(sons[1], Camera.main.transform.position);
			StartCoroutine(Refresh());
			//Debug.Log("esquerre");			
			fills_Sospi=GetComponentsInChildren<Sospe>();
			foreach(Sospe fill_sospi in fills_Sospi) fill_sospi.Seguent(false,molts_sos);						
			if(molts_sos)Crear_sospitos(fila_sos,false);
			fila_sos++;	
			if(array_total_sos.Count(s => s != 0)<6)molts_sos=false;
			if(array_total_sos.Count(s => s != 0)==1)joc=false;
				if(fila_sos>24)
				{
					fila_sos=0;
					while(array_total_sos[fila_sos]==0)fila_sos++;	
				}else
				{
					while(array_total_sos[fila_sos]==0)
					{
						fila_sos++;
						if(fila_sos>24)fila_sos=0;

					}

				}
				if(!molts_sos && una_veg)
				{
					una_veg=false;
					Crear_sospitos(fila_sos,true);
					fila_sos++;
					if(fila_sos>24)fila_sos=0;
					while(array_total_sos[fila_sos]==0)
					{
						fila_sos++;
						if(fila_sos>24)fila_sos=0;
						
					}
				}
			}	

			yield return null;

			
		}


	}
	#endregion

	#region Refresh
	IEnumerator Refresh(){
		gou=false;
		yield return new WaitForSeconds(0.3f);
		gou=true;
	}
	#endregion

	#region Comp_en
	IEnumerator Comp_en(){
		GameObject sosps = gameObject.transform.GetChild(0).gameObject;
		bool culp = sosps.GetComponent<Sospe>().culpable;
		if(!culp)
		{
			text_fin.text="LOSE";
			AudioSource.PlayClipAtPoint(sons[2], Camera.main.transform.position);
		}else
		{
			text_fin.text="WIN";
			AudioSource.PlayClipAtPoint(sons[3], Camera.main.transform.position);
		}
		yield return null;
	}
	#endregion
}
