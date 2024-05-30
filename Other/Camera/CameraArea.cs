using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 이동 가능 영역을 나타내는 컴포넌트입니다.
[RequireComponent(typeof(BoxCollider2D), typeof(DrawBoxCollider))]
public class CameraArea : MonoBehaviour
{
    // 카메라가 이동 가능한 영역을 저장할 변수
    private Bounds _CameraBounds;

    // 카메라가 x 축으로 이동할 수 있는지를 나타내는 변수
    [SerializeField] private bool _CameraMoveableX = false;

    // 카메라가 y 축으로 이동할 수 있는지를 나타내는 변수
    [SerializeField] private bool _CameraMoveableY = false;

    // 카메라가 따라가는지 확인하는 변수
    [SerializeField] private bool CameraMoveable = false;


    // 카메라 영역을 나타내는 콜라이더를 참조할 변수
    public BoxCollider2D cameraAreaCollider { get; private set; } = null;


    public Bounds cameraBounds => _CameraBounds;
    public bool cameraMoveableX => _CameraMoveableX;
    public bool cameraMoveableY => _CameraMoveableY;
    public bool cameraMoveable => CameraMoveable;

    private void Awake()
    {
        cameraAreaCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        // 카메라가 이동 가능한 영역을 계산합니다.
        void CalculateCameraBounds()
        {
            Vector3 cameraHalfSize = GameSettingManager.Instance.followPlayerCamera.
                cameraBound.bounds.extents;

            _CameraBounds.SetMinMax(
                cameraAreaCollider.bounds.min + cameraHalfSize,
                cameraAreaCollider.bounds.max - cameraHalfSize);

            // 카메라 영역보다 이동 가능 영역이 좁은 경우 X 나 Y 축 이동을 막습니다.
            _CameraMoveableX = _CameraBounds.min.x < _CameraBounds.max.x;
            _CameraMoveableY = _CameraBounds.min.y < _CameraBounds.max.y;            
        }

        // 트리거로 설정합니다.
        cameraAreaCollider.isTrigger = true;

        // 카메라 이동 가능 영역을 계산합니다.
        CalculateCameraBounds();
    }

    private void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameSettingManager.Instance.followPlayerCamera.cameraArea != this)
                GameSettingManager.Instance.followPlayerCamera.cameraArea = this;
        }
    }
}
