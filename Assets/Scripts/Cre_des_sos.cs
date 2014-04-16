using UnityEngine;
using System.Collections;
using System.Linq;

public class Cre_des_sos : MonoBehaviour {

	public GameObject p_base;
	public Transform or_sortida;

	//sprites sospitosos
	public Sprite[] caps_spirites;
	public Sprite[] ulls_spirites;
	public Sprite[] boques_spirites;
	public Sprite[] supers_spirites;
	public Sprite[] cossos_spirites;

	// sospitosos actuals
	public int[] li_posis = new int[4] {0, 0, 0, 0};
	// tots sospitosos
	[HideInInspector]
	public int[] array_total_sos = new int[25];
	int fila_sos = 0;
	//culpable
	int culpable;
	int cul_cap;
	int cul_ull;
	int cul_boca;
	int cul_super;
	int cul_cos;
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

	void Start () 
	{	
	//GameObject sospi = Instantiate(p_base,transform.position,Quaternion.identity) as GameObject;
	//sospi.transform.parent = GameObject.Find("Sospitosos").transform;
	Screen.orientation = ScreenOrientation.LandscapeLeft;
	culpable = Random.Range(0,25);
	StartCoroutine(Proces_joc());	
	}

	IEnumerator Proces_joc(){
	yield return StartCoroutine(Generar_lista_sospe());	
	yield return StartCoroutine(Iniciar_joc());
    yield return StartCoroutine(Base_joc());
	Debug.Log("fi");
	}


	IEnumerator Generar_lista_sospe()
	{
		num_caps = caps_spirites.Length;
		num_ulls = ulls_spirites.Length;
		num_boques = boques_spirites.Length;
		num_super = supers_spirites.Length;
		num_cos = cossos_spirites.Length; 
		int temp_num_sospe= 0;
		while(temp_num_sospe<25)
		{		 
			int phantom = 1;
			int temp_cap = Random.Range(0,num_caps);
		    int temp_ull = Random.Range(0,num_ulls);
			int temp_boca = Random.Range(0,num_boques);
			int temp_sup = Random.Range(0,num_super);
			int temp_cos = Random.Range(0,num_cos);
			if(temp_num_sospe==culpable)
			{
				cul_cap = temp_cap;
				cul_ull = temp_ull;
				cul_boca = temp_boca;
				cul_super = temp_sup;
				cul_cos  = temp_cos;
			}
			int temp_num_gen = int.Parse(phantom.ToString()+temp_cap.ToString() + temp_ull.ToString() + temp_boca.ToString() + temp_sup.ToString()+ temp_cos.ToString());
			if(!array_total_sos.Contains(temp_num_gen))
			{
			   array_total_sos[temp_num_sospe] = temp_num_gen;
			   temp_num_sospe++;			   
			}
//			Debug.Log(temp_num_gen);
			yield return null;
		}

	}

	void Crear_sospitos(int fila_sos){

		GameObject sospi = Instantiate(p_base,or_sortida.position,Quaternion.identity) as GameObject;
		sospi.transform.parent = GameObject.Find("Sospitosos").transform;
		sospi.name ="sospi_"+fila_sos;
		Sospe script_sos =sospi.GetComponent<Sospe>();
		script_sos.sospi_num=fila_sos;
		int sos_info = array_total_sos[fila_sos];

		int cap_n = (sos_info / 10000) % 10;
		int ull_n = (sos_info / 1000) % 10;
		int boca_n = (sos_info/ 100) % 10;
		int super_n = (sos_info / 10) % 10;
		int cos_n = sos_info % 10;

		Transform cap_or = sospi.transform.GetChild(0);
		Transform ull_or = sospi.transform.GetChild(1);
		Transform boca_or = sospi.transform.GetChild(2);
		Transform super_or = sospi.transform.GetChild(3);
		Transform cos_or = sospi.transform.GetChild(4);
		
		cap_or.GetComponent<SpriteRenderer>().sprite = caps_spirites[cap_n];
		ull_or.GetComponent<SpriteRenderer>().sprite = ulls_spirites[ull_n];
		boca_or.GetComponent<SpriteRenderer>().sprite = boques_spirites[boca_n];
		super_or.GetComponent<SpriteRenderer>().sprite = supers_spirites[super_n];
		cos_or.GetComponent<SpriteRenderer>().sprite = cossos_spirites[cos_n];
	}

	IEnumerator Iniciar_joc()	{		
		float temps=1f;
		while(fila_sos<4)
		{
			Crear_sospitos(fila_sos);
			fila_sos++;
			temps=temps-0.1f;   
			yield return new WaitForSeconds(temps);
		}

	}

	IEnumerator Base_joc()
	{
		Component[] fills_Sospi;
		while(array_total_sos.Count(s => s != 0)>4)
		{

			if (Input.GetMouseButtonDown(0) && dret.HitTest(Input.mousePosition) && gou==true)
			{
			StartCoroutine(Refresh());
			//Debug.Log("dret");
			fills_Sospi=GetComponentsInChildren<Sospe>();
			foreach(Sospe fill_sospi in fills_Sospi) fill_sospi.Seguent(true);
			Crear_sospitos(fila_sos);			
			fila_sos++;			
			if(fila_sos>24)
				{
				    fila_sos=0;
				    while(array_total_sos[fila_sos]==0)fila_sos++;	
				}else{
					while(array_total_sos[fila_sos]==0){						
						fila_sos++;
						if(fila_sos>24)fila_sos=0;
					}	
				} 
			//Debug.Log(fila_sos);
						
			}
			else if(Input.GetMouseButtonDown(0) && esquerre.HitTest(Input.mousePosition) && gou==true)
			{
			StartCoroutine(Refresh());
			//Debug.Log("esquerre");
			fills_Sospi=GetComponentsInChildren<Sospe>();
			foreach(Sospe fill_sospi in fills_Sospi) fill_sospi.Seguent(false);
			Crear_sospitos(fila_sos);
			fila_sos++;				
				if(fila_sos>24)
				{
					fila_sos=0;
					while(array_total_sos[fila_sos]==0)fila_sos++;	
				}else{
					while(array_total_sos[fila_sos]==0){
						fila_sos++;
						if(fila_sos>24)fila_sos=0;
					}	
				} 
			}	

			yield return null;

			
		}


	}

	IEnumerator Refresh(){
		gou=false;
		yield return new WaitForSeconds(0.3f);
		gou=true;
	}
	
}
