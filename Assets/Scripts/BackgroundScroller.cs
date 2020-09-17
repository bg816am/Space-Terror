using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed = 0.5f;

    private Material _myMaterial;

    private Vector2 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _myMaterial = GetComponent<Renderer>().material;
        _offset = new Vector2(0, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _myMaterial.mainTextureOffset += _offset * Time.deltaTime;
    }
}
