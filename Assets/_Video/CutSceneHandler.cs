using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using FMODUnity;

namespace BTA
{
    public class CutSceneHandler : MonoBehaviour
    {
        [SerializeField] bool MouseActive = false;
        [SerializeField] GameManager m_gameMgr;
        InputManager m_inputMgr;

        [SerializeField] GameObject m_mainCanvas;
        [SerializeField] GameObject m_skipCanvas;

        [SerializeField] StudioEventEmitter m_sound;

        VideoPlayer m_videoPlayer;

        private void Awake()
        {
            Cursor.visible = MouseActive;

            m_gameMgr.CanLaunchGame = false;

            m_videoPlayer = GetComponent<VideoPlayer>();

            m_videoPlayer.started += OnVideoStart;
            m_videoPlayer.loopPointReached += OnVideoEnd;
        }

        private void Start()
        {
            m_inputMgr = m_gameMgr.GetInstanceOf<InputManager>();
            if (m_inputMgr.init)
                InitInput();
            else
            m_inputMgr.OnInitComplete.AddListener(InitInput);
        }

        public void InitInput()
        {
            m_inputMgr.GetEvent(GamePadID.Controller1, GamePadInput.ButtonStart).AddListener(OnVideoSkip);
            m_inputMgr.GetEvent(GamePadID.Controller2, GamePadInput.ButtonStart).AddListener(OnVideoSkip);
        }

        private void Update()
        {
            if (Input.anyKeyDown && !m_skipCanvas.activeSelf && !m_mainCanvas.activeSelf)
                m_skipCanvas.SetActive(true);
        }

        private void OnVideoStart(VideoPlayer src)
        {
            m_sound.Play();
        }

        private void OnVideoSkip()
        {
            m_videoPlayer.Stop();
            OnVideoEnd(null);
        }

        private void OnVideoEnd(VideoPlayer src)
        {
            m_inputMgr.GetEvent(GamePadID.Controller1, GamePadInput.ButtonStart).RemoveListener(OnVideoSkip);
            m_inputMgr.GetEvent(GamePadID.Controller2, GamePadInput.ButtonStart).RemoveListener(OnVideoSkip);

            m_skipCanvas.SetActive(false);
            m_mainCanvas.SetActive(true);
            StartCoroutine(EnableGameManagerWithDelay(1f));
        }

        IEnumerator EnableGameManagerWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            m_gameMgr.CanLaunchGame = true;
        }
    }
}
