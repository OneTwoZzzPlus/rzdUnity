using GameLogic;
using UnityEngine;

public class WebGLReceiver : MonoBehaviour
{
    private static int index = -1;

    private void WebGlBridgeDetected(int ind)
    {
        index = ind;
        WebGLBridge.Detect(index);
    }

    private void WebGlBridgeSetMatrix(object obj)
    {
        var matHelper = JsonUtility.FromJson<MatHelper>(obj.ToString());
        if (matHelper is var args)
        {
            var m = new Matrix4x4
            {
                m00 = args.elements[0],
                m10 = args.elements[1],
                m20 = args.elements[2],
                m30 = args.elements[3],
                m01 = args.elements[4],
                m11 = args.elements[5],
                m21 = args.elements[6],
                m31 = args.elements[7],
                m02 = args.elements[8],
                m12 = args.elements[9],
                m22 = args.elements[10],
                m32 = args.elements[11],
                m03 = args.elements[12],
                m13 = args.elements[13],
                m23 = args.elements[14],
                m33 = args.elements[15]
            };
            WebGLBridge.Compute(index, m);
        }
    }

    private void WebGlBridgeLost(int ind)
    {
        index = ind;
        WebGLBridge.Lost(index);
    }
}