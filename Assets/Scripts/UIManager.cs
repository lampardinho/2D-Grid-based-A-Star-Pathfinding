using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _startPathfindingButton;
    [SerializeField] private AppManager _manager;

	private void Start ()
    {
        SetupCamera();

        _startPathfindingButton.onClick.AddListener(() =>
		{
		    _manager.StartPathfinding();
		});
    }

    private void SetupCamera()
    {
        var cam = Camera.main;
        var gridWidth = _manager.Grid.Width;
        var gridHeight = _manager.Grid.Height;
        cam.transform.position = new Vector3(gridWidth / 2, gridHeight / 2, -10);
        cam.orthographicSize = Mathf.Max(gridWidth, gridHeight) / 2 + 1;
    }
}
