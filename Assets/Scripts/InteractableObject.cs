using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public bool isSelected;

	//When the mouse hovers over the GameObject, it turns to this color (red)
	Color m_MouseOverColor = Color.red;

	//This stores the GameObject’s original color
	Color m_OriginalColor;

	//Get the GameObject’s mesh renderer to access the GameObject’s material and color
	MeshRenderer m_Renderer;

	void Start()
	{
		//Fetch the mesh renderer component from the GameObject
		m_Renderer = GetComponentInChildren<MeshRenderer>();
		//Fetch the original color of the GameObject
		if(m_Renderer)
			m_OriginalColor = m_Renderer.material.color;
	}

	void OnMouseOver()
	{
		// Change the color of the GameObject to red when the mouse is over GameObject
		if(m_Renderer)
			m_Renderer.material.color = m_MouseOverColor;
		isSelected = true;
	}

	void OnMouseExit()
	{
		// Reset the color of the GameObject back to normal
		if(m_Renderer)
			m_Renderer.material.color = m_OriginalColor;
		isSelected = false;
	}
}