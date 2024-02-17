//Shady
using UnityEngine;

[ExecuteInEditMode]
public class Reveal : MonoBehaviour
{
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;
    [SerializeField] GameObject Lighting;

    void Update ()
    {
        if (Lighting.activeInHierarchy) {
            float dist = Vector3.Distance(SpotLight.transform.position, transform.position);
            if (dist < 10) {
                Mat.SetVector("MyLightPosition", SpotLight.transform.position);
                Mat.SetVector("MyLightDirection", -SpotLight.transform.forward);
                Mat.SetFloat("MyLightAngle", SpotLight.spotAngle);
            }
            else {
                Mat.SetVector("MyLightPosition", Vector4.zero);
                Mat.SetVector("MyLightDirection", Vector4.zero);
                Mat.SetFloat("MyLightAngle", 0);
            }
        }
        else {
            Mat.SetVector("MyLightPosition", Vector4.zero);
            Mat.SetVector("MyLightDirection", Vector4.zero);
            Mat.SetFloat("MyLightAngle", 0);
        }
    }//Update() end
}//class end