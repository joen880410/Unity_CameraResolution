using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    public class CanvasScalerResolution : CanvasScaler
    {
        private bool isLandscape = true;

        protected override void Awake()
        {
            base.Awake();
            CheckResolution();
        }

        private void Update()
        {
            SwitchScreenOrientation();
            CheckResolution();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void CheckResolution()
        {
            /// 根據傳入的螢幕方向，對CanvasResolution進行切換
            Vector2 canvasResolution = new Vector2();

            float TargetAspect_Target = 0f;

            if (this.isLandscape)
            {
                canvasResolution = ResolutionUtility.LandscapeCanvasResolution;
                TargetAspect_Target = ResolutionUtility.Landscape_Aspect_Target;
            }
            else
            {
                canvasResolution = ResolutionUtility.PortraitCanvasResolution;
                TargetAspect_Target = ResolutionUtility.Portrait_Aspect_Target;
            }

            referenceResolution = canvasResolution;

#if !UNITY_2020_3_20
            if (TargetCanvasScaler == null)
                return;

            float aspect = (float)Screen.width / Screen.height;

            if (aspect < TargetAspect_Target)
            {
                var newCanvasResolution = new Vector2(TargetCanvasScaler.referenceResolution.x, TargetCanvasScaler.referenceResolution.y);
                newCanvasResolution.y = newCanvasResolution.x / aspect;
                TargetCanvasScaler.referenceResolution = newCanvasResolution;
            }
            else
            {
                TargetCanvasScaler.referenceResolution = canvasResolution;
            }
#endif

        }

        /// <summary>
        /// 切換螢幕方向
        /// </summary>
        public void SwitchScreenOrientation(bool isLandscape = true)
        {
            this.isLandscape = Screen.height < Screen.width; ;
        }
    }
}