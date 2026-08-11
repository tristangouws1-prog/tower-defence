using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public static TowerPlacer Instance;

    GameObject selectedPrefab;
    int selectedCost;
    bool isHero;

    Camera mainCam;

    [Header("Layer Masks")]
    public LayerMask terrainLayer;
    public LayerMask pathLayer;

    [Header("Preview")]
    public Material validMaterial;
    public Material invalidMaterial;
    GameObject previewObject;
    bool placementValid;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        mainCam = Camera.main;
    }

    void Update()
    {
        if (selectedPrefab == null) return;
        UpdatePreview();
        if (Input.GetMouseButtonDown(0) && placementValid) PlaceSelected();
        if (Input.GetMouseButtonDown(1)) CancelPlacement();
    }

    void UpdatePreview()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = isHero ? pathLayer : terrainLayer;
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, mask))
        {
            if (previewObject == null)
            {
                previewObject = Instantiate(selectedPrefab);
                DisableScripts(previewObject);
            }
            previewObject.transform.position = hit.point;
            placementValid = true;
            SetPreviewMaterial(validMaterial);
        }
        else
        {
            placementValid = false;
            if (previewObject != null) SetPreviewMaterial(invalidMaterial);
        }
    }

    void PlaceSelected()
    {
        if (!GameManager.Instance.SpendGold(selectedCost)) return;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = isHero ? pathLayer : terrainLayer;
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, mask))
        {
            Instantiate(selectedPrefab, hit.point, Quaternion.identity);
            CancelPlacement();
        }
    }

    public void SelectTower(GameObject prefab, int cost)
    {
        CancelPlacement();
        selectedPrefab = prefab;
        selectedCost = cost;
        isHero = prefab.GetComponent<Hero>() != null;
    }

    public void CancelPlacement()
    {
        selectedPrefab = null;
        if (previewObject != null) { Destroy(previewObject); previewObject = null; }
    }

    void SetPreviewMaterial(Material mat)
    {
        if (mat == null) return;
        foreach (Renderer r in previewObject.GetComponentsInChildren<Renderer>())
            r.material = mat;
    }

    void DisableScripts(GameObject obj)
    {
        foreach (MonoBehaviour mb in obj.GetComponentsInChildren<MonoBehaviour>())
            mb.enabled = false;
        foreach (Collider c in obj.GetComponentsInChildren<Collider>())
            c.enabled = false;
    }
}
