using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가
using UnityEngine.UI; // UI 버튼을 사용하기 위해 추가
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering; // Post-Processing 활성화를 위해 추가

namespace HoSik
{
    public class ObjectTagToUI : MonoBehaviour
    {
        [Header("XR Controller")]
        public XRBaseController controller; // XR 컨트롤러 (오른손/왼손)

        [Header("Ray Settings")]
        public float rayLength = 10f; // Raycast의 거리
        public LayerMask interactableLayer; // 감지할 레이어

        [Header("UI Elements")]
        public GameObject startPanel; // Start 패널 UI
        public Button startButton; // Start 버튼
        public TextMeshProUGUI mainGuideText; // 주요 가이드 텍스트
        public TextMeshProUGUI secondaryGuideText; // 보조 가이드 텍스트
        public GameObject remoteUI; // 리모컨 UI
        public GameObject keyboardUI; // 키보드 UI
        public BrailleButtonInput brailleButtonInput; // Braille 입력 관리 스크립트

        [Header("Post Processing")]
        public Volume postProcessingVolume; // Post-Processing Volume

        private GameObject activeUI = null; // 현재 활성화된 UI 참조

        private void Start()
        {
            // Start 버튼에 이벤트 연결
            startButton.onClick.AddListener(OnStartButtonClicked);

            // BrailleButtonInput 이벤트 등록
            brailleButtonInput.OnValidBusNumberEntered += OnCorrectBusNumberEntered;

            // 초기 설정: Start 패널 활성화, 나머지 UI 비활성화
            startPanel.SetActive(true);
            postProcessingVolume.enabled = false; // Post-Processing 비활성화
            mainGuideText.gameObject.SetActive(false); // 가이드 텍스트 비활성화
            secondaryGuideText.gameObject.SetActive(false); // 보조 가이드 텍스트 비활성화
            DeactivateAllUI();
        }

        private void Update()
        {
            // Start 패널이 활성화된 동안에는 Update를 실행하지 않음
            if (startPanel.activeSelf) return;

            // ESC를 누르면 모든 UI를 비활성화
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeactivateAllUI();
                return;
            }

            // Ray를 발사하여 오브젝트 감지
            Ray ray = new Ray(controller.transform.position, controller.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, interactableLayer))
            {
                string hitTag = hit.collider.gameObject.tag;

                if (hitTag == "Remote Controller")
                {
                    ActivateUI(remoteUI, "Remote Controller");
                }
                else if (hitTag == "Keyboard")
                {
                    ActivateUI(keyboardUI, "Keyboard");
                    UpdateSecondaryGuide("Enter the correct bus number.");
                }
                else if (hitTag == "Door")
                {
                    LoadNextScene(); // Door 태그 감지 시 씬 전환
                }
                else
                {
                    UpdateMainGuide($"Looking at: {hitTag}");
                }
            }
            else
            {
                UpdateMainGuide("Point the controller at an object.");
            }
        }

        // Start 버튼 클릭 시 호출
        public void OnStartButtonClicked()
        {
            startPanel.SetActive(false); // Start 패널 비활성화
            postProcessingVolume.enabled = true; // Post-Processing 활성화
            mainGuideText.gameObject.SetActive(true); // Main Guide Text 활성화
            secondaryGuideText.gameObject.SetActive(true); // Secondary Guide Text 활성화
            UpdateSecondaryGuide("Find Keyboard to search bus stop"); // 초기 가이드 설정
        }

        // 특정 UI 활성화
        private void ActivateUI(GameObject uiElement, string objectName)
        {
            if (activeUI == uiElement) return; // 이미 활성화된 경우 무시

            DeactivateAllUI(); // 다른 UI가 활성화되어 있다면 비활성화
            activeUI = uiElement;
            activeUI.SetActive(true);
            UpdateMainGuide($"Activated: {objectName}"); // GuideText는 계속 표시
        }

        // 모든 UI 비활성화
        private void DeactivateAllUI()
        {
            if (activeUI != null) activeUI.SetActive(false);
            activeUI = null;
            UpdateMainGuide("Point the controller at an object."); // 기본 GuideText
        }

        // Main Guide Text 업데이트
        private void UpdateMainGuide(string text)
        {
            mainGuideText.text = text;
        }

        // Secondary Guide Text 업데이트
        private void UpdateSecondaryGuide(string text)
        {
            secondaryGuideText.text = text;
        }

        // 올바른 버스 번호 입력 시 호출
        private void OnCorrectBusNumberEntered()
        {
            UpdateSecondaryGuide("Find Door to go out");
        }

        // 씬 전환 함수
        private void LoadNextScene()
        {
            //string nextSceneName = "RoadScene_0_"; // 전환할 씬 이름
            //SceneManager.LoadScene(nextSceneName);
        }
    }
}
