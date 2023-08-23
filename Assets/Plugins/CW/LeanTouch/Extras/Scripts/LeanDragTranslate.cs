using UnityEngine;
using CW.Common;

namespace Lean.Touch
{
    /// <summary>This component allows you to translate the current GameObject relative to the camera using the finger drag gesture.</summary>
    [HelpURL(LeanTouch.HelpUrlPrefix + "LeanDragTranslate")]
    [AddComponentMenu(LeanTouch.ComponentPathPrefix + "Drag Translate")]
    public class LeanDragTranslate : MonoBehaviour
    {
        public LeanFingerFilter Use = new LeanFingerFilter(true);

        public Camera Camera { set { _camera = value; } get { return _camera; } }
        [SerializeField] private Camera _camera;

        public float Sensitivity { set { sensitivity = value; } get { return sensitivity; } }
        [SerializeField] private float sensitivity = 1.0f;

        public float Damping { set { damping = value; } get { return damping; } }
        [SerializeField] protected float damping = -1.0f;

        public float Inertia { set { inertia = value; } get { return inertia; } }
        [SerializeField][Range(0.0f, 1.0f)] private float inertia;

        public float maxX = Mathf.Infinity;
        public float maxY = Mathf.Infinity;
        public float minX = -Mathf.Infinity;
        public float minY = -Mathf.Infinity;

        [SerializeField]
        private Vector3 remainingTranslation;

        public void AddFinger(LeanFinger finger)
        {
            Use.AddFinger(finger);
        }

        public void RemoveFinger(LeanFinger finger)
        {
            Use.RemoveFinger(finger);
        }

        public void RemoveAllFingers()
        {
            Use.RemoveAllFingers();
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            Use.UpdateRequiredSelectable(gameObject);
        }
#endif

        protected virtual void Awake()
        {
            Use.UpdateRequiredSelectable(gameObject);
        }

        protected virtual void Update()
        {
            var oldPosition = transform.localPosition;

            var fingers = Use.UpdateAndGetFingers();

            var screenDelta = LeanGesture.GetScreenDelta(fingers);

            if (screenDelta != Vector2.zero)
            {
                if (transform is RectTransform)
                {
                    TranslateUI(screenDelta);
                }
                else
                {
                    Translate(screenDelta);
                }
            }

            remainingTranslation += transform.localPosition - oldPosition;

            var factor = CwHelper.DampenFactor(Damping, Time.deltaTime);

            var newRemainingTranslation = Vector3.Lerp(remainingTranslation, Vector3.zero, factor);

            transform.localPosition = oldPosition + remainingTranslation - newRemainingTranslation;

            if (fingers.Count == 0 && inertia > 0.0f && Damping > 0.0f)
            {
                newRemainingTranslation = Vector3.Lerp(newRemainingTranslation, remainingTranslation, inertia);
            }

            remainingTranslation = newRemainingTranslation;
        }

        private void TranslateUI(Vector2 screenDelta)
        {
            var finalCamera = _camera;

            if (finalCamera == null)
            {
                var canvas = transform.GetComponentInParent<Canvas>();

                if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                {
                    finalCamera = canvas.worldCamera;
                }
            }

            var screenPoint = RectTransformUtility.WorldToScreenPoint(finalCamera, transform.position);

            screenPoint += screenDelta * Sensitivity;

            var worldPoint = default(Vector3);

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, finalCamera, out worldPoint) == true)
            {
                worldPoint.x = Mathf.Clamp(worldPoint.x, minX, maxX);
                worldPoint.y = Mathf.Clamp(worldPoint.y, minY, maxY);

                transform.position = worldPoint;
            }
        }

        private void Translate(Vector2 screenDelta)
        {
            var camera = CwHelper.GetCamera(this._camera, gameObject);

            if (camera != null)
            {
                var screenPoint = camera.WorldToScreenPoint(transform.position);

                screenPoint += (Vector3)screenDelta * Sensitivity;

                var worldPoint = camera.ScreenToWorldPoint(screenPoint);

                worldPoint.x = Mathf.Clamp(worldPoint.x, minX, maxX);
                worldPoint.y = Mathf.Clamp(worldPoint.y, minY, maxY);

                transform.position = worldPoint;
            }
            else
            {
                Debug.LogError("Failed to find camera. Either tag your camera as MainCamera, or set one in this component.", this);
            }
        }
    }
}
