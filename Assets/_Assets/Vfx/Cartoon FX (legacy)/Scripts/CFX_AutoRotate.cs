using UnityEngine;

// Cartoon FX  - (c) 2015 Jean Moreno

// Indefinitely rotates an object at a constant speed

namespace _Assets.Vfx.Cartoon_FX__legacy_.Scripts
{
	public class CFX_AutoRotate : MonoBehaviour
	{
		// Rotation speed & axis
		public Vector3 rotation;
	
		// Rotation space
		public Space space = Space.Self;
	
		void Update()
		{
			this.transform.Rotate(rotation * Time.deltaTime, space);
		}
	}
}
