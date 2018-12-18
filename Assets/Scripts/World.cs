using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

  public static World currentWorld;

  public int chunkWidth = 20, chunkHeight = 20, seed = 0;

  // Start is called before the first frame update
  void Awake() {
    currentWorld = this;

    if(seed == 0) {
      seed = Random.Range(0, int.MaxValue);
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
