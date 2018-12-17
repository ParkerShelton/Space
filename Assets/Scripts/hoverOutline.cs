using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverOutline : MonoBehaviour {

  Renderer rend;
  // Shader normal;
  // Shader hover;
  public Material normal;
  public Material hover;

  // Start is called before the first frame update
  void Start() {
    rend = GetComponent<Renderer> ();
    // normal = Shader.Find("Diffuse");
    // hover = Shader.Find("Outlined/Silhouette Only");
  }

  void OnMouseOver() {
    // rend.material.shader = hover;
    rend.material = hover;
  }

  void OnMouseExit() {
    // rend.material.shader = normal;
    rend.material = normal;
  }
}
