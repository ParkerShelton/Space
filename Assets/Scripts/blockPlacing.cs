using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockPlacing : MonoBehaviour {

  static blockPlacing obj;

  public bool unlimitedBlocks = false;
  public GameObject blockPrefab;

  private bool lockCursor = true;
  private int blocksLeft = 0;
  private float range = 7f;

  GUIStyle style = new GUIStyle();
  Texture2D crosshair;

  int layerMask = 1 << 8;

  // Start is called before the first frame update
  void Start() {
    blockPlacing.obj = this;
    layerMask = ~layerMask;

    crosshair = new Texture2D(128, 128);
  }

  // Update is called once per frame
  void Update() {
    if (lockCursor) {
      Cursor.lockState = CursorLockMode.Locked;
    }

    if (blocksLeft > 0 || unlimitedBlocks) {

      // Place block
      if (Input.GetMouseButtonDown(1)) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, layerMask)) {

          GameObject block = Instantiate(blockPrefab, hit.normal + hit.transform.position, hit.transform.rotation);

          if (!unlimitedBlocks) {
            blocksLeft--;
          }

          if (block.GetComponent<Collider>().bounds.Intersects(this.transform.parent.GetComponent<Collider>().bounds)) {
            GameObject.Destroy(block);
            if (!unlimitedBlocks) {
              blocksLeft++;
            }
          }
        }
      }

      // Destroy block
      if (Input.GetMouseButtonDown(0)) {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, layerMask)) {
          Destroy(hit.collider.gameObject);

          if(!unlimitedBlocks) {
            blocksLeft++;
          }
        }
      }


    }
  }

  void OnGUI() {

    for (int y = 0; y < crosshair.height; ++y) {
      for (int x = 0; x < crosshair.width; ++x) {
        Color color = Color.white;
        crosshair.SetPixel(x, y, color);
      }
    }
    crosshair.Apply();


    //This is the crosshair
    style.normal.background = crosshair;
    GUI.Box(new Rect(Screen.width / 2 - 5, Screen.height / 2 - 5, 5, 5), "", style);
  }

  //Static adding of blocks. This isn't needed but definately helps a lot
  public static void addBlocks(int i = 1) {
    blockPlacing.obj.blocksLeft += i;
  }
}
