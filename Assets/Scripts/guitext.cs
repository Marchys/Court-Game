using UnityEngine;
using System.Collections;

public class guitext : MonoBehaviour {

		
		public Vector2 offset;
		
		
		
		public float ratio = 10;		
		
			
		void OnGUI(){			
			
			
			float finalSize = (float)Screen.width/ratio;
			
			guiText.fontSize = (int)finalSize;
			
			guiText.pixelOffset = new Vector2( offset.x * Screen.width, offset.y * Screen.height);
			
		}
		
		
		

}
