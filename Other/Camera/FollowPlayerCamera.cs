using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FollowPlayerCamera : MonoBehaviour
{
    // 카메라의 추적 속도
    [SerializeField] private float _CameraFollowSpeed = 10.0f;

    // 이동할 목표 위치를 저장할 변수
    private Vector3 _TargetCameraPosition = Vector3.zero;

    // 플레이어를 나타냅니다.
    public Player player { get; private set; } = null;

    // 현재 플레이어가 위치한 영역을 나타냅니다.
    public CameraArea cameraArea { get; set; } = null;

    // 카메라 크기를 나타내는 콜라이더에 대한 프로퍼티입니다.
    public BoxCollider2D cameraBound { get; private set; } = null;

    private void Awake()
    {
        GameSettingManager.Instance.followPlayerCamera = this;
        cameraBound = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {

        player = PlayerManager.Instance.player;
        _TargetCameraPosition = transform.position;
    }

    private void Update()
    {
        FollowPlayer();
    }

    // 플레이어를 따라가는 메서드
    private void FollowPlayer()
    {
        // 목표 위치를 변경하는 내부 함수입니다.
        void FixTargetPosition()
        {
            _TargetCameraPosition.x = player.transform.position.x;
            _TargetCameraPosition.y = player.transform.position.y;

            _TargetCameraPosition.Set(
                (cameraArea.cameraMoveableX) ?
                    Mathf.Clamp(_TargetCameraPosition.x,
                        cameraArea.cameraBounds.min.x,
                        cameraArea.cameraBounds.max.x) :
                        cameraArea.cameraBounds.center.x,

                (cameraArea.cameraMoveableY) ?
                    Mathf.Clamp(_TargetCameraPosition.y,
                        cameraArea.cameraBounds.min.y,
                        cameraArea.cameraBounds.max.y) :
                        cameraArea.cameraBounds.center.y,

                _TargetCameraPosition.z);
        }

        // 플레이어가 속한 영역이 존재하지 않는다면 실행하지 않습니다.
        if (cameraArea == null) return;

        FixTargetPosition();

        if (cameraArea.cameraMoveable)
            transform.position = Vector3.Lerp(
            transform.position,
            _TargetCameraPosition,
            _CameraFollowSpeed * Time.deltaTime);
        else transform.position = _TargetCameraPosition;

    }
}
