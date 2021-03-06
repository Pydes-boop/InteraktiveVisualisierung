using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardClick : MonoBehaviour
{

    GameObject lastClicked = null;

    public float rayDistance;
    public float targetOverBordDistance = 0.05f;

    private GameObject Player;

    private Camera cam;

    public Pair[] pairsDraggables;
    public Pair[] pairsDropOff;
    private GameObject[] draggables;

    public delegate void Accept(bool correctAnswers);
    public event Accept OnAccept;

    private Image image;
    private Text textBox;
    private Text textHead;

    private FadeIn fadein;

    private void Awake()
    {
        this.Player = GameObject.Find("FirstPersonPlayerTime");
        this.cam = this.Player.GetComponentInChildren<Camera>();
        this.fadein = this.Player.GetComponentInChildren<FadeIn>();

        this.image = this.transform.Find("Parant").Find("Canvas").Find("Image").GetComponent<Image>();
        this.textBox = this.transform.Find("Parant").Find("Canvas").Find("Text").GetComponent<Text>();
        this.textHead = this.transform.Find("Parant").Find("Canvas").Find("Header").GetComponent<Text>();

        this.draggables = GameObject.FindGameObjectsWithTag("Dragable");
    }

    void Update()
    {
       

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            lastClicked = getClickedObject("Dragable");
            if (lastClicked != null)
            {
                BoardItem script = lastClicked.GetComponent<BoardItem>();
                if (script.getDropOff() != null) script.getDropOff().SetActive(true);
                script.setDropOff(null);

                this.image.sprite = lastClicked.GetComponent<BoardItemInfo>().image;
                this.textBox.text = lastClicked.GetComponent<BoardItemInfo>().Text;
                this.textHead.text = lastClicked.GetComponent<BoardItemInfo>().Header;
                this.image.color = new Color(1, 1, 1, 1);
            }

            GameObject accepted = getClickedObject("Accept");
            if (accepted != null)
            {
                bool correctness = checkCorrectness();
                OnAccept?.Invoke(correctness);
                if (correctness)
                {
                    image.sprite = null;
                    textBox.text = "";
                    textHead.text = "";
                    image.color = new Color(0, 1, 0, 1);
                    fadein.OnFadeDone += SceneTransition;
                    fadein.setDirection(-1);
                    fadein.setAlpha(0f);
                    fadein.setTime(1f);
                    fadein.StartFade();
                }
                else
                {
                    image.sprite = null;
                    image.color = new Color(1, 0, 0, 1);
                    textBox.text = "";
                    textHead.text = "";
                }
            }

        }

        if (Mouse.current.leftButton.isPressed && lastClicked != null)
        {
            Vector3 pointTo = getImpectPoint();
            if (pointTo.x != -1)
            {
                lastClicked.transform.position = pointTo;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (lastClicked != null)
            {
                GameObject dropOff = getDropOff();
                BoardItem script = lastClicked.GetComponent<BoardItem>();
                if (script.getDropOff() != null) return;
                script.setDropOff(dropOff);

                if (dropOff != null)
                {
                    script.getDropOff().SetActive(false);
                    lastClicked.transform.position = dropOff.transform.position;
                }

            }

            lastClicked = null;
        }
    }

    public GameObject getClickedObject(string Tag)
    {
        if (cam == null) return null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Debug.DrawLine(ray.origin, hit.point, new Color(1, 0, 0), 1);
            return (hit.transform.gameObject.tag.Equals(Tag) ? hit.transform.gameObject : null);
        }
        else
        {
            return null;
        }
    }

    public Vector3 getImpectPoint()
    {
        if (cam == null) return new Vector3(-1, -1, -1);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, rayDistance);
        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * rayDistance), new Color(0, 1, 0), 1);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag.Equals("InfoBord"))
            {
                
                return hits[i].point + (ray.direction.normalized * -targetOverBordDistance);
            }

        }
        return new Vector3(-1, -1, -1);
    }

    public GameObject getDropOff()
    {
        if (cam == null) return null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, rayDistance);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag.Equals("DropOff"))
            {
                return hits[i].transform.gameObject;
            }

        }
        return null;
    }

    public bool checkCorrectness()
    {
        foreach (Pair architects in this.pairsDraggables)
        {
            GameObject off0 = architects.one.GetComponent<BoardItem>().getDropOff();
            GameObject off1 = architects.two.GetComponent<BoardItem>().getDropOff();
            if (off0 == null || off1 == null) return false;

            bool pairing = false;

            foreach (Pair dropoff in this.pairsDropOff)
            {
                if (dropoff.correctPair(off0, off1)) pairing = true;
            }

            if (!pairing) return false;
        }

        return true;
    }

    public void SceneTransition()
    {
        SceneManager.LoadScene("EndCard", LoadSceneMode.Single);
    }

}
