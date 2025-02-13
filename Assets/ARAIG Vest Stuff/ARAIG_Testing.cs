using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices; // Needed to use import DLLs functionality

struct Vector3f
{
    public float x;
    public float y;
    public float z;
}

public class ARAIG_Testing : MonoBehaviour
{
    const string l_ARAIG_DLL = "ARAIG_SDK_64_NTech";
    // Retrieving the functions from the DLL to be used to update the various sensors
    [DllImport(l_ARAIG_DLL)] private static extern void fn_ARAIG_UpdateMultipleSensorsVibration(int[] a_SensorIDs, int a_NumberOfSensorIDs, float a_Intensity, float a_Duration);
    [DllImport(l_ARAIG_DLL)] private static extern void fn_ARAIG_UpdateMultipleSensorsStim(int[] a_SensorIDs, int a_NumberOfSensorIDs, float a_Intensity, float a_Coverage, float a_Frequency, float a_Duration);
    [DllImport(l_ARAIG_DLL)] private static extern void fn_ARAIG_UpdateMultiplePredefinedGroupsOfSensorsVibration(int[] a_PredefinedGroups, int a_NumberOfPredefinedGroups, float a_Intensity, float a_Duration);
    [DllImport(l_ARAIG_DLL)]
    private static extern void fn_ARAIG_UpdateRegionsWithinXYZDistanceOfARAIGLocalSpacePointVibration(Vector3f a_PointInARAIGLocalSpace,
        float a_XDistance, float a_YDistance, float a_ZDistance,
        float a_Intensity, float a_Duration);

    // This functionality will delete the Visualization txt file. This is best to use if you want a clean Visualization file each time you run your code
    [DllImport(l_ARAIG_DLL)] private static extern void fn_ARAIG_DESTROY_STORED_VISUALIZATION_OUTPUT_DATA();

    // Start is called before the first frame update
    void Start()
    {
        fn_ARAIG_DESTROY_STORED_VISUALIZATION_OUTPUT_DATA();
        fn_ActivateARAIG();
    }

    void fn_ActivateARAIG()
    {
        int[] l_SensorIDs = new int[] { 5, 18, 20, 25, 27, 32, 33 };
        fn_ARAIG_UpdateMultipleSensorsVibration(l_SensorIDs, l_SensorIDs.Length, 0.5f, 10.0f);
        fn_ARAIG_UpdateMultipleSensorsVibration(l_SensorIDs, l_SensorIDs.Length, 0.5f, 10.0f);
    }
}