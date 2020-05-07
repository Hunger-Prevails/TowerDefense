using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class CamControl : MonoBehaviour
{
    public float panSpeed;

    public float panMargin;

    public float scrollSpeed;

    public float xMin;

    public float xMax;

    public float yMin;

    public float yMax;

    public float zMin;

    public float zMax;

    public GameObject panelGamePause;

    public SceneFader fader;

    bool camFreeze;

    bool onPause;

    public void freeze()
    {
        camFreeze = true;
    }

    public void toggle()
    {
        if (onPause) {

            camFreeze = false;

            panelGamePause.SetActive(false);

            Time.timeScale = 1;

            onPause = false;

        } else {

            camFreeze = true;

            panelGamePause.SetActive(true);

            Time.timeScale = 0;

            onPause = true;
        }
    }

    public void retry()
    {
        toggle();

        fader.Reload();
    }

    public void menu()
    {
        toggle();

        fader.Transit(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) toggle();
        
        if (camFreeze) return;

        if (Input.GetKey("w")) {

        	transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s")) {

        	transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a")) {

        	transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d")) {

        	transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("z")) {

        	transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("x")) {

        	transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime, Space.World);
        }
        float clamp_x = Mathf.Clamp(transform.position.x, xMin, xMax);
        
        float clamp_y = Mathf.Clamp(transform.position.y, yMin, yMax);
        
        float clamp_z = Mathf.Clamp(transform.position.z, zMin, zMax);

        transform.position = new Vector3(clamp_x, clamp_y, clamp_z);
    }
}
