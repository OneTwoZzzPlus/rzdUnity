using UnityEngine;

namespace DefaultNamespace
{
    public static class Utils
    {
        private static Vector3 velocity;
        private static Vector3 scaleVelocity;
        private const float SMOOTH_TIME_POS = .1f;
        private const float SMOOTH_TIME_ROT = .4f;
        private const float SMOOTH_TIME_SC = .2f;

        public static void SetTransformFromMatrix(this Transform transform, Matrix4x4 matrix)
        {
            transform.localPosition = ExtractTranslationFromMatrix(matrix);
            transform.localRotation = ExtractRotationFromMatrix(matrix);
            transform.localScale = ExtractScaleFromMatrix(matrix);
        }
        
        public static void SetTransformFromMatrixStab(this Transform transform, Matrix4x4 matrix)
        {
            var targetPosition = ExtractTranslationFromMatrix(matrix);
            var targetRotation = ExtractRotationFromMatrix(matrix);
            var targetScale = ExtractScaleFromMatrix(matrix);
            // Debug.Log($"position {targetPosition}");
            // Debug.Log($"rotation {targetRotation.eulerAngles}");
            // Debug.Log($"scale {targetScale}");
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref velocity, SMOOTH_TIME_POS);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, SMOOTH_TIME_ROT);
            transform.localScale = Vector3.SmoothDamp(transform.localScale,targetScale,ref scaleVelocity, SMOOTH_TIME_SC);
        }

        public static Vector3 ExtractTranslationFromMatrix(Matrix4x4 matrix)
        {
            Vector3 translate;
            translate.x = matrix.m03;
            translate.y = matrix.m13;
            translate.z = matrix.m23;
            return translate;
        }
        
        public static Quaternion ExtractRotationFromMatrix(Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;
            
            return forward == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(forward, upwards);
        }
        
        public static Vector3 ExtractScaleFromMatrix(Matrix4x4 matrix)
        {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;

            if (Vector3.Cross(matrix.GetColumn(0), matrix.GetColumn(1)).normalized != (Vector3)matrix.GetColumn(2).normalized)
            {
                scale.x *= -1;
            }

            return scale;
        }
    }
}