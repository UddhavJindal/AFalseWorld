using UnityEngine;
using Cinemachine;

public class CMShakeScript : MonoBehaviour
{
    public static CMShakeScript Instance { get; private set; }
    private CinemachineVirtualCamera CMVC;
    private float ShakeTimer;
    private void Awake()
    {
        Instance = this;
        CMVC = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraShake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cmbmcp = CMVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmbmcp.m_AmplitudeGain = intensity;
        ShakeTimer = time;
    }

    private void Update()
    {
        if(ShakeTimer>0)
        {
            ShakeTimer -= Time.deltaTime;
            if(ShakeTimer<=0)
            {
                CinemachineBasicMultiChannelPerlin cmbmcp = CMVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cmbmcp.m_AmplitudeGain = 0;
            }
        }
    }
}
