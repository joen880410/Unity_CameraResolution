using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    [RequireComponent(typeof(Camera))]
    public class CameraResolution : MonoBehaviour
    {
        private Camera TargetCamera;
        private bool isLandscape = true;
        [SerializeField]
        private bool enableHeight = true;
        [SerializeField]
        private bool enableWidth;
        private void Awake()
        {
            TargetCamera = GetComponent<Camera>();
            CheckResolution();
        }

        private void Update()
        {
            CheckResolution();
        }

        private void OnDestroy()
        {
            TargetCamera = null;
        }

        private void CheckResolution()
        {
            if (TargetCamera == null)
                return;
            CheckLandscape();
            CheckHeight();
            CheckWidth();
        }


        private void CheckHeight()
        {
            if (enableHeight)
            {
                float aspect = (float)Screen.width / Screen.height;
                float Aspect_Target;
                if (isLandscape) Aspect_Target = ResolutionUtility.Portrait_Aspect_Target;
                else Aspect_Target = ResolutionUtility.Landscape_Aspect_Target;
                if (aspect < Aspect_Target)
                {
                    SetHeightRect(aspect, Aspect_Target);
                }
            }
        }

        private void CheckWidth()
        {
            if (enableWidth)
            {
                float aspect = (float)Screen.height / Screen.width;
                float Aspect_Target;
                if (isLandscape) Aspect_Target = ResolutionUtility.Landscape_Aspect_Target;
                else Aspect_Target = ResolutionUtility.Portrait_Aspect_Target;
                if (aspect < Aspect_Target)
                {
                    SetWidthRect(aspect, Aspect_Target);
                }
            }
        }

        private void SetHeightRect(float aspect, float Aspect_Target)
        {
            if (aspect < Aspect_Target)
            {
                var newRect = new Rect(TargetCamera.rect);
                var newHeight = Screen.width / Aspect_Target;
                newRect.height = newHeight / Screen.height;
                newRect.y = (1 - newRect.height) * 0.5f;
                newRect.width = ResolutionUtility.DefaultRect.width;
                newRect.x = ResolutionUtility.DefaultRect.x;
                TargetCamera.rect = newRect;
            }
            else
            {
                TargetCamera.rect = ResolutionUtility.DefaultRect;
            }
        }

        private void SetWidthRect(float aspect, float Aspect_Target)
        {
            if (aspect < Aspect_Target)
            {
                var newRect = new Rect(TargetCamera.rect);
                var newWidth = Screen.height / Aspect_Target;
                newRect.width = newWidth / Screen.width;
                newRect.x = (1 - newRect.width) * 0.5f;
                newRect.height = ResolutionUtility.DefaultRect.height;
                newRect.y = ResolutionUtility.DefaultRect.y;
                TargetCamera.rect = newRect;
            }
            else
            {
                TargetCamera.rect = ResolutionUtility.DefaultRect;
            }
        }

        /// <summary>
        /// 切換螢幕方向
        /// </summary>
        public void SwitchScreenOrientation(bool isLandscape = true)
        {
            this.isLandscape = isLandscape;
        }

        public void CheckLandscape()
        {
            this.isLandscape = Screen.height > Screen.width;
        }
    }
}
